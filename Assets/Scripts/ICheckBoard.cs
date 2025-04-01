using UnityEngine;

public interface ICheckboard
{
    /// <summary>Registra una nueva tarea en el checkboard</summary>
    /// <param name="taskOrder">Para el identificador único de la tarea</param>
    /// <param name="description">Es el texto descriptivo de la tarea</param>
    void RegisterTask(int taskOrder, string description);

    /// <summary>Se marca una tarea como completada.</summary>
    /// <param name="taskOrder">ID de la tarea a completar.</param>
    void CompleteTask(int taskOrder);
}