<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta clase representa un objeto en el juego que puede detectar e interactuar con otros objetos interactuables cerca
public class InteractableObject : MonoBehaviour
{
    // Radio de detección para buscar objetos interactuables alrededor de este objeto
    public float detectionRadius = 0.5f;

    // Capa en la que se encuentran los objetos interactuables (para filtrar con OverlapSphere)
    public LayerMask interactableLayer;

    // Lista que guarda los objetos interactuables que están actualmente cerca
    public List<IInteract> nearbyInteractables = new List<IInteract>();

    // Se llama cada frame
    private void Update()
    {
        // Busca objetos interactuables cercanos en cada frame
        DetectNearbyInteractables();
    }

    // Método para detectar objetos interactuables cercanos     
    public void DetectNearbyInteractables()
    {
        /* Busca todos los colliders dentro de un radio alrededor de este objeto,
        que pertenezcan a la capa de objetos interactuables*/
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, interactableLayer);

        // Recorre todos los colliders encontrados
        foreach (Collider col in colliders)
        {
            // Intenta obtener un componente que implemente la interfaz IInteract
            IInteract interactable = col.GetComponent<IInteract>();

            // Si encontró uno válido y aún no estaba en la lista de cercanos
            if (interactable != null && !nearbyInteractables.Contains(interactable))
            {
                // Lo agrega a la lista y llama a su método Interact()
                nearbyInteractables.Add(interactable);
                interactable.Interact();
            }
        }

        // Elimina de la lista cualquier interactuable que ya no esté cerca
        nearbyInteractables.RemoveAll(interactable => !isObjectStillNearby(interactable));
    }

    // Verifica si un objeto interactuable sigue dentro del rango de detección
    public bool isObjectStillNearby(IInteract interactable)
    {
        // Obtiene su collider (si es un MonoBehaviour, porque IInteract puede no serlo)
        Collider col = (interactable as MonoBehaviour)?.GetComponent<Collider>();

        // Devuelve true si el collider existe y la distancia al objeto es menor o igual al radio
        return col != null && Vector3.Distance(transform.position, col.transform.position) <= detectionRadius;
    }

    // También detecta objetos interactuables mediante colisión (trigger)

    /*
    private void OnTriggerEnter(Collider other)
    {
        IInteract interactable = other.GetComponent<IInteract>();

        // Si el objeto que entró al trigger es interactuable y no está en la lista aún, lo agrega e interactúa
        if (interactable != null && !nearbyInteractables.Contains(interactable))
        {
            nearbyInteractables.Add(interactable);
            interactable.Interact();
        }
    }

    */

    // Este método dibuja en el editor un círculo para visualizar el radio de detección cuando seleccionás el objeto
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta clase representa un objeto en el juego que puede detectar e interactuar con otros objetos interactuables cerca
public class InteractableObject : MonoBehaviour
{
    // Radio de detección para buscar objetos interactuables alrededor de este objeto
    public float detectionRadius = 0.5f;

    // Capa en la que se encuentran los objetos interactuables (para filtrar con OverlapSphere)
    public LayerMask interactableLayer;

    // Lista que guarda los objetos interactuables que están actualmente cerca
    public List<IInteract> nearbyInteractables = new List<IInteract>();

    // Se llama cada frame
    private void Update()
    {
        // Busca objetos interactuables cercanos en cada frame
        DetectNearbyInteractables();
    }

    // Método para detectar objetos interactuables cercanos     
    public void DetectNearbyInteractables()
    {
        /* Busca todos los colliders dentro de un radio alrededor de este objeto,
        que pertenezcan a la capa de objetos interactuables*/
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, interactableLayer);

        // Recorre todos los colliders encontrados
        foreach (Collider col in colliders)
        {
            // Intenta obtener un componente que implemente la interfaz IInteract
            IInteract interactable = col.GetComponent<IInteract>();

            // Si encontró uno válido y aún no estaba en la lista de cercanos
            if (interactable != null && !nearbyInteractables.Contains(interactable))
            {
                // Lo agrega a la lista y llama a su método Interact()
                nearbyInteractables.Add(interactable);
                interactable.Interact();
            }
        }

        // Elimina de la lista cualquier interactuable que ya no esté cerca
        nearbyInteractables.RemoveAll(interactable => !isObjectStillNearby(interactable));
    }

    // Verifica si un objeto interactuable sigue dentro del rango de detección
    public bool isObjectStillNearby(IInteract interactable)
    {
        // Obtiene su collider (si es un MonoBehaviour, porque IInteract puede no serlo)
        Collider col = (interactable as MonoBehaviour)?.GetComponent<Collider>();

        // Devuelve true si el collider existe y la distancia al objeto es menor o igual al radio
        return col != null && Vector3.Distance(transform.position, col.transform.position) <= detectionRadius;
    }

    // También detecta objetos interactuables mediante colisión (trigger)

    /*
    private void OnTriggerEnter(Collider other)
    {
        IInteract interactable = other.GetComponent<IInteract>();

        // Si el objeto que entró al trigger es interactuable y no está en la lista aún, lo agrega e interactúa
        if (interactable != null && !nearbyInteractables.Contains(interactable))
        {
            nearbyInteractables.Add(interactable);
            interactable.Interact();
        }
    }

    */

    // Este método dibuja en el editor un círculo para visualizar el radio de detección cuando seleccionás el objeto
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

>>>>>>> develop
