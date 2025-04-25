using UnityEngine;
using TMPro;
public class Temperatura : MonoBehaviour
{
    public enum TemperatureType { Alta, Baja }
    public TemperatureType tipoTemperatura;
    public float tiempoNecesario = 3f;
    private float tiempoReseteo = 0f;
    public TextMeshProUGUI texto;
    private bool objetoDentro = false;
    private GameObject objetoActual = null;
    private int valorVisual = 0;
        void Start()
    {
        // Inicialmente el texto estara invisiblee
        texto.enabled = false;
    }
    void OnTriggerEnter(Collider other) //Cuando entra el objeto
    {
        if (other.CompareTag("Objeto")) // Aqui se instancia el tag que tiene el usuario o el objeto que va a interactuar
        {
            objetoDentro = true;
            objetoActual = other.gameObject;
            tiempoReseteo = 0f; // Reinicia el contador cuando entra

            texto.enabled = true; // Hacer visible el texto cuando entre en contacto
        }
    }
    void OnTriggerStay(Collider other) // Cuando el objeto está dentro
    {
        if (objetoDentro && other.gameObject == objetoActual)
        {
            tiempoReseteo += Time.deltaTime; // Acumula el tiempo que el objeto está dentro del trigger

            // Calculamos el porcentaje del tiempo transcurrido
            float porcentaje = Mathf.Clamp01(tiempoReseteo / tiempoNecesario); // valor de 0 a 1

            // Asegurarnos de que el valor visual no pase de 100
            if (tipoTemperatura == TemperatureType.Alta)
            {
                // Para ir calentando el valor sube hasta 100
                valorVisual = Mathf.RoundToInt(porcentaje * 100f);
                valorVisual = Mathf.Min(valorVisual, 100); // Limita el valor a 100
                texto.text = $"Calentando... {valorVisual}%"; // Muestra el porcentaje de calentamiento
                Debug.Log($"Calentando... {valorVisual}");
            }
            else if (tipoTemperatura == TemperatureType.Baja)
            {
                // Para enfriar, el valor baja hasta 0
                valorVisual = Mathf.RoundToInt((1f - porcentaje) * 100f);
                valorVisual = Mathf.Min(valorVisual, 100); // Limitamos a 100 para que no se vaya por las nubes
                texto.text = $"Enfriando... {valorVisual}%"; // Muestra el porcentaje de enfriamiento
                Debug.Log($"Enfriando... {valorVisual}");
            }
        }
    }
    void OnTriggerExit(Collider other) //Cuando sale el objeto
    {
        if (other.gameObject == objetoActual)
        {
            objetoDentro = false;
            tiempoReseteo = 0f;
            objetoActual = null;

            texto.enabled = false; // lo volvemos a poner invisble
        }
    }
}