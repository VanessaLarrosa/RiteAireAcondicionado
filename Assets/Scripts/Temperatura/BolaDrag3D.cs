using UnityEngine;

public class Drag3D : MonoBehaviour
{
    private Camera cam;
    private bool arrastrando = false;
    private float distancia;

    void Start()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        distancia = Vector3.Distance(cam.transform.position, transform.position);
        arrastrando = true;
    }

    void OnMouseUp()
    {
        arrastrando = false;
    }

    void Update()
    {
        if (arrastrando)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            transform.position = ray.GetPoint(distancia);
        }
    }
}
