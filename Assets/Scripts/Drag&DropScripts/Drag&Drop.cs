
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    [SerializeField] bool hasPhysics = true; //Variable para saber si el objeto tiene físicas
    [SerializeField] float forceMultiplier = 1f; //Variable para multiplicar la fuerza (dar impulso)
    [SerializeField] float minDragDistance = 0.1f; //Variable para saber la distancia mínima para arrastrar y aplicar la fuerza. Ajustar según masa del objeto
    [SerializeField] Camera cam; //Variable para guardar la cámara

    Vector3 mousePosition; //Variable para guardar la posición del mouse
    Vector3 initialPosition; //Variable para guardar la posición inicial del objeto
    Rigidbody rb; //Variable para guardar el rigidbody del objeto
    Vector3 initialVelocity; //Variable para guardar la velocidad inicial del objeto

    private void Start()
    {
       if (cam == null) //Si la cámara no está asignada
        {
            cam = Camera.main; //Asignar la cámara principal
        }
       
       if (hasPhysics) //Si el objeto tiene físicas
        {
            rb = GetComponent<Rigidbody>(); //Obtener el rigidbody del objeto
        }

    }

    Vector3 GetMousePosition() //Función para obtener la posición del mouse
    {
        return cam.WorldToScreenPoint(transform.position); //Devolver la posición del mouse en la pantalla
    }

    private void OnMouseDown() //Cuando se presiona el mouse
    {
        mousePosition = Input.mousePosition - GetMousePosition(); // Calcular la posición del mouse con el offset del objeto
        initialPosition = transform.position; //Guardar la posición inicial del objeto 

        if (hasPhysics) 
        {
            initialVelocity = rb.velocity; //Guardar la velocidad del rigidbody
            rb.velocity = Vector3.zero; //Detener la velocidad del objeto al mover el objeto 
        }
    }

    private void OnMouseDrag() //Cuando se arrastra el mouse
    {
        transform.position = cam.ScreenToWorldPoint(Input.mousePosition - mousePosition); //actualizar el transform en base a la posición del mouse y el offset
    }

    private void OnMouseUp() //Cuando se suelta el mouse
    {
        if (!hasPhysics) return; //Si el objeto no tiene físicas
        
           
        Vector3 dragDirection = transform.position - initialPosition; //Calcular la dirección del arrastre
        float dragDistance = dragDirection.magnitude; //Calcular la distancia del arrastre

          if (dragDistance >= minDragDistance && rb.velocity.magnitude <= 1f) //Si la distancia del arrastre es mayor o igual a la distancia mínima y la velocidad del objeto es menor o igual a 1
          {
                dragDirection.Normalize(); //Normalizar la dirección del arrastre
                rb.AddForce(dragDirection * forceMultiplier, ForceMode.Impulse); //Aplicar una fuerza al objeto
          }

        rb.velocity = initialVelocity; //Restaurar la velocidad del objeto
    }

    private void OnCollisionEnter(Collision collision) // Método para detectar colisiones
    {
        IPlatform platform = collision.gameObject.GetComponent<IPlatform>(); //Obtener el componente IPlatform de la colisión

        if (platform != null)
        {
            IBall ball = GetComponent<IBall>(); //Obtener el componente IBall del objeto
            if (ball != null)
            {
                platform.OnBallCollision(ball); //Llamar al método OnBallCollision de la plataforma
            }
        }
    }


}