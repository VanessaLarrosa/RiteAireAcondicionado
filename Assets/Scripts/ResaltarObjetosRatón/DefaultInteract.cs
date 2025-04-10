using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultInteract : MonoBehaviour, IInteract
{
    [SerializeField] private bool logHoverEvents = true;

    public void OnHoverEnter()
    {
        if (logHoverEvents)
            Debug.Log($"[DefaultInteract] Ratón sobre {gameObject.name}", gameObject);
    }

    public void OnHoverExit()
    {
        if (logHoverEvents)
            Debug.Log($"[DefaultInteract] Ratón salió de {gameObject.name}", gameObject);
    }

    public void Interact() { }

    public void GetName()
    {
        Debug.Log(gameObject.name);
    }

    public void Completed(bool value)
    {
        Debug.Log($"Interacción con {gameObject.name} completada: {value}");
    }
}

