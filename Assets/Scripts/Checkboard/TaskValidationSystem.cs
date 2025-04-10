using System.Collections;
using UnityEngine;

// Esta clase se encarga de comprobar periódicamente si las tareas han sido completadas automáticamente
// según la lógica definida en su validador asociado (TaskValidator).
public class TaskValidationSystem : MonoBehaviour
{
    [SerializeField] private float checkInterval = 0.5f; // Intervalo en segundos entre cada comprobación de tareas

    // Al iniciar la escena, se lanza una corrutina que comprobará las tareas cada cierto tiempo
    private void Start()
    {
        StartCoroutine(ValidationRoutine());
    }

    // Corrutina que se ejecuta de forma indefinida cada "checkInterval" segundos
    private IEnumerator ValidationRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval); // Espera antes de cada comprobación

            // Si no existe un TaskManager activo, se detiene la corrutina (no tiene sentido seguir)
            if (TaskManager.Instance == null) yield break;

            // Recorre todas las tareas gestionadas por el TaskManager
            foreach (Task task in TaskManager.Instance.task)
            {
                // Solo comprobamos tareas que no estén ya completadas y que tengan un validador asignado
                if (task != null && !task.Check && task.validator != null)
                {
                    task.CheckCompletion(); // Llama al método que pregunta al validador si la tarea está completada
                }
            }
        }
    }
}
