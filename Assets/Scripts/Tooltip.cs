using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour {
    public TMP_Text tooltipText;
    public GameObject tooltipPanel;
    public Camera playerCamera;

    void Update() {
        // Posicionar el tooltip a la derecha de la cámara
        Vector3 screenPosition = playerCamera.WorldToScreenPoint(playerCamera.transform.position + playerCamera.transform.right * 200);
        transform.position = screenPosition;
    }

    public void ShowTooltip(string info) {
        tooltipText.text = info;
        tooltipPanel.SetActive(true);
    }

    public void HideTooltip() {
        tooltipPanel.SetActive(false);
    }
}
