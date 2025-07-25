using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;

public class JournalManager : MonoBehaviour
{
    public List<Clue> items = new List<Clue>();
    public static JournalManager instance;
    public delegate void onUpdateJournal();

    public int inventorySpace;
    public onUpdateJournal onJournalChangedCallback;
    public delegate void onJournalClicked(Clue item);
    public onJournalClicked onJournalClickedCallback;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of inventory");
        }
        instance = this;
    }

    void Start()
    {
        EventDispatcher.Instance.AddListener<FoundClue>(AddClue);
    }

    public void AddClue(FoundClue evt)
    {
        Clue clue = ClueManager.Instance.GetClue(evt.clueName);
        if (clue == null)
        {
            Debug.Log("\"" + evt.clueName + "\" does not exit.");
            return;
        }
        
        if (items.Contains(clue))
        {
            Debug.Log("\"" + evt.clueName + "\" has already been found.");
            return;
        }

        items.Add(clue);
        //MurderBoardSlots.instance.CreateSlot(clue);
        Debug.Log("Found \"" + evt.clueName + "\".");

        if (onJournalChangedCallback != null)
        {
            Debug.Log("performing callback");
            onJournalChangedCallback.Invoke();
        }
    }

    /*
    public bool AddItem(Clue newItem)
    {
        //want to have a better way of checking if item is in list 
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name == newItem.name)
            {

                return false;

            }
        }


        items.Add(newItem);
        Debug.Log("added item to list");
        //newItem.hasItem = true;
        //updating the UI 
        if (onJournalChangedCallback != null)
        {
            Debug.Log("performing callback");
            onJournalChangedCallback.Invoke();

        }



        return true;

    }
    */

    public void Print()
    {
        foreach (var i in items)
        {
            Debug.Log("Item: " + i.name);
        }
    }
    
    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<FoundClue>(AddClue);
    }
}
