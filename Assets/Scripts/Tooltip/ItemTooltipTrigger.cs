using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTooltipTrigger : MonoBehaviour {
    public string itemInfo = "Information about the item goes here.";
    private Tooltip tooltip;

    void Start() {
        tooltip = FindObjectOfType<Tooltip>();
    }

    void OnMouseEnter() {
        tooltip.ShowTooltip(itemInfo);
    }

    void OnMouseExit() {
        tooltip.HideTooltip();
    }
}
