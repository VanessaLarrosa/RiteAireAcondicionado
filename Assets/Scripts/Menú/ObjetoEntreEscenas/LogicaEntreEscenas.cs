using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaEntreEscenas : MonoBehaviour
{

    private void Awake()
    {
        // Verifica si ya existe otro objeto entre escenas para evitar duplicados

        var noDestruirEntreEscenas = FindObjectsOfType<LogicaEntreEscenas>();
        if(noDestruirEntreEscenas.Length > 1) 
        {
            Destroy(gameObject); // Destruye el objeto si ya existe otro
            return;
        }

        DontDestroyOnLoad(gameObject); // Mantiene el objeto entre escenas
    } 
    
}
