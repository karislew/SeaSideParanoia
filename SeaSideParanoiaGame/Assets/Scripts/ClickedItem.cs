using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;

public class ClickedItem : Interactable
{
    public Clue items;
   

   
    public override void OnInteract()
    {
        //bool addedItem = Inventory.instance.AddItem(items);
        //bool addJournalClue = JournalManager.instance.AddItem(items);
        EventDispatcher.Instance.RaiseEvent<FoundClue>(new FoundClue
        {
            clueName = items.name
        });
        /*
        if (addJournalClue)
        {
            Debug.Log("I was able to add the item once");
        }
        */
        //Inventory.instance.Print();
        //print($"We CLICKED HORRAYY {this.name}");
    }
}
