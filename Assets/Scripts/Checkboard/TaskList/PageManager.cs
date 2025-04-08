using UnityEngine;
using System.Collections; // ¡Ojo! Necesario para IEnumerator

public class PageManager : MonoBehaviour
{
    public static PageManager Instance;

    private int pageCount;
    public int PageCount
    {
        get { return pageCount; }
        set { pageCount = value; }
    }

    private int actualPage;
    public int ActualPage
    {
        get { return actualPage; }
        set
        {
            actualPage = Mathf.Clamp(value, 0, pageCount - 1);
            Checkboard.Instance.UpdatePageText(); // Nueva línea
            Checkboard.Instance.ShowCurrentPage();
        }
    }

    void Start()
    {
        Instance = this;
        UpdatePageCount();
        StartCoroutine(DelayedInit());
    }

    private IEnumerator DelayedInit()
    {
        yield return null; // Espera 1 frame

        UpdatePageCount();

        Checkboard checkboard = Checkboard.Instance;
        if (checkboard == null) checkboard = FindObjectOfType<Checkboard>();

        if (checkboard != null)
        {
            checkboard.ShowCurrentPage();
        }
        else
        {
            Debug.LogError("Checkboard no encontrado después de búsqueda!");
        }
    }

    public void UpdatePageCount()
    {
        int totalTareas = TaskManager.Instance.task.Count;
        int tareasPorPagina = Checkboard.Instance != null ? Checkboard.Instance.tareasPorPagina : 10;
        pageCount = Mathf.CeilToInt((float)totalTareas / tareasPorPagina);
    }
}
