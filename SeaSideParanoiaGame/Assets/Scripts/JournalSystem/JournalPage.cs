using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JournalPage : MonoBehaviour
{
    Clue item;


    public Image icon;

    public void AddItem(Clue newItem)
    {
        Debug.Log("Adding that item baby");
        item = newItem;
        icon.sprite = item.journalPage;
        icon.enabled = true;
    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
   
}
