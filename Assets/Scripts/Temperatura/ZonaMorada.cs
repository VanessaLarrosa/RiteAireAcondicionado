using UnityEngine;

public class ZonaMorada : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Debug.Log("Temperatura alta");
    }
}
