using UnityEngine;

public class MedidorTemperatura : MonoBehaviour
{
    public enum TipoZona { Alta, Baja }
    public TipoZona tipoZona;

    public float temperatura = 0f;
    public float velocidadCambio = 5f;

    private bool dentro = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            dentro = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            dentro = false;
    }

    private void Update()
    {
        if (!dentro) return;

        float cambio = velocidadCambio * Time.deltaTime;

        if (tipoZona == TipoZona.Alta)
            temperatura += cambio;
        else
            temperatura -= cambio;

        Debug.Log($"Temperatura actual en zona {tipoZona}: {temperatura:F2}");
    }
}