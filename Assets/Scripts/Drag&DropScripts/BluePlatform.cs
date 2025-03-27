using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlatform : MonoBehaviour, IPlatform
{
    public void OnBallCollision(IBall ball)
    {
        Debug.Log("La bola '" + ball.GetName() + "' se encuentra en la Plataforma Azul.");
    }

}
