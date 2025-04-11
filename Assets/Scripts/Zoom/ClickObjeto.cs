using UnityEngine;

public class ClickObjeto : MonoBehaviour
{
    public Camara cameraMover;

    void OnMouseDown()
    {
        cameraMover.MoveToObject(transform);  // le pasamos el objeto clicado como referencia
    }
}
