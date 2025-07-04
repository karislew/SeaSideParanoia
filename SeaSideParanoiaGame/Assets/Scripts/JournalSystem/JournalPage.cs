using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class JournalPage : MonoBehaviour
{
    Items item;


    public Image icon;

    public void AddItem(Items newItem)
    {
        Debug.Log("Adding that item baby");
        item = newItem;
        icon.sprite = item.imageIcon;
        icon.enabled = true;
    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
   
}
