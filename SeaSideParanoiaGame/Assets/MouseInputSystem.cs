using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;




public class MouseInputSystem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
   public void OnPointerEnter(PointerEventData eventData)
   {
       print($"New Input System: ON Mouse Enter Called On {this.name}!");
   }
   public void OnPointerExit(PointerEventData eventData)
   {
       print($"New Input System: ON Mouse Exit Called On {this.name}!");
   }


   public void OnPointerDown(PointerEventData eventData)
   {
       if (eventData.button == PointerEventData.InputButton.Left)
       {
           print("Left Button was pressed");
       }
       else
       {
           print("Another button was pressed");
       }
   }
   public void OnPointerUp(PointerEventData eventData)
   {
       print("we up");
   }
    public void OnPointerClick(PointerEventData eventData)
   {
       print("we clicl");
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




