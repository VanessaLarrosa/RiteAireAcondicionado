using UnityEngine;
using UnityEngine.EventSystems;

public class TaskInteractable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Task task;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1) // Click simple
        {
            TaskManager.Instance.TaskChecker(task);
        }
        else if (eventData.clickCount == 2) // Doble click
        {
            TaskManager.Instance.TaskUnchecker(task);
        }
    }

}