using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour{
    
    public CharacterController controller;
    public float velocidad;
    public float velocidadRotacion;
    public GameObject Camara;
    float xRotation;

    private void Start()
    {
        Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        controller.Move(transform.forward * z * velocidad * Time.deltaTime);
        controller.Move(transform.right * x * velocidad * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * velocidadRotacion * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * velocidadRotacion * Time.deltaTime;

        xRotation -= mouseY;
        XRotation = Mathf.Clamp(xRotation, -55 , 55f);

        Camara.transform.localRotation = Quaternion.Equals(xRotation, 0,0);
        
        transform.Rotate(Vector3.up * mouseX);

    }


}
