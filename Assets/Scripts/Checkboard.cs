using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Checkboard : MonoBehaviour, ICheckboard
{
    [Header("Arrastrar aqu�")]
    public Image pencil; //la imagen del lapiz que tacha
    public AudioClip scratchSound; //El audio al tachar
    public Transform tasksContainer; // Aqui se crear�n las tareas(es el ChechBoard)
    public GameObject taskPrefab;   // He hecho un Prefab con las tareas base(tarea+tachado)(tiene que ser TextMeshPro+imagen)

    [Header("Ajustes de Animaci�n + sonido")] //Opciones para cuadrarlo bien
    [Tooltip("Delay antes de que suene el efecto de tachado")]
    public float soundDelay = 0.1f;
    [Tooltip("Duraci�n total de la animaci�n de tachado")]
    public float animationDuration = 0.5f;
    [Tooltip("Distancia que recorre el l�piz")]
    public float pencilTravelDistance = 300f;
    [Tooltip("Posici�n inicial del l�piz (derecha de la tarea)")]
    public float pencilStartOffset = 200f;

    // "Diccionrio" para guardar las tareas numeradas
    private Dictionary<int, TaskUI> tasks = new Dictionary<int, TaskUI>();

    // Clase iterna de cada tarea
    private class TaskUI
    {
        public TextMeshProUGUI textComponent;
        public Image redLine;
        public bool isCompleted;
    }

    void Start()
    {
        // Registrar tareas
        RegisterTask(1, "Pulsa la tecla ' T '");
        RegisterTask(2, "Ahora pulsa la tecla ' Y '");
        RegisterTask(3, "Ahora pulsa la tecla ' U '");
        RegisterTask(4, "Clikea la carta");

        //  Restaurar tareas completadas al cargar la escena
        foreach (var taskEntry in tasks)
        {
            int taskOrder = taskEntry.Key;
            if (GameState.tareasCompletadas.Contains(taskOrder))
            {
                MarkTaskAsCompleted(taskOrder);
            }
        }

        // Caso especial para la carta PRUEBA (Carta Yeray) Despues borrar por las nuevas funciones
        if (GameState.cartaTocada)
        {
            CompleteTask(4);
        }
    }


        private void MarkTaskAsCompleted(int taskOrder)
    {
        if (tasks.TryGetValue(taskOrder, out TaskUI task))
        {
            task.redLine.gameObject.SetActive(true);
            task.textComponent.color = new Color(0.6f, 0.6f, 0.6f);
            task.isCompleted = true;
        }
    }


    // M�todo para A�ADIR TAREAS NUEVAS (se llamar�n desde otros scripts) es de la interfaz ICheckboard
    public void RegisterTask(int taskOrder, string description)
    {
        if (tasks.ContainsKey(taskOrder))
        {
            Debug.LogError($"Task order {taskOrder} ya est� registrado."); //por si se duplican tareas
            return;
        }

        // Creamos una nueva tarea desde el prefab
        GameObject newTask = Instantiate(taskPrefab, tasksContainer);
        TextMeshProUGUI textComp = newTask.GetComponentInChildren<TextMeshProUGUI>();
        Image line = newTask.GetComponentInChildren<Image>(true); //Para buscar aunque est� desactivado



        // Le ponemos el texto (ej: "1. Recoger la llave")
        textComp.text = $"{taskOrder}. {description}";
        line.gameObject.SetActive(false);

        // La guardamos en el diccionario para luego poder tacharla
        tasks.Add(taskOrder, new TaskUI
        {
            textComponent = textComp,
            redLine = line,
            isCompleted = false
        });
    }

    // M�todo para TACHAR TAREAS (se llama autom�ticamente cuando se complete algo) de la interfaz ICheckboard
    public void CompleteTask(int taskOrder)
    {
        if (tasks.TryGetValue(taskOrder, out TaskUI task))
        {
            if (task.isCompleted) return; // Evita marcarla varias veces

            Debug.Log($"✅ Tarea {taskOrder} COMPLETADA: {task.textComponent.text}");
            StartCoroutine(TachadoAnimation(task));

            // ✅ Guardar en `GameState`
            GameState.tareasCompletadas.Add(taskOrder);
        }
        else
        {
            Debug.LogError($" La tarea {taskOrder} no existe en el diccionario.");
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

        // 5. GUARDAR ESTADO AL COMPLETAR
        // Obtenemos el n�mero de tarea (taskOrder) desde el diccionario
        int taskOrder = -1;
        foreach (var kvp in tasks)
        {
            if (kvp.Value == task)
            {
                taskOrder = kvp.Key;
                break;
            }
        }

        if (taskOrder != -1)
        {
            PlayerPrefs.SetInt($"TaskCompleted_{taskOrder}", 1); // 1 = completada
            PlayerPrefs.Save();
            Debug.Log($"Tarea {taskOrder} guardada como completada");
        }


        // 6. Volver a ocultar l�piz
        yield return new WaitForSeconds(0.4f);
        pencil.gameObject.SetActive(false);
    }

    void Update()
    {
        // Para los ejemplos y simulaciones de tareas
        if (Input.GetKeyDown(KeyCode.T)) CompleteTask(1);
        if (Input.GetKeyDown(KeyCode.Y)) CompleteTask(2);
        if (Input.GetKeyDown(KeyCode.U)) CompleteTask(3);
    }
}