using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]                    // Evita que se a�ada m�s de una vez
public class HoverDetector : MonoBehaviour
{
    [Tooltip("Si est� activado, mostrar� mensajes de debug")]
    [SerializeField] private bool debugMode = true;

    private IInteract interactable;

    private void Awake()
    {
        TryGetComponent(out interactable);

        if (interactable == null)
        {
            interactable = gameObject.AddComponent<DefaultInteract>();
            if (debugMode)
                Debug.Log($"[HoverDetector] Se a�adi� DefaultInteract a {gameObject.name}", gameObject);
        }
    }

    private void OnMouseEnter() => interactable?.OnHoverEnter();
    private void OnMouseExit() => interactable?.OnHoverExit();
}

