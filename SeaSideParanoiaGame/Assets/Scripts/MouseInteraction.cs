using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;




public class MouseInputSystem : MonoBehaviour
{
   
    public void OnMouseDown()
    {
          print("we down fr");
    }
    public void OnMouseEnter()
    {
        print($"ON Mouse Enter {this.name}!");
    }


    // Start is called before the first frame update
    void Start()
   {
       print("is this workign");
   }


   // Update is called once per frame
   void Update()
   {


   }


 
}



