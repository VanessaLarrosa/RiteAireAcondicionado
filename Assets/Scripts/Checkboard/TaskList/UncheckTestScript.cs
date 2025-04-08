using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UncheckTestScript : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] private Task task;
    public void OnPointerDown(PointerEventData eventData)
    {
        TaskManager.Instance.TaskUnchecker(task);
    }

}
