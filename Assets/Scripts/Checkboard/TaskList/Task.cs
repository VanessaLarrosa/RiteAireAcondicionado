using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Task", menuName = "Task")]
public class Task : ScriptableObject
{
    [SerializeField] private string summary;
    [SerializeField] private bool check;
    private int page;

    public string Summary
    {
        get { return summary; }
        set { summary = value; }
    }
    public bool Check
    {
        get { return check; } 
        set { check = value; }
    }
    public int Page
    {
        get { return page; }
        set { page = value; }
    }
}
