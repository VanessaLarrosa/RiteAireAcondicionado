using UnityEngine;
using UnityEngine.EventSystems;

public class TaskToggleClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Task task;
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f; // tiempo máximo entre clics para considerar "doble clic"

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            // Doble clic detectado
            if (task.Check)
            {
                // Si está completada, volverla pendiente
                task.Check = false;
                Checkboard.Instance.RevertTask(task); // NUEVO método
            }
            else
            {
                // Si está pendiente, completarla
                task.Check = true;
                Checkboard.Instance.CompleteTask(task);
            }
        }

        lastClickTime = Time.time;
    }
}
