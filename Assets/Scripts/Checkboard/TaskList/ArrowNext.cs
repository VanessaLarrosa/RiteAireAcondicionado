using UnityEngine;
using UnityEngine.EventSystems;

// Este script se debe colocar en la flecha de avance (derecha) del sistema de paginación.
// Al hacer clic sobre ella, incrementa en 1 la página actual del Checkboard.
// Usa el sistema de eventos de Unity (interfaz IPointerDownHandler).
public class ArrowNext : MonoBehaviour, IPointerDownHandler
{
    // Este método se ejecuta cuando el usuario hace clic (presiona) sobre el GameObject asociado
    public void OnPointerDown(PointerEventData eventData)
    {
        // Cambia a la siguiente página del Checkboard
        PageManager.Instance.ActualPage++;
    }
}
