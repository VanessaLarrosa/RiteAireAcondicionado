using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

// Esta clase gestiona todas las tareas del sistema. Se encarga de cargarlas, mantener su estado,
// actualizar su representación visual, y facilitar su modificación por parte de otros scripts.
public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance; // Singleton: permite acceder a esta clase desde cualquier parte del proyecto

    public List<Task> task = new List<Task>(); // Lista de tareas activas en el sistema

    public bool manual = false; // Si es false, las tareas se cargan automáticamente desde Resources/ScriptableTasks

    private List<GameObject> objectTaskList = new List<GameObject>(); // Lista de objetos visuales que representan las tareas en la UI
    public List<GameObject> ObjectTaskList
    {
        get { return objectTaskList; }
        set { objectTaskList = value; }
    }

    public Texture2D uncheckedCB; // Imagen para la casilla no marcada
    public Texture2D checkedCB;   // Imagen para la casilla marcada

    
    
    // Se ejecuta al cargar la escena. Inicializa el singleton y carga las tareas si no es modo manual.
    private void Awake()
    {
        //PlayerPrefs.DeleteAll(); // ¡Esto se puede descomentar para reiniciar las tareas guardadas!
        //PlayerPrefs.Save();
        //Debug.Log("✅ PlayerPrefs borrados");

        Instance = this; // Asigna esta instancia al singleton

        // Si no estamos en modo manual, se cargan todas las tareas desde la carpeta Resources
        if (manual == false)
        {
            task.Clear();
            task = Resources.LoadAll<Task>("ScriptableTasks").ToList();

            // Se asigna una página a cada tarea para la paginación
            for (int i = 0; i < task.Count; i++)
            {
                task[i].Page = i / 10;
            }
        }

        //// Validación: asegurarse de que el Checkboard está en la escena
        //if (Checkboard.Instance == null)
        //{
        //    Debug.LogError("Checkboard no encontrado en la escena!");
        //}
    }

    // Alterna el estado de una tarea (completada/incompleta)
    public void ToggleTask(Task task)
    {
        if (task == null) return;

        int taskIndex = this.task.FindIndex(t => t == task);
        if (taskIndex == -1)
        {
            Debug.LogWarning("La tarea no existe en el TaskManager");
            return;
        }

        // Si ya está marcada, la desmarcamos
        if (this.task[taskIndex].Check)
        {
            this.task[taskIndex].Check = false;
            Checkboard.Instance?.UncompleteTask(task); // Borra el tachado visual
        }
        else // Si no está marcada, la marcamos como completada
        {
            this.task[taskIndex].Check = true;
            Checkboard.Instance?.CompleteTask(task); // Lanza el tachado visual
        }
    }

    // Alterna el estado y actualiza la textura del checkbox en la UI
    public void TaskSwapCheck(Task tk)
    {
        int index = task.IndexOf(tk);
        RawImage checkBox = ObjectTaskList[index].GetComponentInChildren<RawImage>();

        task[index].Check = !task[index].Check;
        if (task[index].Check == true)
        {
            checkBox.texture = checkedCB;
            Debug.Log("Marcada tarea " + task[index].Summary);
        }
        else
        {
            checkBox.texture = uncheckedCB;
            Debug.Log("Desmarcada tarea " + task[index].Summary);
        }
    }

    // Marca la tarea como completada y actualiza su aspecto visual
    public void TaskChecker(Task tk)
    {
        int index = task.IndexOf(tk);
        if (!task[index].Check)
        {
            task[index].Check = true;

            // Actualiza la textura del checkbox visual si existe
            if (index < ObjectTaskList.Count)
            {
                RawImage checkBox = ObjectTaskList[index].GetComponentInChildren<RawImage>();
                checkBox.texture = checkedCB;
            }
            Checkboard.Instance.CompleteTask(tk);
        }
    }

    // Desmarca la tarea como completada y actualiza su aspecto visual
    public void TaskUnchecker(Task tk)
    {
        int index = task.IndexOf(tk);
        if (task[index].Check == true)
        {
            RawImage checkBox = ObjectTaskList[index].GetComponentInChildren<RawImage>();

            task[index].Check = !task[index].Check;
            checkBox.texture = uncheckedCB;
        }
    }
}
