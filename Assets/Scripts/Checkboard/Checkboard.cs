using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Checkboard : MonoBehaviour, ICheckboard
{
    [Header("Arrastrar aquí")]
    public Image pencil; //la imagen del lapiz que tacha
    public AudioClip scratchSound; //El audio al tachar
    public Transform tasksContainer; // Aqui se crearán las tareas(es el ChechBoard)
    public GameObject taskPrefab;   // He hecho un Prefab con las tareas base(tarea+tachado)(tiene que ser TextMeshPro+imagen)

    [Header("Ajustes de Animación + sonido")] //Opciones para cuadrarlo bien
    [Tooltip("Delay antes de que suene el efecto de tachado")]
    public float soundDelay = 0.1f;
    [Tooltip("Duración total de la animación de tachado")]
    public float animationDuration = 0.5f;
    [Tooltip("Distancia que recorre el lápiz")]
    public float pencilTravelDistance = 300f;
    [Tooltip("Posición inicial del lápiz (derecha de la tarea)")]
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
        // Ejemplos de registro de tareas iniciales para hacer pruebas (esto luego lo pulirá Yeray con un ResgisterTask)
        RegisterTask(1, "Pulsa la tecla ' T '");
        RegisterTask(2, "Ahora pulsa la tecla ' Y '");
        RegisterTask(3, "Ahora pulsa la tecla ' U '");

        // Carga tareas completadas al iniciar(para que esto funcione se tiene que modificar el cript del menu principal)
        //foreach (var task in tasks)
        //{
        //    int taskOrder = task.Key;
        //    if (PlayerPrefs.GetInt($"TaskCompleted_{taskOrder}", 0) == 1)
        //    {
        //        task.Value.redLine.gameObject.SetActive(true);
        //        task.Value.textComponent.color = new Color(0.6f, 0.6f, 0.6f);
        //        task.Value.textComponent.fontStyle = FontStyles.Strikethrough;
        //        task.Value.isCompleted = true;
        //        Debug.Log($"Tarea {taskOrder} cargada como completada");
        //    }
        //}

        
    }

    // Método para AÑADIR TAREAS NUEVAS (se llamarán desde otros scripts) es de la interfaz ICheckboard
    public void RegisterTask(int taskOrder, string description)
    {
        if (tasks.ContainsKey(taskOrder))
        {
            Debug.LogError($"Task order {taskOrder} ya está registrado."); //por si se duplican tareas
            return;
        }

        // Creamos una nueva tarea desde el prefab
        GameObject newTask = Instantiate(taskPrefab, tasksContainer);
        TextMeshProUGUI textComp = newTask.GetComponentInChildren<TextMeshProUGUI>();
        Image line = newTask.GetComponentInChildren<Image>(true); //Para buscar aunque esté desactivado



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

    // Método para TACHAR TAREAS (se llama automáticamente cuando se complete algo) de la interfaz ICheckboard
    public void CompleteTask(int taskOrder)
    {
        if (tasks.TryGetValue(taskOrder, out TaskUI task))
        {
            if (task.isCompleted) //Se comprueba si la tarea ya estaba hecha
            {
                Debug.Log($"La tarea {taskOrder} ('{task.textComponent.text}') YA ESTABA COMPLETADA");
                return;
            }
            Debug.Log($"¡Tarea {taskOrder} COMPLETADA!: {task.textComponent.text}"); //Avisa de la tarea cpmpletada
            StartCoroutine(TachadoAnimation(task)); //Empieza la animación
        }
        else
        {
            Debug.LogError($"No existe la tarea {taskOrder}"); //Por si hiciera falta
        }
    }

    // Todo el tema del tachado (no tocar mucho a menos que quieras cambiar la animación)
    private IEnumerator TachadoAnimation(TaskUI task)
    {
        // 1. Posicion para el lápiz
        pencil.transform.position = task.textComponent.transform.position + new Vector3(pencilStartOffset, 0, 0);
        pencil.gameObject.SetActive(true);

        // 2. Delay para cuadrar mejor el sonido
        yield return new WaitForSeconds(soundDelay);
        AudioSource.PlayClipAtPoint(scratchSound, Camera.main.transform.position);

        // 3. Animación del lápiz
        float elapsed = 0f;
        Vector3 startPos = pencil.transform.position;
        Vector3 endPos = startPos - new Vector3(pencilTravelDistance, 0, 0);

        while (elapsed < animationDuration)
        {
            pencil.transform.position = Vector3.Lerp(startPos, endPos, elapsed / animationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 4. Efectos visuales del tachado (texto se vuelve gris, y línea roja))
        task.redLine.gameObject.SetActive(true);
        task.textComponent.color = new Color(0.6f, 0.6f, 0.6f);
        //task.textComponent.fontStyle = FontStyles.Strikethrough;
        task.isCompleted = true;

        // 5. GUARDAR ESTADO AL COMPLETAR
        // Obtenemos el número de tarea (taskOrder) desde el diccionario
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


        // 6. Volver a ocultar lápiz
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