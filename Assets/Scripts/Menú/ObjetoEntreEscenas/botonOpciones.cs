using UnityEngine;

public class botonOpciones : MonoBehaviour
{
    public static botonOpciones Instance { get; private set; }

    private void Awake()
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

    public void mostrarMenu()
    {
        gameObject.SetActive(true);
    }
}

