using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dummy : MonoBehaviour, IClickable, IEnterable, IExitable, IHoverable
{
    public void OnGPClick()
    {
        Debug.Log("Clicked " + gameObject.name);
    }
    public void OnGPEnter()
    {
        Debug.Log("Entered " + gameObject.name);
    }
    public void OnGPHover()
    {
        Debug.Log("Hovering " + gameObject.name);
    }
    public void OnGPExit()
    {
        Debug.Log("Exited " + gameObject.name);
    }
}
