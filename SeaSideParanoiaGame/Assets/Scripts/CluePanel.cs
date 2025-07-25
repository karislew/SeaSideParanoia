using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using GHEvtSystem;

public class CluePanel : MonoBehaviour, IDropHandler
{
    public Transform trueParent;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DragItem draggedItem = dropped.GetComponent<DragItem>();
        draggedItem.parentAfterDrag = trueParent;
        GHEvtSystem.EventDispatcher.Instance.RaiseEvent<SlotUpdate>(new SlotUpdate
        {
            clueID = draggedItem.clue.id,
            unGuessing = true
        });
    }
}
