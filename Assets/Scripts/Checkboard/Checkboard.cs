using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening; // Librería para animaciones suaves como mover objetos o cambiar opacidad

// Clase principal encargada de gestionar el sistema de tareas con animaciones, paginación y guardado de progreso.
public class Checkboard : MonoBehaviour, ICheckboard
{
    [Header("Arrastrar aquí")]
    public Image pencil;                   // Imagen del lápiz animado que dibuja la línea de tachado al completar una tarea
    public AudioClip scratchSound;        // Sonido que se reproduce cuando se tacha una tarea
    public Transform tasksContainer;      // Objeto padre que contendrá todas las tareas instanciadas
    public GameObject taskPrefab;         // Prefab de la tarea (debe contener un TextMeshPro y una imagen para la línea de tachado)

    public static Checkboard Instance;    // Patrón Singleton: permite acceder a esta clase desde cualquier otro script mediante Checkboard.Instance
    public RectTransform libroTransform;  // Usado para animar el libro de tareas (mostrar u ocultar)
    public bool bookHide;                 // Indica si el libro de tareas está oculto (true) o visible (false)

    public PageManager pageManager;       // Referencia al sistema de paginación que gestiona qué tareas se muestran por página

    [Header("Ajustes de Animación + sonido")]
    [Tooltip("Delay antes de que suene el efecto de tachado")]
    public float soundDelay = 0.1f;       // Tiempo que espera antes de reproducir el sonido del tachado
    [Tooltip("Duración total de la animación de tachado")]
    public float animationDuration = 0.5f; // Duración de la animación del lápiz al tachar
    [Tooltip("Distancia que recorre el lápiz")]
    public float pencilTravelDistance = 300f; // Distancia horizontal que recorre el lápiz al tachar
    [Tooltip("Posición inicial del lápiz (derecha de la tarea)")]
    public float pencilStartOffset = 200f; // Distancia desde el texto de la tarea hasta donde empieza el lápiz

    [Header("Configuración de paginación")]
    [Tooltip("Cantidad de tareas por página")]
    public int tareasPorPagina = 10; // Número máximo de tareas visibles por página

    [Header("UI References")]
    [Tooltip("Texto que muestra 'Página X de Y'")]
    public TextMeshProUGUI pageCountText; // Texto que muestra en qué página estás actualmente

    // Diccionario que relaciona cada tarea con su interfaz visual (texto, línea, estado)
    private Dictionary<Task, TaskUI> tasks = new Dictionary<Task, TaskUI>();

    // Clase interna que contiene los elementos visuales de una tarea y si está completada
    private class TaskUI
    {
        public TextMeshProUGUI textComponent; // Texto que se muestra en la tarea
        public Image redLine;                 // Imagen de la línea de tachado
        public bool isCompleted;              // Estado de la tarea (true = completada)
    }

    void Awake()
    {
        // Configura el singleton. Si ya existe una instancia y no es esta, se destruye
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        // Por defecto, el libro comienza oculto
        bookHide = true;
    }

    void Start()
    {
        UpdatePageText(); // Mostrar la página actual al iniciar

        // Validación 1: asegurar que el PageManager está asignado
        if (pageManager == null)
        {
            pageManager = FindObjectOfType<PageManager>();
            if (pageManager == null)
            {
                Debug.LogError("PageManager no encontrado!");
                return;
            }
        }

        // Validación 2: asegurar que se han asignado las referencias de la interfaz
        if (tasksContainer == null || taskPrefab == null)
        {
            Debug.LogError("TasksContainer o taskPrefab no asignado!");
            return;
        }

        // Lanza una corrutina para esperar un frame antes de iniciar, asegurando que el resto del sistema está listo
        StartCoroutine(DelayedInit());
    }

    IEnumerator DelayedInit()
    {
        yield return null; // Esperamos un frame

        pageManager.UpdatePageCount(); // Calcula cuántas páginas hacen falta
        ShowCurrentPage();             // Muestra las tareas de la primera página

        // Recorre todas las tareas y restaura su estado guardado como completado
        foreach (var task in TaskManager.Instance.task)
        {
            if (PlayerPrefs.GetString($"TaskCompleted_{task.Summary}", "0") == "1")
            {
                if (tasks.TryGetValue(task, out TaskUI taskUI))
                {
                    taskUI.redLine.gameObject.SetActive(true);                     // Activa la línea roja de tachado
                    taskUI.textComponent.color = new Color(0.6f, 0.6f, 0.6f);      // Cambia el color del texto a gris
                    taskUI.isCompleted = true;                                     // Marca la tarea como completada
                    Debug.Log($"Tarea {task.Summary} cargada como completada");
                }
            }
        }
    }

    void Update()
    {
        // Atajo de teclado: muestra u oculta el libro de tareas al pulsar TAB
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bookHide = !bookHide;

            if (bookHide)
                HideBook();
            else
                ShowBook();
        }
    }

    // Registra una tarea nueva en la interfaz (instancia el prefab y la guarda en el diccionario)
    public void RegisterTask(Task task)
    {
        if (taskPrefab == null || tasksContainer == null)
        {
            Debug.LogError("\u00a1Prefab o container no asignados en Checkboard!", this);
            return;
        }

        if (tasks.ContainsKey(task))
        {
            Debug.LogError($"Task {task.Summary} ya está registrada.");
            return;
        }

        GameObject newTask = Instantiate(taskPrefab, tasksContainer);
        TextMeshProUGUI textComp = newTask.GetComponentInChildren<TextMeshProUGUI>();
        Image line = newTask.GetComponentInChildren<Image>(true);

        textComp.text = task.Summary;
        line.gameObject.SetActive(false);

        newTask.GetComponent<TaskInteractable>()?.SetTask(task);

        tasks.Add(task, new TaskUI
        {
            textComponent = textComp,
            redLine = line,
            isCompleted = false
        });
    }

    // Marca una tarea como completada visualmente y lanza la animación de tachado
    public void CompleteTask(Task task)
    {
        if (tasks.TryGetValue(task, out TaskUI taskUI))
        {
            if (taskUI.isCompleted)
            {
                Debug.Log($"La tarea {task.Summary} YA ESTABA COMPLETADA");
                return;
            }

            Debug.Log($"\u00a1Tarea COMPLETADA!: {task.Summary}");
            StartCoroutine(TachadoAnimation(taskUI));
        }
        else
        {
            Debug.LogError($"No existe la tarea {task.Summary}");
        }
    }

    // Reverso de CompleteTask: deshace el tachado y marca la tarea como incompleta
    public void UncompleteTask(Task task)
    {
        if (tasks.TryGetValue(task, out TaskUI taskUI))
        {
            taskUI.redLine.gameObject.SetActive(false);
            taskUI.textComponent.color = Color.black;
            taskUI.isCompleted = false;

            PlayerPrefs.SetString($"TaskCompleted_{task.Summary}", "0");
            PlayerPrefs.Save();
        }
    }

    // Corrutina que hace la animación del lápiz y aplica el efecto de tachado visual
    private IEnumerator TachadoAnimation(TaskUI task)
    {
        pencil.transform.position = task.textComponent.transform.position + new Vector3(pencilStartOffset, 0, 0);
        pencil.gameObject.SetActive(true);

        yield return new WaitForSeconds(soundDelay);
        AudioSource.PlayClipAtPoint(scratchSound, Camera.main.transform.position);

        float elapsed = 0f;
        Vector3 startPos = pencil.transform.position;
        Vector3 endPos = startPos - new Vector3(pencilTravelDistance, 0, 0);

        while (elapsed < animationDuration)
        {
            pencil.transform.position = Vector3.Lerp(startPos, endPos, elapsed / animationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        task.redLine.gameObject.SetActive(true);
        task.textComponent.color = new Color(0.6f, 0.6f, 0.6f);
        task.isCompleted = true;

        Task taskKey = null;
        foreach (var kvp in tasks)
        {
            if (kvp.Value == task)
            {
                taskKey = kvp.Key;
                break;
            }
        }

        if (taskKey != null)
        {
            PlayerPrefs.SetString($"TaskCompleted_{taskKey.Summary}", "1");
            PlayerPrefs.Save();
            Debug.Log($"Tarea {taskKey.Summary} guardada como completada");
        }

        yield return new WaitForSeconds(0.4f);
        pencil.gameObject.SetActive(false);
    }

    // Muestra visualmente el libro de tareas en pantalla
    public void ShowBook()
    {
        libroTransform.DOAnchorPosX(-600, 0.5f); // Mueve la libreta para que aparezca (ajusta X según tu UI)
    }

    // Oculta visualmente el libro de tareas
    public void HideBook()
    {
        libroTransform.DOAnchorPosX(-1200, 0.5f); // Mueve la libreta fuera de la pantalla (ajusta X según tu UI)
    }

    // Se puede llamar desde fuera para forzar el tachado
    public void PlayTachadoFromTask(Task task)
    {
        CompleteTask(task);
    }

    // Muestra solo las tareas de la página actual
    public void ShowCurrentPage()
    {
        if (tasksContainer == null || pageManager == null || taskPrefab == null || TaskManager.Instance == null)
        {
            Debug.LogError("\u274c Faltan referencias en Checkboard");
            return;
        }

        UpdatePageText();

        foreach (Transform child in tasksContainer)
        {
            if (child.CompareTag("TaskUI")) // Solo elimina los elementos con el tag "TaskUI"
            {
                Destroy(child.gameObject);
            }
        }

        tasks.Clear();

        int startIndex = Mathf.Clamp(pageManager.ActualPage * tareasPorPagina, 0, TaskManager.Instance.task.Count);
        int endIndex = Mathf.Min(startIndex + tareasPorPagina, TaskManager.Instance.task.Count);

        for (int i = startIndex; i < endIndex; i++)
        {
            Task task = TaskManager.Instance.task[i];
            if (task != null)
            {
                RegisterTask(task);
                if (task.Check) CompleteTask(task);
            }
        }
    }

    // Pasa a la página siguiente
    public void NextPage()
    {
        if (pageManager.ActualPage < pageManager.PageCount - 1)
        {
            pageManager.ActualPage++;
            ShowCurrentPage();
        }
    }

    // Vuelve a la página anterior
    public void PreviousPage()
    {
        if (pageManager.ActualPage > 0)
        {
            pageManager.ActualPage--;
            ShowCurrentPage();
        }
    }

    // Actualiza el texto que muestra en qué página estamos ("Página X de Y")
    public void UpdatePageText()
    {
        if (pageCountText != null && pageManager != null)
        {
            pageCountText.text = $"Página {pageManager.ActualPage + 1} de {pageManager.PageCount}";
        }
    }
}
