using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Select : MonoBehaviour
{

    //Creamos variables de materiales y transformación
    public Material highlightMaterial;
    public Material selectionMaterial;

    private Material originalMaterial;
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;


    // Update is called once per frame
    void Update()
    {
        // Bloque que maneja el resaltado de los objetos 

        if (highlight != null)
        {
            highlight.GetComponent<MeshRenderer>().material = originalMaterial; // Reset al material original
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Creamos un raycast con un if que verifique si el raton está sobre un elemento de la UI
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Selectable") && highlight != selection) //Hacemos que verifique que el objeto tiene el tag "Selectable" para aplicar lo siguiente
            {
                if (highlight.GetComponent<MeshRenderer>().material != highlightMaterial)
                {
                    originalMaterial = highlight.GetComponent<MeshRenderer>().material; //Hacemos que el objeto vuelva a poder tener su color original
                    highlight.GetComponent<MeshRenderer>().material = highlightMaterial;//cuando el el focus del highlight no este en el
                }
            }
            else
            {
                highlight = null; //Si no sucede eso, se borra el resaltado
            }
        }

       
    }
}

