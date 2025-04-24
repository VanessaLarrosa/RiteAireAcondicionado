using UnityEngine;

public class Camara : MonoBehaviour
{
    private Transform objetivo;
    public float velocidad = 5f;

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;

    [Header("Offset de seguimiento")]
    public Vector3 offset = new Vector3(0, 0.5f, -1.5f);  // Ajusta para ver desde arriba o detrás

    private bool siguiendo = false;

    void Start()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
    }

    void Update()
    {
        if (siguiendo && objetivo != null)
        {
            Vector3 destino = objetivo.position + offset;
            transform.position = Vector3.Lerp(transform.position, destino, velocidad * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            VolverAPosicionInicial();
        }

    }

    public void SeguirObjeto(Transform nuevoObjetivo)
    {
        objetivo = nuevoObjetivo;
        siguiendo = true;
    }

    public void DejarDeSeguir()
    {
        siguiendo = false;
        objetivo = null;
    }

    public void VolverAPosicionInicial()
    {
        siguiendo = false;
        objetivo = null;
        transform.position = posicionInicial;
        transform.rotation = rotacionInicial;
    }
}
