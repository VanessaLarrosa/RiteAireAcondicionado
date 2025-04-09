using UnityEngine;
using UnityEngine.EventSystems;

public class TaskInteractable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Task task;
    private float lastClickTime = 0;
    private const float doubleClickTime = 0.3f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - lastClickTime < doubleClickTime)
        {
            if (TaskManager.Instance != null && task != null)
            {
                TaskManager.Instance.ToggleTask(this.task); // Usamos this.task para claridad
            }
        }
        lastClickTime = Time.time;
    }

    // Método para asignar la tarea programáticamente
    public void SetTask(Task newTask)
    {
        task = newTask;
    }
}