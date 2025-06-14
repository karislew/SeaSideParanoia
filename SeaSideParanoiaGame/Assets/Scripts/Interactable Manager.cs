using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;
using System.Drawing.Printing;
public class MouseInteract : GHEvtSystem.Event
    {
        public GameObject clickedObject;
    }

public class InteractableManager : MonoBehaviour
{
    private static InteractableManager instance;
    private InteractableManager()
    {
        EventDispatcher.Instance.AddListener<MouseInteract>(Testing);
    }
    static public InteractableManager GetInstance()
    {
        if (instance == null)
        {
            instance = new InteractableManager();
        }
        return instance;
    }
    public void Testing(MouseInteract evt)
    {
        print("WORK PLEASE");
        Debug.Log("Is this thing working");
    }
}

