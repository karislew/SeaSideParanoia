using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GHEvtSystem;


public class InvestigationSystem : MonoBehaviour
{
    public Image confirmation;
    public string first;
    public string second;
    public List<BoardSlot> slots = new List<BoardSlot>();

    public GameObject boardParent;
    public Image lineImage;
    private RectTransform firstPosition = null;
    private RectTransform secondPosition = null;

    void Start()
    {
        EventDispatcher.Instance.AddListener<FoundClue>(ShowClue);
        EventDispatcher.Instance.AddListener<StateChangeRequest>(HandleStateChangeRequest);
        EventDispatcher.Instance.AddListener<StateChangeResponse>(HandleStateChangeResponse);
        Debug.Log("good morning");
    }

    void HandleStateChangeRequest(StateChangeRequest evt)
    {
        switch (evt.currentState)
        {
            case ButtonState.DISABLED:
                return;
            case ButtonState.NORMAL:
                if (string.IsNullOrEmpty(first))
                {
                    first = evt.clueName;
                    Debug.Log("selected " + evt.clueName);
                    EventDispatcher.Instance.RaiseEvent<StateChangeResponse>(new StateChangeResponse
                    {
                        callerName = evt.callerName,
                        newState = ButtonState.SELECTED
                    });
                    firstPosition = FindSlotByName(evt.callerName);
                    if (firstPosition != null)
                    {
                        Debug.Log(first + " is at position: " + firstPosition.anchoredPosition);
                    }else{
                        Debug.Log("rect transform is null");
                    }
                }
                else if (string.IsNullOrEmpty(second))
                {
                    second = evt.clueName;
                    Debug.Log("selected " + evt.clueName);
                    EventDispatcher.Instance.RaiseEvent<StateChangeResponse>(new StateChangeResponse
                    {
                        callerName = evt.callerName,
                        newState = ButtonState.SELECTED
                    });
                    secondPosition = FindSlotByName(evt.callerName);
                    if (secondPosition != null)
                    {
                        Debug.Log(second + " is at position: " + secondPosition.anchoredPosition);
                    }else{
                        Debug.Log("rect transform is null");
                    }
                }
                else
                {
                    Debug.Log("Two panels have already been selected.");
                }
                break;
            case ButtonState.SELECTED:
                if (first.Equals(evt.clueName))
                {
                    first = "";
                    Debug.Log("deselected " + evt.clueName);
                    EventDispatcher.Instance.RaiseEvent<StateChangeResponse>(new StateChangeResponse
                    {
                        callerName = evt.callerName,
                        newState = ButtonState.NORMAL
                    });
                }
                else if (second.Equals(evt.clueName))
                {
                    second = "";
                    Debug.Log("deselected " + evt.clueName);
                    EventDispatcher.Instance.RaiseEvent<StateChangeResponse>(new StateChangeResponse
                    {
                        callerName = evt.callerName,
                        newState = ButtonState.NORMAL
                    });
                }
                else
                {
                    Debug.Log("WARNING: \"" + evt.clueName + "\" is not selected but has state selected.");
                }
                break;
        }
        
        if (!string.IsNullOrEmpty(first) &&
        !string.IsNullOrEmpty(second) &&
        Connects())
        {
            Debug.Log(first + " connects with " + second);
            // TODO: do confirmation
            if (lineImage == null)
            {
                return;
            }
            // do distance calculation
            float xOffset = Mathf.Min(Mathf.Abs(firstPosition.anchoredPosition.x), Mathf.Abs(secondPosition.anchoredPosition.x));
            float yOffset = Mathf.Min(Mathf.Abs(firstPosition.anchoredPosition.y), Mathf.Abs(secondPosition.anchoredPosition.y));
            float dist = Mathf.Sqrt(Mathf.Pow((firstPosition.anchoredPosition.x - secondPosition.anchoredPosition.x), 2)
            + Mathf.Pow((firstPosition.anchoredPosition.y - secondPosition.anchoredPosition.y), 2));
            float distX = Mathf.Abs(firstPosition.anchoredPosition.x - secondPosition.anchoredPosition.x);
            float distY = Mathf.Abs(firstPosition.anchoredPosition.y - secondPosition.anchoredPosition.y);
            // duplicate line image
            Image newLine = Instantiate(lineImage);
            newLine.gameObject.transform.SetParent(boardParent.transform);
            // set new line image position to 1/2 distance
            newLine.rectTransform.localScale = new Vector3(1f, 1f, 1f);
            newLine.rectTransform.anchoredPosition3D = new Vector3((xOffset + distX/2f), (yOffset + distY/2f), 22f);
            // set new line image height to distance
            newLine.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dist);
            // set rotatation to arcsin(dx/d)
            newLine.rectTransform.Rotate(new Vector3(0f, 0f, (Mathf.Asin(distX / dist))*(180/Mathf.PI)));
        }
    }

    void HandleStateChangeResponse(StateChangeResponse evt)
    {
        if (!evt.callerName.Equals("all"))
        {
            return;
        }

        first = "";
        second = "";
    }

    bool Connects()
    {
        Clue firstClue = ClueManager.Instance.GetClue(first);
        Clue secondClue = ClueManager.Instance.GetClue(second);
        if ((firstClue == null) || (secondClue == null))
        {
            return false;
        }
        List<Clue> firstConnections = ClueManager.Instance.GetConnections(first);
        List<Clue> secondConnections = ClueManager.Instance.GetConnections(second);
        if ((firstConnections.Count == 0) || (secondConnections.Count == 0)) {
            return false;
        }

        if (firstConnections.Contains(secondClue) ||
        secondConnections.Contains(firstClue))
        {
            return true;
        }
        return false;
    }

    void ShowClue(FoundClue evt)
    {
        if (ClueManager.Instance.GetStatus(evt.clueName))
        {
            Debug.Log("\"" + evt.clueName + "\" either doesn't exist or is already displayed.");
            return;
        }

        foreach (BoardSlot slot in slots)
        {
            if (string.IsNullOrEmpty(slot.clueName))
            {
                Clue clue = ClueManager.Instance.GetClue(evt.clueName);
                if (clue == null)
                {
                    Debug.Log("\"" + evt.clueName + "\" doesn't exist.");
                    return;
                }
                Debug.Log("storing \"" + evt.clueName + "\" in a slot...");
                slot.ShowClue(clue);
                return;
            }
        }

        Debug.Log("no more empty slots.");
    }

    RectTransform FindSlotByName(string target)
    {
        foreach(BoardSlot slot in slots)
        {
            if (slot.gameObject.name.Equals(target))
            {
                return slot.gameObject.GetComponent<RectTransform>();
            }
        }
        return null;
    }

    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<FoundClue>(ShowClue);
        EventDispatcher.Instance.RemoveListener<StateChangeRequest>(HandleStateChangeRequest);
        EventDispatcher.Instance.RemoveListener<StateChangeResponse>(HandleStateChangeResponse);
    }
}
