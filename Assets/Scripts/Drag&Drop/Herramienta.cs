using UnityEngine;
using System.Collections.Generic;

// Obliga a que el objeto tenga un Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class Herramienta : MonoBehaviour
{

    private Rigidbody rb;
    private Vector3 offset;             // Diferencia entre mouse y objeto al hacer clic
    private Vector3 targetPosition;     // Posición destino del objeto al arrastrar
    private float zCoord;               // Profundidad del objeto respecto a la cámara
    private bool dragging;              // Indica si está siendo arrastrado
    private bool isDroppedOnValidPoint = false; // Indica si el objeto fue soltado en un punto válido

    // Velocidad con la que el objeto sigue al mouse
    [Header("Movimiento del objeto")]
    [SerializeField] private float followSpeed = 10f;

    [Header("Puntos válidos para soltar")]
    [SerializeField] private List<Transform> validDropPoints; // Lista de puntos válidos para soltar el objeto

    [SerializeField] private float validDropDistance = 0.5f; // Distancia mínima para soltar el objeto en un punto válido


    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el Rigidbody del objeto

        // Activar físicas y mejorar detección de colisiones
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        if (validDropPoints == null) //Inicializar la lista si no estuviera inicializada
        {
            validDropPoints = new List<Transform>();
        }
    }

    // Al hacer clic sobre el objeto
    void OnMouseDown()
    {

        if (isDroppedOnValidPoint) //Permitir arrastrar si está en un punto válido
        {
            isDroppedOnValidPoint = false;
        }
        
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

        CheckValidDrop(); // Comprobar que el objeto se soltó en un punto válido
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

 

    // Convertir posición del mouse en pantalla a posición en el mundo 3D
    private Vector3 GetMouseWorldPosition()
    {
        // Crear un vector con la posición del mouse y la profundidad del objeto
        Vector3 screenMouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zCoord);

        // Convertir de pantalla a mundo
        return Camera.main.ScreenToWorldPoint(screenMouse);
    }

   

    public void OnHoverEnter()
    {
        if (!dragging)
        {
            Debug.Log($"[Herramienta] {gameObject.name} señalada", gameObject);
            // Activar tooltip aquí si se quiere
        }
    }

    public void OnHoverExit()
    {
        // Desactivar tooltip aquí si se quiere
    }

    private void CheckValidDrop()
    {
        isDroppedOnValidPoint = false; // Reiniciar el estado antes de verificar

        foreach (Transform dropPoint in validDropPoints)
        {
            // Calcular la distancia entre el objeto y el punto válido

            float distance = Vector3.Distance(transform.position, dropPoint.position);

            // Si la distancia es menor a la permitida

            if (distance <= validDropDistance)
            {
                Debug.Log("Objeto soltado en un punto válido");
                return;
            }
        }

        Debug.Log("No se encuentra en un punto válido");
    }
}