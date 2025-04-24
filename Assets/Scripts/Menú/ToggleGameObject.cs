using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToggleGameObject : MonoBehaviour
{
    public GameObject objectToToggle;
    public GameObject objectToAffectOnToggle;
    public bool deactivateOnToggleOn = false;

   

    public void Toggle()
    {
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(!objectToToggle.activeSelf);

            if (objectToAffectOnToggle != null)
            {
                if (deactivateOnToggleOn)
                {
                    objectToAffectOnToggle.SetActive(!objectToToggle.activeSelf);
                }
                else
                {
                    objectToAffectOnToggle.SetActive(!objectToAffectOnToggle.activeSelf);
                }
            }
        }
        else
        {
            Debug.LogError("¡No se ha asignado ningún objeto para alternar en el Inspector!");
        }
    }
}

