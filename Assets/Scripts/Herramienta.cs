using UnityEngine;

// Obliga a que el objeto tenga un Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class Herramienta : MonoBehaviour
{
 
    private Rigidbody rb;
    private Vector3 offset;             // Diferencia entre mouse y objeto al hacer clic
    private Vector3 targetPosition;     // Posición destino del objeto al arrastrar
    private float zCoord;               // Profundidad del objeto respecto a la cámara
    private bool dragging;              // Indica si está siendo arrastrado
 
    // Velocidad con la que el objeto sigue al mouse
    [SerializeField] private float followSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el Rigidbody del objeto

        // Activar físicas y mejorar detección de colisiones
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    // Al hacer clic sobre el objeto
    void OnMouseDown()
    {
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z; // Obtener la profundidad en Z del objeto respecto a la cámara
        offset = transform.position - GetMouseWorldPosition();  // Calcular el offset entre el mouse y el objeto
        rb.useGravity = false; // Desactivar la gravedad para que no caiga mientras se arrastra
        dragging = true; // Activar el arrastre
    }

    // Mientras se mantiene el clic
    void OnMouseDrag()
    {
        targetPosition = GetMouseWorldPosition() + offset; // Actualizar la posición objetivo usando el mouse y el offset
    }

    // Al soltar el clic
    void OnMouseUp()
    {
        dragging = false; // Desactivar el arrastre
        //rb.useGravity = true; // Reactivar la gravedad (Desactivado para poder 
        rb.velocity = Vector3.zero; // Detener el movimiento residual
    }


    // Se ejecuta en intervalos fijos, ideal para manejar físicas
    void FixedUpdate()
    {

        // Si se está arrastrando y es arrastrable
        if (dragging)
        {
            // Calcular dirección y aplicar velocidad para mover el objeto suavemente
            Vector3 direction = targetPosition - rb.position;
            rb.velocity = direction * followSpeed;
        }
    }

    // Cuando el objeto colisiona con otro
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Colisión con: {collision.gameObject.name}"); // Mostrar nombre del objeto con el que se colisiona


        // Detectar colisiones específicas por tag
        switch (collision.gameObject.tag)
        {
            case "Morado":
                Debug.Log("Colisión Morado");
                break;
            case "Naranja":
                Debug.Log("Colisión Naranja");
                break;
        }
    }

    // Convertir posición del mouse en pantalla a posición en el mundo 3D
    private Vector3 GetMouseWorldPosition()
    {
        // Crear un vector con la posición del mouse y la profundidad del objeto
        Vector3 screenMouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zCoord);

        // Convertir de pantalla a mundo
        return Camera.main.ScreenToWorldPoint(screenMouse);
    }
}
