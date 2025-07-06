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

    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<FoundClue>(ShowClue);
        EventDispatcher.Instance.RemoveListener<StateChangeRequest>(HandleStateChangeRequest);
        EventDispatcher.Instance.RemoveListener<StateChangeResponse>(HandleStateChangeResponse);
    }
}
