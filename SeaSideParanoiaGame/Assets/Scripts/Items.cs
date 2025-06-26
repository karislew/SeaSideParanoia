using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Items
{
    public string itemName;
    public Sprite imageIcon;
    [TextArea]
    public string itemDescription;
    public bool hasItem = false;

    public Items(string name, string description, Sprite icon, bool itemBool)
    {
        itemName = name;
        itemDescription = description;
        imageIcon = icon;
        hasItem = itemBool;
    
    }

}

