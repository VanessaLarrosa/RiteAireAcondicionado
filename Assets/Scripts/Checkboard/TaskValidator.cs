using UnityEngine;

// Clase base abstracta para validadores de tareas.
// Se utiliza para definir diferentes formas de comprobar si una tarea ha sido completada.
// Esta clase debe ser heredada por otras que implementen su propia lógica de validación (por ejemplo, PositionValidator).
public abstract class TaskValidator : ScriptableObject
{
    // Método que deben implementar las clases hijas.
    // Debe devolver true si la tarea está completada, false si no lo está.
    // Unity llamará a este método de forma periódica mediante TaskValidationSystem.
    public abstract bool IsTaskComplete();
}