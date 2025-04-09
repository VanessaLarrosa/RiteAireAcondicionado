using UnityEngine;

public class Temperatura : MonoBehaviour
{
    public enum TemperatureType { Alta, Baja }
    public TemperatureType tipoTemperatura;

    public float tiempoNecesario = 3f;
    private float tiempoDentro = 0f;

    private bool objetoDentro = false;
    private GameObject objetoActual = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Objeto")) //Poner el nombre del objeto si no no lo pilla
        {
            objetoDentro = true;
            objetoActual = other.gameObject;
            tiempoDentro = 0f; // Reinicia el contador cuando entra
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (objetoDentro && other.gameObject == objetoActual)
        {
            tiempoDentro += Time.deltaTime;

            if (tiempoDentro >= tiempoNecesario)
            {
                MedirTemperatura();
                tiempoDentro = 0f; // Reinicia el tiempo para no medir infinitamente
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objetoActual)
        {
            objetoDentro = false;
            tiempoDentro = 0f;
            objetoActual = null;
        }
    }

    void MedirTemperatura()
    {
        if (tipoTemperatura == TemperatureType.Alta)
        {
            Debug.Log("Temperatura Alta detectada: 80°C");
        }
        else if (tipoTemperatura == TemperatureType.Baja)
        {
            Debug.Log("Temperatura Baja detectada: 10°C");
        }
    }
}