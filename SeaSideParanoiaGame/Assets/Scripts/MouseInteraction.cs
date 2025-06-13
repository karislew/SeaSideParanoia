using System.Collections;
using System.Collections.Generic;
using GHEvtSystem;
using UnityEngine;
using UnityEngine.EventSystems;




public class MouseInputSystem : MonoBehaviour
{
   
    public void OnMouseDown()
    {
        MouseInteract mouseInteract = new MouseInteract()
        {
            MouseClicked = true
        };
        EventDispatcher.Instance.RaiseEvent<MouseInteract>(mouseInteract);
        print($"we down fr {this.name}");
    }
    public void OnMouseEnter()
    {
       
    }
    public void OnMouseExit()
    {
        
        
    }


    // Start is called before the first frame update
    void Start()
   {

   }


   // Update is called once per frame
   void Update()
   {


   }


 
}



