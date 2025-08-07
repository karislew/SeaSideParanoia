using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GHEvtSystem;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public Transform parentAfterDrag;
  
    public Clue clue;
    public bool canDrag = true;

    void Awake()
    {
        EventDispatcher.Instance.AddListener<GPStartDrag>(OnGPStartDrag);
        EventDispatcher.Instance.AddListener<GPDrag>(OnGPDrag);
        EventDispatcher.Instance.AddListener<GPEndDrag>(OnGPEndDrag);
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDrag();
    }
    public void OnDrag(PointerEventData eventData)
    {
        Drag();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        EndDrag();
    }

    public void OnGPStartDrag(GPStartDrag eventData)
    {
        BeginDrag();
    }
    public void OnGPDrag(GPDrag eventData)
    {
        Drag();
    }
    public void OnGPEndDrag(GPEndDrag eventData)
    {
        EndDrag();
    }

    void BeginDrag()
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    void Drag()
    {
        transform.position = ControllerCursor.Instance.GetPosition();
    }
    void EndDrag()
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<GPStartDrag>(OnGPStartDrag);
        EventDispatcher.Instance.RemoveListener<GPDrag>(OnGPDrag);
        EventDispatcher.Instance.RemoveListener<GPEndDrag>(OnGPEndDrag);
    }
}
