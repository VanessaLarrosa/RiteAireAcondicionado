using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarManguera : MonoBehaviour
{
    public GameObject conector;     // El objeto que debe soltarse aquí (Conector1)
    public GameObject mostrarEsto;  // El objeto que se mostrará (manguera.002)

    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto que entra es el conector
        if (other.gameObject == conector)
        {
            if (mostrarEsto != null)
                mostrarEsto.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == conector)
        {
            if (mostrarEsto != null)
                mostrarEsto.SetActive(false);
        }
    }
}
