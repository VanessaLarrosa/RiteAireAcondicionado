using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeOpciones : MonoBehaviour
{
    public static ControladorDeOpciones Instance { get; private set; }

    public GameObject pantallaOpciones;


    public void Awake()
    {
         // Singleton Pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); // Si ya existe uno, destruir el duplicado
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject); // Persistir entre escenas
        }
    }
