using UnityEngine;

public class ClickObjeto : MonoBehaviour
{
    public Camara camaraPersonalizada;

    private void OnMouseDown()
    {
        if (camaraPersonalizada != null)
        {
            camaraPersonalizada.SeguirObjeto(transform);
        }
        else
        {
            Debug.LogWarning("No se ha asignado una cámara en el Inspector.");
        }
    }
}
