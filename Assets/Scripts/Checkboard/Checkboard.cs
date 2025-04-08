using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Checkboard : MonoBehaviour, ICheckboard
{
    [Header("Arrastrar aqu�")]
    public Image pencil; //la imagen del lapiz que tacha
    public AudioClip scratchSound; //El audio al tachar
    public Transform tasksContainer; // Aqui se crear�n las tareas(es el ChechBoard)
    public GameObject taskPrefab;   // He hecho un Prefab con las tareas base(tarea+tachado)(tiene que ser TextMeshPro+imagen)

    public static Checkboard Instance;
    public RectTransform libroTransform;
    public bool bookHide; // Ocultar Tareas Pendientes
        
    public PageManager pageManager;// Referencia al PageManager existente

    [Header("Ajustes de Animaci�n + sonido")] //Opciones para cuadrarlo bien
    [Tooltip("Delay antes de que suene el efecto de tachado")]
    public float soundDelay = 0.1f;
    [Tooltip("Duraci�n total de la animaci�n de tachado")]
    public float animationDuration = 0.5f;
    [Tooltip("Distancia que recorre el l�piz")]
    public float pencilTravelDistance = 300f;
    [Tooltip("Posici�n inicial del l�piz (derecha de la tarea)")]
    public float pencilStartOffset = 200f;

    [Header("Configuración de paginación")]
    [Tooltip("Cantidad de tareas por página")]
    public int tareasPorPagina = 10;
    [Header("UI References")]
    [Tooltip("Texto que muestra 'Página X de Y'")]
    public TextMeshProUGUI pageCountText; // Arrastra tu objeto TextMeshPro aquí


    // "Diccionrio" para guardar las tareas numeradas
    private Dictionary<Task, TaskUI> tasks = new Dictionary<Task, TaskUI>();

    // Clase iterna de cada tarea
    private class TaskUI
    {
        public TextMeshProUGUI textComponent;
        public Image redLine;
        public bool isCompleted;
    }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        bookHide = true;
    }

    void Start()
    {
        UpdatePageText();
        // ⚠️ Validaciones básicas
        if (pageManager == null)
        {
            pageManager = FindObjectOfType<PageManager>();
            if (pageManager == null)
            {
                Debug.LogError("PageManager no encontrado!");
                return;
            }
        }

        if (tasksContainer == null || taskPrefab == null)
        {
            Debug.LogError("TasksContainer o taskPrefab no asignado!");
            return;
        }

        // 🕒 Inicia la lógica de carga después de asegurarse de que todo está bien
        StartCoroutine(DelayedInit());
    }

    IEnumerator DelayedInit()
    {
        yield return null; // ⏳ Esperamos un frame a que Unity inicialice todo

        // 🧮 Actualizamos el número de páginas
        pageManager.UpdatePageCount();

        // 📋 Mostramos tareas de la primera página
        ShowCurrentPage();

        // 🧠 Restauramos el estado guardado (PlayerPrefs)
        foreach (var task in TaskManager.Instance.task)
        {
            if (PlayerPrefs.GetString($"TaskCompleted_{task.Summary}", "0") == "1")
            {
                if (tasks.TryGetValue(task, out TaskUI taskUI))
                {
                    taskUI.redLine.gameObject.SetActive(true);
                    taskUI.textComponent.color = new Color(0.6f, 0.6f, 0.6f);
                    taskUI.isCompleted = true;
                    Debug.Log($"Tarea {task.Summary} cargada como completada");
                }
            }
        }
    }


    void Update()
    {
        // Para los ejemplos y simulaciones de tareas
       // if (Input.GetKeyDown(KeyCode.T)) CompleteTask(1);
        //if (Input.GetKeyDown(KeyCode.Y)) CompleteTask(2);
        //if (Input.GetKeyDown(KeyCode.U)) CompleteTask(3);

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bookHide = !bookHide;

            if (bookHide == true)
            {
                HideBook();
            }
            else
            {
                ShowBook();
            }

        }
    }

    // M�todo para A�ADIR TAREAS NUEVAS (se llamar�n desde otros scripts) es de la interfaz ICheckboard
    public void RegisterTask(Task task)
    {
        // Validación añadida:
        if (taskPrefab == null || tasksContainer == null)
        {
            Debug.LogError("¡Prefab o container no asignados en Checkboard!", this);
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

        tasks.Add(task, new TaskUI
        {
            textComponent = textComp,
            redLine = line,
            isCompleted = false
        });
    }

    // M�todo para TACHAR TAREAS (se llama autom�ticamente cuando se complete algo) de la interfaz ICheckboard
    public void CompleteTask(Task task)
    {
        if (tasks.TryGetValue(task, out TaskUI taskUI))
        {
            if (taskUI.isCompleted)
            {
                Debug.Log($"La tarea {task.Summary} YA ESTABA COMPLETADA");
                return;
            }

            Debug.Log($"¡Tarea COMPLETADA!: {task.Summary}");
            StartCoroutine(TachadoAnimation(taskUI));
        }
        else
        {
            Debug.LogError($"No existe la tarea {task.Summary}");
        }
    }
    public void RevertTask(Task task)
    {
        if (tasks.TryGetValue(task, out var taskUI))
        {
            taskUI.redLine.gameObject.SetActive(false);
            taskUI.textComponent.color = Color.black;
            taskUI.isCompleted = false;

            // También lo quitamos del guardado si quieres
            PlayerPrefs.SetString($"TaskCompleted_{task.Summary}", "0");
            PlayerPrefs.Save();

            Debug.Log($"Tarea {task.Summary} marcada como INCOMPLETA");
        }
    }


    // Todo el tema del tachado (no tocar mucho a menos que quieras cambiar la animaci�n)
    private IEnumerator TachadoAnimation(TaskUI task)
    {
        // 1. Posicion para el l�piz
        pencil.transform.position = task.textComponent.transform.position + new Vector3(pencilStartOffset, 0, 0);
        pencil.gameObject.SetActive(true);

        // 2. Delay para cuadrar mejor el sonido
        yield return new WaitForSeconds(soundDelay);
        AudioSource.PlayClipAtPoint(scratchSound, Camera.main.transform.position);

        // 3. Animaci�n del l�piz
        float elapsed = 0f;
        Vector3 startPos = pencil.transform.position;
        Vector3 endPos = startPos - new Vector3(pencilTravelDistance, 0, 0);

        while (elapsed < animationDuration)
        {
            pencil.transform.position = Vector3.Lerp(startPos, endPos, elapsed / animationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 4. Efectos visuales del tachado (texto se vuelve gris, y l�nea roja))
        task.redLine.gameObject.SetActive(true);
        task.textComponent.color = new Color(0.6f, 0.6f, 0.6f);
        //task.textComponent.fontStyle = FontStyles.Strikethrough;
        task.isCompleted = true;

        // Obtenemos el n�mero de tarea (taskOrder) desde el diccionario
        // 5. GUARDAR ESTADO AL COMPLETAR
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
            // Usamos el nombre de la tarea como identificador único
            PlayerPrefs.SetString($"TaskCompleted_{taskKey.Summary}", "1"); // "1" = completada
            PlayerPrefs.Save();
            Debug.Log($"Tarea {taskKey.Summary} guardada como completada");
        }



        // 6. Volver a ocultar l�piz
        yield return new WaitForSeconds(0.4f);
        pencil.gameObject.SetActive(false);
    }


    public void ShowBook()
    {
        libroTransform.DOAnchorPosX(-600, 0.5f);
    }

    public void HideBook()
    {
        libroTransform.DOAnchorPosX(-1200, 0.5f);

    }
    public void PlayTachadoFromTask(Task task)
    {
        // Reutilizar vuestra animación normal
        CompleteTask(task);
    }
    // Método actualizado para mostrar tareas
    public void ShowCurrentPage()
    {
        // Validaciones
        if (tasksContainer == null || pageManager == null || taskPrefab == null || TaskManager.Instance == null)
        {
            Debug.LogError("❌ Faltan referencias en Checkboard");
            return;
        }
        // Actualizar texto al final
        UpdatePageText();

        // Elimina solo los objetos que son tareas
        foreach (Transform child in tasksContainer)
        {
            if (child.CompareTag("TaskUI")) // Asegúrate que el prefab tenga este tag
            {
                Destroy(child.gameObject);
            }
        }

        tasks.Clear();

        // Calcular tareas a mostrar
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



    // Actualiza los métodos de paginación
    public void NextPage()
    {
        if (pageManager.ActualPage < pageManager.PageCount - 1)
        {
            pageManager.ActualPage++;
            ShowCurrentPage();
        }
    }

    public void PreviousPage()
    {
        if (pageManager.ActualPage > 0)
        {
            pageManager.ActualPage--;
            ShowCurrentPage();
        }
    }
    public void UpdatePageText()
    {
        if (pageCountText != null && pageManager != null)
        {
            pageCountText.text = $"Página {pageManager.ActualPage + 1} de {pageManager.PageCount}";
        }
    }
}