using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowPrev : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        PageManager.Instance.ActualPage--;
    }
}