using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour {
    public TMP_Text tooltipText;
    public GameObject tooltipPanel;
    public Camera playerCamera;

    void Update() {
    Vector3 mousePosition = Input.mousePosition;
    transform.position = mousePosition;
    }


    public void ShowTooltip(string info) {
        tooltipText.text = info;
        tooltipPanel.SetActive(true);
    }

    public void HideTooltip() {
        tooltipPanel.SetActive(false);
    }
}
