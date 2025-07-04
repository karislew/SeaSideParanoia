using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    public List<Items> items = new List<Items>();
    public static JournalManager instance;
    public delegate void onUpdateJournal();
    
    public int inventorySpace;
    public onUpdateJournal onJournalChangedCallback;
    public delegate void onJournalClicked(Items item);
    public onJournalClicked onJournalClickedCallback;
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
        

        items.Add(newItem);
        Debug.Log("added item to list");
        newItem.hasItem = true;
        //updating the UI 
        if (onJournalChangedCallback != null)
        {
            Debug.Log("performing callback");
            onJournalChangedCallback.Invoke();

        }

        

        return true;

    }
   
    
    public void Print()
    {
        foreach (var i in items)
        {
            Debug.Log("Item: " + i.itemName);
        }
    }
}
