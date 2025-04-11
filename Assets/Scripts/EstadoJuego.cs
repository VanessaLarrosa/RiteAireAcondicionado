using System.Collections.Generic;
using UnityEngine;

public static class GameState
{   //Iremos agregando el booleano de cada tarea
    public static bool cartaTocada = false; // Estado de la tarea
    public static HashSet<int> tareasCompletadas = new HashSet<int>(); // Guarda las tareas completadas
}
