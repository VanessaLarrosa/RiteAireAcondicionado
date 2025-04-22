
using UnityEngine;

public class ActivatePanelOnLoad : MonoBehaviour
{

    [SerializeField] string panelNameToActivateFromOptions = "OptionsMenuPlay"; // Panel a activar desde Options
    [SerializeField] string panelNameToActivateFromMainMenu = "LogosAccess"; // Panel a activar desde Main Menu
    [SerializeField] string CanvasPlayNameToDeactivate = "CanvasPlay"; // Canvas a desactivar en ambos casos


    private static bool cameFromOptions = false; //Variable para saber si se viene de opciones


    public static void SetCameFromOptions() //Método para activar la variable cameFromOptions
    {
        cameFromOptions = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Desactivar el canvas y el panel que corresponde al CanvasPLay al iniciar la escena

        GameObject CanvasPlayToDeactivate = GameObject.Find(CanvasPlayNameToDeactivate);
        if (CanvasPlayToDeactivate != null)
        {
            CanvasPlayToDeactivate.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No se encontró el Canvas a desactivar: " + CanvasPlayNameToDeactivate);
        }

        //Activar el panel correspondiente dependiendo de la variable cameFromOptions

        if (cameFromOptions)
        {
            GameObject panelToActivate = GameObject.Find(panelNameToActivateFromOptions); // Buscar el panel a activar desde Options
            if (panelToActivate != null)
            {
                panelToActivate.SetActive(true); 
            }
            else
            {
                Debug.LogWarning("No se encontró el panel a activar (desde Options): " + panelNameToActivateFromOptions); // Mensaje de advertencia si no se encuentra el panel
            }

            // Reiniciar la variable
            cameFromOptions = false;
        }

        // Si no se viene de opciones, activar el panel correspondiente desde Main Menu
        else
        {
            GameObject panelToActivate = GameObject.Find(panelNameToActivateFromMainMenu); // Buscar el panel a activar desde Main Menu
            if (panelToActivate != null)
            {
                panelToActivate.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No se encontró el panel a activar (desde Main Menu): " + panelNameToActivateFromMainMenu);
            }
        }
    }



    }
