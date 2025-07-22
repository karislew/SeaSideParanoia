using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hint : MonoBehaviour
{
   // Start is called before the first frame update
   


   private void OnMouseEnter()
   {
       
       HintManager.instance.ShowHint();


   }
   private void OnMouseExit()
   {
       HintManager.instance.HideHint();
   }


}
