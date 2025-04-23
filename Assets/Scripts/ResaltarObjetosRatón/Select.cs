using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Select : MonoBehaviour
{

    //Creamos variables de materiales y transformación
    public Material highlightMaterial;
    public Material selectionMaterial;

    private Dictionary<Renderer, Material> originalMaterials = new(); // Guardamos los materiales originales por cada Renderer

    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;


    // Update is called once per frame
    void Update()
    {
        // Bloque que maneja el resaltado de los objetos 

        if (highlight != null)
        {
            // Restauramos todos los materiales originales
            foreach (var entry in originalMaterials)
            {
                if (entry.Key != null)
                    entry.Key.material = entry.Value;
            }

            originalMaterials.Clear();
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Creamos un raycast con un if que verifique si el raton está sobre un elemento de la UI
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Selectable") && highlight != selection) //Hacemos que verifique que el objeto tiene el tag "Selectable" para aplicar lo siguiente
            {
                {
                    // Modificamos todos los MeshRenderer del objeto y sus hijos
                    foreach (var renderer in highlight.GetComponentsInChildren<Renderer>())
                    {
                        if (renderer.material != highlightMaterial)
                        {
                            originalMaterials[renderer] = renderer.material; // Guardamos el material original
                            renderer.material = highlightMaterial; // Aplicamos el resaltado
                        }
                    }
                }
            }
            else
            {
                highlight = null; //Si no sucede eso, se borra el resaltado
            }
        }

       
    }
}

