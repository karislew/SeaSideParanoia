using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using GHEvtSystem;

public class ClueBoardSlot : MonoBehaviour, IDropHandler
{
    public Clue correctClue;

    // Start is called before the first frame update
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DragItem draggedItem = dropped.GetComponent<DragItem>();
            draggedItem.parentAfterDrag = transform;
            if (correctClue == draggedItem.clue){
                GHEvtSystem.EventDispatcher.Instance.RaiseEvent<SlotUpdate>(new SlotUpdate
                {
                    clueID = draggedItem.clue.id,
                    unGuessing = false
                });
            } else {
                GHEvtSystem.EventDispatcher.Instance.RaiseEvent<SlotUpdate>(new SlotUpdate
                {
                    clueID = draggedItem.clue.id,
                    unGuessing = true
                });
            }
        }
        
    }
}
