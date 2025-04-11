using UnityEngine;

public class Camara : MonoBehaviour
{
    private Vector3 posicion;
    private Quaternion objetivoRotacion;
    private bool mover = false;
    public float distanciaDelObjeto = 3f;
    public float velocidad = 5f;

    public void MoveToObject(Transform target)
    {
        // Calculamos una posición justo enfrente del cubo
        Vector3 direction = (transform.position - target.position).normalized;
        posicion = target.position + direction * distanciaDelObjeto;

        // También queremos que mire hacia el cubo
        objetivoRotacion = Quaternion.LookRotation(target.position - posicion);

        mover = true;
    }

    void Update()
    {
        if (mover)
        {
            // Movimiento suave de posición
            transform.position = Vector3.Lerp(transform.position, posicion, Time.deltaTime * velocidad);

            // Movimiento suave de rotación
            transform.rotation = Quaternion.Slerp(transform.rotation, objetivoRotacion, Time.deltaTime * velocidad);

            // Detener si estamos muy cerca
            if (Vector3.Distance(transform.position, posicion) < 0.05f)
            {
                mover = false;
            }
        }
    }
}