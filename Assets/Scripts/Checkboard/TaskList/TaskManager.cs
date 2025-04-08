using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{

    public static TaskManager Instance;
    public List<Task> task = new List<Task>();
    public bool manual = false;
    private List<GameObject> objectTaskList = new List<GameObject>();
    public List<GameObject> ObjectTaskList
    {
        get { return objectTaskList; }
        set { objectTaskList = value; }
    }
    public Texture2D uncheckedCB;
    public Texture2D checkedCB;
    public Task[] taskTest;

    private Checkboard checkboard;

    // On awake, creates the singletone and checks if task are managed automatically or manually
    private void Awake()
    {
        Instance = this;

        if (manual == false)
        {
            task.Clear();
            task = Resources.LoadAll<Task>("ScriptableTasks").ToList();

            // Asignar páginas
            for (int i = 0; i < task.Count; i++)
            {
                task[i].Page = i / 10;
            }
        }

        // Asegurar que Checkboard existe
        if (Checkboard.Instance == null)
        {
            Debug.LogError("Checkboard no encontrado en la escena!");
        }
    }

    // Method for swapping between checked and unchecked
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

    // Method for just checking
    public void TaskChecker(Task tk)
    {
        int index = task.IndexOf(tk);
        if (!task[index].Check)
        {
            task[index].Check = true;

            // Actualizar checkbox visual
            if (index < ObjectTaskList.Count)
            {
                RawImage checkBox = ObjectTaskList[index].GetComponentInChildren<RawImage>();
                checkBox.texture = checkedCB;
            }
            Checkboard.Instance.CompleteTask(tk);
        }
    }

    // Method for just unchecking
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
