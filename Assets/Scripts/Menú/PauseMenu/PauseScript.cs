
using UnityEngine;
using UnityEngine.SceneManagement; // Importa la biblioteca para gestionar escenas

public class PauseScript : MonoBehaviour
{
    [SerializeField] GameObject ObjectPauseMenu;
    [SerializeField] bool Pause = false;
    [SerializeField] GameObject ExitMenu;


    // Update is called once per frame
    void Update()
    {
        // Función que comprueba si la escena está en pausa y si no lo está, la pausa se activa. Y el menú se pausa se activa con la tecla Escape.

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pause == false)
            {
                ObjectPauseMenu.SetActive(true);
                ExitMenu.SetActive(false); // Corroborar que el menú de salir esté oculto al pausar
                Pause = true;

                Time.timeScale = 0f; // Pausa la escena
                Cursor.visible = true; // Muestra el cursor al pausar la escena
                Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor al pausar la escena

            }
            else if (Pause == true) //Si la escena está en pausa y se vuelve a pulsar la tecla Escape, se reanuda la escena y se oculta el menú de pausa.
            {
                Resume();

            }

        }

    }
    //Función que muestra el menú de Pause y esconde el mení de Exit. Funciona igual que la función anterior, pero se va a llamar desde un botón y tener acceso por dos zonas.
    public void ShowPauseMenuButton()
    {
        if (Pause == false)
        {
            ObjectPauseMenu.SetActive(true);
            ExitMenu.SetActive(false); // Corroborar que el menú de salir esté oculto al pausar
            Pause = true;
            Time.timeScale = 0f; // Pausa la escena
            Cursor.visible = true; // Muestra el cursor
            Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
        }
    }


    public void ShowExitMenu() // Función que muestra el menú de salir y esconde el menú de pausa
    {
        ObjectPauseMenu.SetActive(false); // Desactivamos el menú de pausa principal
        ExitMenu.SetActive(true);        // Activamos el menú de Exit
    }

    public void ResumeFromExit() // Función que muestra el menú de pausa y esconde el menú de salir
    {
        ExitMenu.SetActive(false);        // Desactivamos el menú de Exit
        ObjectPauseMenu.SetActive(true); // Volvemos a activar el menú de pausa principal
        Time.timeScale = 0f;             // Aseguramos que el juego siga pausado
    }

    public void Resume()
    {
        ObjectPauseMenu.SetActive(false);
        ExitMenu.SetActive(false); // Desactiva el menú de pausa
        Pause = false;

        Time.timeScale = 1f; // Reanuda la escena
        Cursor.visible = true; // Muestra el cursor al reanudar la escena porque en nuestro caso el cursor no se oculta
        Cursor.lockState = CursorLockMode.None; // Asegúrate de que el cursor no esté bloqueado

    }

    public void GoMainMenu(string NombreMenu)
    {
        SceneManager.LoadScene(NombreMenu); // Carga la escena del menú principal

    }

    public void ExitGame()
    {
        Application.Quit(); // Cierra la aplicación
        Debug.Log("Saliendo del juego..."); // Muestra un mensaje en la consola de que se está saliendo de la aplaicación
    }
}
