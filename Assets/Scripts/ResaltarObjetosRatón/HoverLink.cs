using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color originalColor;

    public Color hoverColor = Color.red;


    private TMP_Text textMeshPro;

    void Start()
    {
      
        textMeshPro = GetComponent<TMP_Text>();
        originalColor = textMeshPro.color;
    }

   
    public void OnPointerEnter(PointerEventData eventData)
    {
        textMeshPro.color = hoverColor; 
    }

   
    public void OnPointerExit(PointerEventData eventData)
    {
        textMeshPro.color = originalColor;  
    }
}

