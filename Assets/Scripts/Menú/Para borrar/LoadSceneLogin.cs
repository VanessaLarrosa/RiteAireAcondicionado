
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneLogin : MonoBehaviour
{
    [SerializeField] private string sceneName = "LoginScene"; //Nombre de la escena a cargar

    public void LoadLogin() //Método para cargar la escena
    {
        SceneManager.LoadScene(sceneName);
    }
}
