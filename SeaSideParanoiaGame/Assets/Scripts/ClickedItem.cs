using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedItem : Interactable
{
    public Items items;
   

   
    public override void OnInteract()
    {
        //bool addedItem = Inventory.instance.AddItem(items);
        bool addJournalClue = JournalManager.instance.AddItem(items);
        if (addJournalClue)
        {
            Debug.Log("I was able to add the item once");
        }
        //Inventory.instance.Print();
        //print($"We CLICKED HORRAYY {this.name}");
    }
}
