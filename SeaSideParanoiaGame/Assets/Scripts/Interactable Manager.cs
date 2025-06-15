using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;
using System.Drawing.Printing;
using Unity.VisualScripting;
using System;
using UnityEngine.EventSystems;

public class MouseInteract : GHEvtSystem.Event
{
    //public Vector3 position;
    public Ray raycast;
    public LayerMask layerMask;
        
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


    //created public function to get instance (since var is set to private)
    //able to access instance and call methods - i.e how i have it set up in interactables
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
        //tried 3 ways, one is looping through interactables and checking closest
        //the other one uses raycast.collider to get the interactable and call onInteract function
        //third is casting raycast inside function, and checking for closest interactable if more than one raycast is hit. 
        //RaycastInteractable(evt.raycastHit);
        //GettingInteractable(evt.position);
        CheckInteractable(evt.raycast, evt.layerMask);
    }
    
    public void CheckInteractable(Ray rayInteractable, LayerMask layerMask)
    {

        float maxDistance = Mathf.Infinity;
        RaycastHit[] rays = Physics.RaycastAll(rayInteractable, maxDistance, layerMask);
        Array.Sort(rays,(a, b) =>(a.distance.CompareTo(b.distance)));
       
        
        if (rays.Length == 0)
        {
            return;
        }
        closestInteractable = null;
        foreach (RaycastHit hit in rays)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            
            if (interactable != null)
            {
                //the only iffy thing about this is, since i have 2d and 3d objects in the scene
                //needed to use rayInteractable origin since i wanted the origin mouse position from when i called raycast
                
                //CHECK LATER
                float distance = Vector3.Distance(rayInteractable.origin, interactable.transform.position);
               
                if (distance < maxDistance)
                {
                    closestInteractable = interactable;
                    maxDistance = distance;
                }

            }
        }
        if (closestInteractable != null)
        { 
            closestInteractable.OnInteract();
        }
    }
}

/* 
public void GettingInteractable(Vector3 position)
    {

        float maxDistance = Mathf.Infinity;
        closestInteractable = null;
        //checking each interactable obj in the interactables list 
        foreach (Interactable interactable in interactables)
        {
            //getting the distance between the interactable objects and the mouseposition 

            float distance = Vector3.Distance(position, interactable.transform.position);
            Debug.Log($"Interactable {interactable.name},  position {interactable.transform.position}");
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
    public void RaycastInteractable(RaycastHit rayHitInteractable)
    {
        Debug.Log("trying just using the ray hit");
        rayHitInteractable.collider.GetComponent<Interactable>().OnInteract();

    }
*/