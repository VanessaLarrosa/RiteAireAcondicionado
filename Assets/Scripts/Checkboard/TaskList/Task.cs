using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este atributo permite crear instancias de esta clase como ScriptableObjects desde Unity:
// Menú: Assets > Create > Task
[CreateAssetMenu(fileName = "New Task", menuName = "Task")]

// Esta clase representa una tarea individual.
// Se guarda como ScriptableObject, lo que permite reutilizar tareas entre escenas y editarlas sin necesidad de estar en runtime.
public class Task : ScriptableObject
{
    [SerializeField] private string summary; // Texto resumen o nombre de la tarea que se mostrará al usuario
    [SerializeField] private bool check;     // Indica si la tarea está completada o no (true = completada)
    private int page;                         // Número de página al que pertenece esta tarea (lo calcula el TaskManager)

    [Header("Validación")]
    public TaskValidator validator; // ScriptableObject que define la condición para dar esta tarea por completada

    // Propiedad para acceder/modificar el resumen desde otros scripts
    public string Summary
    {
        get { return summary; }
        set { summary = value; }
    }

    // Propiedad para acceder/modificar si está completada
    public bool Check
    {
        get { return check; }
        set { check = value; }
    }

    // Propiedad para acceder/modificar la página de la tarea
    public int Page
    {
        get { return page; }
        set { page = value; }
    }

    // Este método se llama para comprobar si la tarea se ha completado automáticamente según su validador
    public void CheckCompletion()
    {
        // Solo se comprueba si tiene un validador y aún no está completada
        if (validator != null && !Check && validator.IsTaskComplete())
        {
            // Marca la tarea como completada si cumple la condición
            TaskManager.Instance?.TaskChecker(this);
        }
    }
}