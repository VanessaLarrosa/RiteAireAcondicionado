using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TaskChecker : MonoBehaviour, IPointerDownHandler
{
    void Start()
    {
        int index = TaskManager.Instance.ObjectTaskList.IndexOf(gameObject);

        Debug.Log("Index de la task: " + index);
    }

    // It just checks and unchecks the task when clicking on it
    public void OnPointerDown(PointerEventData eventData)
    {
        int index = TaskManager.Instance.ObjectTaskList.IndexOf(gameObject);
        Task task = TaskManager.Instance.task[index];

        TaskManager.Instance.TaskSwapCheck(task);
    }
}
