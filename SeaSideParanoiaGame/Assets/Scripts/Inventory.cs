using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Items> items = new List<Items>();
    public static Inventory instance;
    public delegate void onItemChanged();
    public int inventorySpace;
    public onItemChanged onItemChangedCallback;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of inventory");
        }
        instance = this;
    }
    public bool AddItem(Items newItem)
    {
        //want to have a better way of checking if item is in list 
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == newItem.itemName)
            {

                return false;

            }
        }
        if (items.Count >= inventorySpace)
        {
            
            items.Add(newItem);
            newItem.hasItem = true;
            //updating the UI 
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();

            }

        }
        
        return true;
        
    }
    public void RemoveItem(Items newItem)
    {
        //updating the UI
        items.Remove(newItem);
        if (onItemChangedCallback != null)
        { 
            onItemChangedCallback.Invoke();
            
        }
    }
    public void Print()
    {
        foreach (var i in items)
        {
            Debug.Log("Item: " + i.itemName);
        }
    }
}
