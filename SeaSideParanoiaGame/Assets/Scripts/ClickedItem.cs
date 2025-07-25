using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;

public class ClickedItem : Interactable
{
    public Clue items;
    SpriteRenderer spriteRenderer;
    protected override void Start()
    {
        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = items.worldSprite;
        }


    }
   

   
    public override void OnInteract()
    {
        if (ModeManager.Instance.GetCurrentMode() != Mode.Game)
        {
            Debug.Log("mode is not set to game, clickable item will not trigger");
            return;
        }
        //bool addedItem = Inventory.instance.AddItem(items);
        //bool addJournalClue = JournalManager.instance.AddItem(items);
        EventDispatcher.Instance.RaiseEvent<FoundClue>(new FoundClue
        {
            clueID = items.id
        });
        /*
        if (addJournalClue)
        {
            Debug.Log("I was able to add the item once");
        }
        */
        //Inventory.instance.Print();
        //print($"We CLICKED HORRAYY {this.name}");
    }
}
