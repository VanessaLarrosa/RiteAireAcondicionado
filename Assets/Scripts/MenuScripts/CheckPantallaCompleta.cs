using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CheckPantallaCompleta : MonoBehaviour
{
    public Toggle toggle;

    public TMP_Dropdown resolucionDropDown;
    Resolution[] resolucion;
    // Start is called before the first frame update
    void Start()
    {
        if(Screen.fullScreen){
            toggle.isOn = true;
        }else{
            toggle.isOn = false;
        }
        
        RevisarResolucion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcctivarPantallaompleta(bool pantallacompleta){
        Screen.fullScreen = pantallacompleta;
    }

    public void RevisarResolucion(){
        resolucion = Screen.resolutions;
        resolucionDropDown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for(int i= 0 ; i<resolucion.Length; i++){
            string opcion = resolucion[i].width + " x " + resolucion[i].height;
            opciones.Add(opcion);

            if (Screen.fullScreen && resolucion[i].width == Screen.currentResolution.width && resolucion[i].height == Screen.currentResolution.height){
                resolucionActual = i;
            }
        }

        resolucionDropDown.AddOptions(opciones);
        resolucionDropDown.value = resolucionActual;
        resolucionDropDown.RefreshShownValue();

    }

    public void CambiarResolucion(int indiceResolucion){

        Resolution resoluciones = resolucion[indiceResolucion];
        Screen.SetResolution(resoluciones.width, resoluciones.height, Screen.fullScreen);
    }
}
