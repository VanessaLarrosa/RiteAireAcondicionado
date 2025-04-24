using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaOpciones : MonoBehaviour
{

    public ControladorDeOpciones panelOpciones;

    // Start is called before the first frame update
    void Start()
    {
        panelOpciones = GameObject.FindGameObjectWithTag("opciones").GetComponent<ControladorDeOpciones>();

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void MostrarOpciones(){
        panelOpciones.pantallaOpciones.SetActive(true);
    }
}
