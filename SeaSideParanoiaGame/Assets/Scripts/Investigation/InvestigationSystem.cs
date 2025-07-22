using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GHEvtSystem;


public class ConnectedSlots
{
    string clueA;
    string clueB;
    public Image line;
    bool isNull;

    public ConnectedSlots(string a, string b, Image l)
    {
        clueA = a;
        clueB = b;
        line = l;
        isNull = false;
    }

    public bool Equals(string a, string b)
    {
        if ((clueA.Equals(a) && clueB.Equals(b)) ||
        (clueA.Equals(b) && clueB.Equals(a)))
        {
            return true;
        }

        return false;
    }

    public void Print()
    {
        Debug.Log(clueA + ", " + clueB + ", " + line.name);
    }
}


public class InvestigationSystem : MonoBehaviour
{
    public Image confirmation;
    public List<BoardSlot> slots = new List<BoardSlot>();
    public GameObject boardParent;
    public Image lineImage;
    public List<ConnectedSlots> currentlyConnected = new List<ConnectedSlots>();

    private string first;
    private RectTransform firstPosition = null;
    private BoardSlot firstSlot = null;
    private string second;
    private RectTransform secondPosition = null;
    private BoardSlot secondSlot = null;

    public List<BoardSlot[]> connectedSlots = new List<BoardSlot[]>();

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
                    firstSlot = FindSlotByName(evt.callerName);
                    firstPosition = firstSlot.gameObject.GetComponent<RectTransform>();
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
                    secondSlot = FindSlotByName(evt.callerName);
                    secondPosition = secondSlot.gameObject.GetComponent<RectTransform>();
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
                    firstPosition = null;
                    firstSlot = null;
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
                    secondPosition = null;
                    secondSlot = null;
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
        !string.IsNullOrEmpty(second))
        {
            ConnectedSlots conn = AlreadyConnected();

            if (conn != null)
            {
                // TODO: destroy line and remove from list
                Destroy(conn.line);
                currentlyConnected.Remove(conn);
            } else {
                Debug.Log(first + " and " + second + " connect!");
                BoardSlot[] pair = {firstSlot, secondSlot};
                connectedSlots.Add(pair);

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
        
        /*
        if (!string.IsNullOrEmpty(first) &&
        !string.IsNullOrEmpty(second) &&
        !ClueManager.Instance.GetConnectionStatus(first, second) &&
        ClueManager.Instance.Connects(first, second))
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
        */
    }

    void HandleStateChangeResponse(StateChangeResponse evt)
    {
        if (!evt.callerName.Equals("all"))
        {
            return;
        }

        first = "";
        second = "";
        firstPosition = null;
        secondPosition = null;
        firstSlot = null;
        secondSlot = null;
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

    BoardSlot FindSlotByName(string target)
    {
        foreach(BoardSlot slot in slots)
        {
            if (slot.gameObject.name.Equals(target))
            {
                return slot;
            }
        }
        return null;
    }

    ConnectedSlots AlreadyConnected()
    {
        foreach (ConnectedSlots conn in currentlyConnected)
        {
            if (conn.Equals(first, second))
            {
                return conn;
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
