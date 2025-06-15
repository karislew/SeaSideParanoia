using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;
using System.Drawing.Printing;
using Unity.VisualScripting;
using System;

public class MouseInteract : GHEvtSystem.Event
    {
        public Vector3 position;
    }
public class InteractableManager 
{
    private static InteractableManager instance;
    private Interactable closestInteractable = null;
    private InteractableManager()
    {
        EventDispatcher.Instance.AddListener<MouseInteract>(Testing);
    }
    private List<Interactable> interactables = new List<Interactable>();
    static public InteractableManager GetInstance()
    {
        if (instance == null)
        {
            instance = new InteractableManager();
        }
        return instance;
    }
    public void AddInteractable(Interactable interactable_obj)
    { 
        interactables.Add(interactable_obj);
    }
    public void RemoveInteractable(Interactable interactable_obj)
    { 
        interactables.Remove(interactable_obj);
    }
    public void Testing(MouseInteract evt)
    {

        GettingInteractable(evt.position);
    }
    public void GettingInteractable(Vector3 position)
    {
        
        float maxDistance = Mathf.Infinity;
        closestInteractable = null;
       //checking each interactable obj in the interactables list 
        foreach (Interactable interactable in interactables)
        {
            //getting the distance between the interactable objects and the mouseposition 
          
            float distance = Vector3.Distance(position, interactable.transform.position);
            //if its lower than the maxdistance
            //set the new distance and make that object associated the closest Interactable
            if (distance < maxDistance)
            {
                maxDistance = distance;
                closestInteractable = interactable;
            }
        }
        //call that interactable objects OnInteract function
        if (closestInteractable != null)
        {
            closestInteractable.OnInteract();
        }
        return;
    }
}

