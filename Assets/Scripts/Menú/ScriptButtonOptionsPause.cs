
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptButtonOptionsPause : MonoBehaviour
{
    [SerializeField] private string playSceneName = "PlayScene";

    public void LoadSceneAndOpenOptions()
    {
        Debug.Log("LoadSceneAndOpenOptions called from Options button"); // Mensaje de depuración para verificar que se llama al método

        // Desactivar el canvas y el panel que corresponde al CanvasPLay al iniciar la escena
        ActivatePanelOnLoad.SetCameFromOptions();


        // Carga la escena PlayScene
        SceneManager.LoadScene(playSceneName);
    }
}