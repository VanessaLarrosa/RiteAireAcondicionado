using UnityEngine;

public class ArrastrarConMouse : MonoBehaviour
{
    private Camera cam;
    private Plane planoMovimiento;

    void Start()
    {
        cam = Camera.main;
        // Define el plano horizontal (XZ)
        planoMovimiento = new Plane(Vector3.up, transform.position);
    }

    void OnMouseDrag()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (planoMovimiento.Raycast(ray, out float distancia))
        {
            Vector3 punto = ray.GetPoint(distancia);
            transform.position = punto;
        }
    }
}
