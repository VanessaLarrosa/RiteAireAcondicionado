using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowNext : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        PageManager.Instance.ActualPage++;
    }
}
