using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;
using UnityEngine.InputSystem;

public class JournalUI : MonoBehaviour
{
    JournalManager journal;
    public Transform itemsParent;
    JournalPage[] itemSlots;
    CluePage[] clueSlots;
    public GameObject journalUI;

    void Start()
    {
        journal = JournalManager.instance;
        journal.onJournalChangedCallback += UpdateUI;
        journalUI.SetActive(false);

        itemSlots = GetComponentsInChildren<JournalPage>(true);
        clueSlots = GetComponentsInChildren<CluePage>(true);
     
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (journalUI.activeSelf == true)
            {
                
                EventDispatcher.Instance.RaiseEvent<StateChangeResponse>(new StateChangeResponse
                {
                    callerName = "all",
                    newState = ButtonState.NORMAL
                });
            }
           
            journalUI.SetActive(!journalUI.activeSelf);
        }


    }
    void UpdateUI()
    {
        Debug.Log("getig slots");
        Debug.Log("itemslots count" +  itemSlots.Length);
        //going through the inventory slots and adding items from the list 
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < journal.items.Count)
            {
                itemSlots[i].AddItem(journal.items[i]);
            }
            else
            {
                itemSlots[i].ClearSlot();
            }
        }
        for (int i = 0; i < clueSlots.Length; i++)
        {
            if (i < journal.items.Count)
            {
                itemSlots[i].AddItem(journal.items[i]);
            }
            else
            {
                clueSlots[i].ClearSlot();
            }
        }

        Debug.Log("Update that UI");
    }
}
