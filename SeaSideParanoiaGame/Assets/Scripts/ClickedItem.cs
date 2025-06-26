using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedItem : Interactable
{
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public bool hasItem;
   

   
    public override void OnInteract()
    {
        bool addedItem = Inventory.instance.AddItem(new Items(itemName, itemDescription, itemIcon, hasItem));
        if (addedItem)
        {
            Debug.Log("I was able to add the item once");
        }
        //Inventory.instance.Print();
        //print($"We CLICKED HORRAYY {this.name}");
    }
}
