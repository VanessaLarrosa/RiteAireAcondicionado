using UnityEngine;
using UnityEngine.EventSystems;

// Este script se debe colocar en la flecha de retroceso (izquierda) del sistema de paginación.
// Al hacer clic sobre ella, reduce en 1 el número de página actual.
// Usa el sistema de eventos de Unity (interfaz IPointerDownHandler).
public class ArrowPrev : MonoBehaviour, IPointerDownHandler
{
    // Este método se ejecuta cuando el usuario hace clic (presiona) sobre el GameObject asociado
    public void OnPointerDown(PointerEventData eventData)
    {
        // Cambia a la página anterior del Checkboard
        PageManager.Instance.ActualPage--;
    }
}