using UnityEngine;

public interface ICheckboard
{
    /// <summary>Registra una nueva tarea en el checkboard</summary>
    /// <param name="task">El objeto Task a registrar</param>
    void RegisterTask(Task task);

    /// <summary>Se marca una tarea como completada.</summary>
    /// <param name="task">La tarea a completar.</param>
    void CompleteTask(Task task);
}
