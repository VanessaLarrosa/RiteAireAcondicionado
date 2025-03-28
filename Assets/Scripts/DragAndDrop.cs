using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseAsWorldPoint();
        rb.isKinematic = true; // Desactivar física al arrastrar
    }

    void OnMouseDrag()
    {
        // Usar Rigidbody para mover la esfera, respetando las colisiones
        Vector3 targetPosition = GetMouseAsWorldPoint() + offset;
        rb.MovePosition(targetPosition);
    }

    void OnMouseUp()
    {
        rb.isKinematic = false; // Restaurar física al soltar
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "CuadroRojo")
        {
            Debug.Log("La esfera está en el cuadro rojo");
        }
        else if (other.gameObject.name == "CuadroVerde")
        {
            Debug.Log("La esfera está en el cuadro verde");
        }
    }
}
