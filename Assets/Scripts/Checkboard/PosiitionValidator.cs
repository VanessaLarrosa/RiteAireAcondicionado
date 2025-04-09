using UnityEngine;

// Este atributo permite crear este ScriptableObject desde el menú de Unity: Assets > Create > Task Validators > Position Validator
[CreateAssetMenu(fileName = "NewPositionValidator", menuName = "Task Validators/Position Validator")]

// Clase que hereda de TaskValidator y permite validar si un objeto está dentro de una zona específica
public class PositionValidator : TaskValidator
{
    [Header("Nombres exactos de los objetos en la escena")]
    public string requiredObjectName = "PelotaNaranja"; // Nombre exacto del objeto que se debe encontrar (por ejemplo, una herramienta o pelota)
    public string targetAreaName = "ZonaValida";         // Nombre del objeto que representa la zona donde debe estar el anterior (por ejemplo, una caja o área válida)

    // Método obligatorio que determina si la tarea está completada
    public override bool IsTaskComplete()
    {
        // Busca el objeto que debe ser validado por su nombre en la jerarquía de la escena
        GameObject ball = GameObject.Find(requiredObjectName);

        // Busca el área destino por su nombre en la escena
        GameObject area = GameObject.Find(targetAreaName);

        // Si alguno de los dos no se encuentra, se muestra un mensaje y se retorna false
        if (ball == null || area == null)
        {
            Debug.LogWarning($"❌ No se encontró alguno de los objetos: {requiredObjectName} o {targetAreaName}");
            return false;
        }

        // Se intenta obtener el Collider del objeto principal y de la zona destino
        Collider ballCollider = ball.GetComponent<Collider>();
        Collider areaCollider = area.GetComponent<Collider>();

        // Si alguno no tiene Collider, también fallamos
        if (ballCollider == null || areaCollider == null)
        {
            Debug.LogWarning("❌ Alguno de los objetos no tiene Collider");
            return false;
        }

        // Validación principal:
        // Se comprueba si el centro del objeto (ball) está dentro de los límites del área destino
        // Esto permite verificar que el objeto esté correctamente colocado, sin importar su tamaño
        return areaCollider.bounds.Contains(ballCollider.bounds.center);
    }
}
