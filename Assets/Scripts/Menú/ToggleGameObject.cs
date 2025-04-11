using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{

    public GameObject objectToToggle; // Aquí puedes arrastrar desde Unity el objeto que quieres que aparezca y desaparezca.
    public GameObject objectToAffectOnToggle; // Aquí puedes arrastrar otro objeto que quieras que cambie cuando el primero cambia.
    public bool deactivateOnToggleOn = false; // Si marcas esta casilla en Unity, el segundo objeto se desactivará cuando el primero se active.

    public void Toggle()
    {
        if (objectToToggle != null) // Primero, revisamos si has puesto un objeto en "objectToToggle" en Unity.
        {
            // Esta línea cambia si el objeto está activo o no. Si está activo, lo desactiva; si está desactivado, lo activa.
            objectToToggle.SetActive(!objectToToggle.activeSelf);

            // Ahora, vemos si se puso algo en "objectToAffectOnToggle".
            if (objectToAffectOnToggle != null)
            {
                // Revisamos si esta marcada la casilla "deactivateOnToggleOn".
                if (deactivateOnToggleOn)
                {
                    // Si la casilla está marcada, hacemos lo contrario al objeto principal.
                    objectToAffectOnToggle.SetActive(!objectToToggle.activeSelf);
                }
                else
                {
                    // Si la casilla no está marcada, simplemente cambiamos el estado del segundo objeto también.
                    objectToAffectOnToggle.SetActive(!objectToAffectOnToggle.activeSelf);
                }
            }
        }
        else
        {
            // Si olvidaste arrastrar un objeto en Unity, este mensaje te lo dirá en la consola.
            Debug.LogError("¡No se ha asignado ningún objeto para alternar en el Inspector!");
        }
    }
}

