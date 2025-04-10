using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ForgotPasswordLink : MonoBehaviour
{
    public Button forgotPasswordButton;  

    void Start()
    {   Debug.Log("El script ForgotPasswordLink ha iniciado.");
        forgotPasswordButton.onClick.AddListener(OnForgotPasswordClicked);
    }

  
    void OnForgotPasswordClicked()
    {
        Debug.Log("Botón clickeado, abriendo URL...");
        Application.OpenURL("https://www.haier-europe.com/es_ES/");
    }
}

