using UnityEngine;
using UnityEngine.SceneManagement;


public class LoginManager : MonoBehaviour
{
    [SerializeField] private GameObject Login; //Panel de Inicio de Sesi�n
    [SerializeField] private GameObject Register; //Panel de Registro
    [SerializeField] private GameObject MenuPrincipal; //Panel de Men� Principal
    [SerializeField] private GameObject MenuCancel; //Panel de Cancelar Operaci�n

    public void ActivarRegistro() //M�todo para activar el panel de Registro
    {
        Login.SetActive(false);
        Register.SetActive(true);
       
    }

     [Header("Panels")]
    public GameObject loginPanel;
    public GameObject registerPanel;
   
     public void OpnePanel(GameObject panel){
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);

        panel.SetActive(true);
    }

    public void ActivarLogin() //M�todo para activar el panel de Inicio de Sesi�n
    {
        Login.SetActive(true);
        Register.SetActive(false);
    }

    public void IniciarSesion() //M�todo para cargar la escena de Men� Principal (falta la l�gica de verificaci�n de usuario)
    {
        SceneManager.LoadScene("MainMenu");
    }
   
    public void CancelarOperacion() //M�todo para cargar la escena de Cancelar Operaci�n
    {
        SceneManager.LoadScene("MenuCancel");
    }
}
