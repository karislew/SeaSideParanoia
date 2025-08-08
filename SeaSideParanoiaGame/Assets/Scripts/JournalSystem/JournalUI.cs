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

    [SerializeField] Journal journalObject;

    void Start()
    {
        journal = JournalManager.instance;
        journal.onJournalChangedCallback += UpdateUI;
        journalUI.SetActive(false);

        itemSlots = GetComponentsInChildren<JournalPage>(true);
        //clueSlots = GetComponentsInChildren<CluePage>(true);

        EventDispatcher.Instance.AddListener<ToggleJournal>(HandleToggleJournal);
    }

    void HandleToggleJournal(ToggleJournal evt)
    {
        if (journalObject != null && journalObject.rotate)
        {
            Debug.Log("I SAIDD the journal is still rotating");
            return;
        }
        if (journalUI.activeSelf)
        {
            // to deselect all board slots
            EventDispatcher.Instance.RaiseEvent<StateChangeResponse>(new StateChangeResponse
            {
                callerName = "all",
                newState = ButtonState.NORMAL
            });
        }
        
        journalUI.SetActive(!journalUI.activeSelf);
    }

    void UpdateUI()
    {
        Debug.Log("Starting to update UI");
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
        

        Debug.Log("Updated UI");
    }

    void OnDestroy() {

        EventDispatcher.Instance.RemoveListener<ToggleJournal>(HandleToggleJournal);
    }
}
/*for (int i = 0; i < clueSlots.Length; i++)
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
*/