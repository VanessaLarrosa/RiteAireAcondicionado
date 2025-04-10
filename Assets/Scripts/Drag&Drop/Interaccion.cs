using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaccion : MonoBehaviour, IInteract
{
    private float tiempoUltimoLog = -Mathf.Infinity;
    [SerializeField] private float logCooldown = 1f; // 1 segundo entre logs


    public string texto;

    public void Completed(bool value)
    {
        // throw new System.NotImplementedException();
    }

    public void GetName()
    {
        // throw new System.NotImplementedException();
    }


    public void Interact()
    {
        if (Time.time - tiempoUltimoLog >= logCooldown)
        {
            print(texto);
            tiempoUltimoLog = Time.time;
        }
    }

    public void OnHoverEnter()
    {
        // throw new System.NotImplementedException();
    }

    public void OnHoverExit()
    {
        // throw new System.NotImplementedException();
    }
}
