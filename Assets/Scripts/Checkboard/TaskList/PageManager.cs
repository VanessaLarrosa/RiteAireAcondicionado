using UnityEngine;
using System.Collections; // Necesario para usar corrutinas como IEnumerator

// Esta clase se encarga de controlar la paginación de las tareas mostradas en el Checkboard
// Calcula cuántas páginas hay y en qué página estás actualmente
public class PageManager : MonoBehaviour
{
    public static PageManager Instance; // Singleton: permite acceder fácilmente a esta clase desde cualquier otro script

    private int pageCount; // Total de páginas que existen según la cantidad de tareas
    public int PageCount
    {
        get { return pageCount; }
        set { pageCount = value; }
    }

    private int actualPage; // Página actual (empieza en 0)
    public int ActualPage
    {
        get { return actualPage; }
        set
        {
            // Evita que el valor se salga de los límites válidos
            actualPage = Mathf.Clamp(value, 0, pageCount - 1);

            // Cada vez que cambiamos de página, actualizamos la UI del Checkboard
            Checkboard.Instance.UpdatePageText();
            Checkboard.Instance.ShowCurrentPage();
        }
    }

    // Al iniciar, asignamos el singleton y calculamos las páginas
    void Start()
    {
        Instance = this;
    
        StartCoroutine(DelayedInit());
    }

    // Corrutina que espera un frame para asegurarse que el Checkboard ya está listo
    private IEnumerator DelayedInit()
    {
        yield return null;

        UpdatePageCount();

        // Buscamos el Checkboard en escena si no está asignado
        Checkboard checkboard = Checkboard.Instance;
        if (checkboard == null) checkboard = FindObjectOfType<Checkboard>();

        if (checkboard != null)
        {
            checkboard.ShowCurrentPage(); // Muestra la página actual de tareas
        }
        else
        {
            Debug.LogError("Checkboard no encontrado después de búsqueda!");
        }
    }

    // Calcula cuántas páginas necesitamos según el número total de tareas y cuántas se muestran por página
    public void UpdatePageCount()
    {
        int totalTareas = TaskManager.Instance.task.Count;

        // Usamos el valor definido en el Checkboard (por defecto 10 tareas por página)
        int tareasPorPagina = Checkboard.Instance != null ? Checkboard.Instance.tareasPorPagina : 10;

        // Calcula el total redondeando hacia arriba
        pageCount = Mathf.CeilToInt((float)totalTareas / tareasPorPagina);
    }
}
