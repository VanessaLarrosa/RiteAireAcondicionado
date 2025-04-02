using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaccion : MonoBehaviour, IInteract
{
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
        print(texto);
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
