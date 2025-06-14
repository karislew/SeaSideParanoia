using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : Interactable
{
    public string titleNode;
    public override void onInteract()
    {
        print("This is the yarnspinner dialogue node i want to run: " + titleNode);
    }
}
