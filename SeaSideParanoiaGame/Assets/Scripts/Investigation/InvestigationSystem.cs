using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;


public class Selected
{
    public Clue first = null;
    public Clue second = null;
}

public class InvestigationSystem : MonoBehaviour
{
    public string path = "Testing/SO";
    public Dictionary<Clue, List<Clue>> connection_data = new Dictionary<Clue, List<Clue>>();
    public Dictionary<Clue, bool> clue_status;

    public SpriteRenderer confirmaiton;

    private Selected currentSelection = new Selected();

    void Start()
    {
        Clue[] clues = Resources.LoadAll<Clue>(path);

        foreach (Clue clue in clues)
        {
            connection_data[clue] = clue.connections;
        }

        EventDispatcher.Instance.AddListener<SelectedPanel>(HandleSelection);
        EventDispatcher.Instance.AddListener<DeselectedPanel>(HandleDeselection);
        Debug.Log("good morning");
    }

    void HandleSelection(SelectedPanel evt)
    {
        // TODO
        if (evt.selected == null)
        {
            return;
        }

        if (currentSelection.first == null)
        {
            currentSelection.first = evt.selected;
            Debug.Log("selected " + evt.selected.name);
        }
        else if (currentSelection.second == null)
        {
            currentSelection.second = evt.selected;
            Debug.Log("selected " + evt.selected.name);
        }
        // I chose to do nothing if a third panel is selected.
        else
        {
            Debug.Log("Two panels have already been selected.");
            Debug.Log("caller: " + evt.caller.gameObject.name);
            // TODO: raise event to tell board panel not to select itself.
            EventDispatcher.Instance.RaiseEvent<ReverseSelection>(new ReverseSelection
            {
                caller = evt.caller
            });
            return;
        }

        if ((currentSelection.first != null) &&
        (currentSelection.second != null) &&
        Connects())
        {
            Debug.Log(currentSelection.first.name + " connects with " + currentSelection.second.name);
            // TODO: make string appear
            confirmaiton.color = Color.green;
        }
    }

    void HandleDeselection(DeselectedPanel evt)
    {
        if (evt.selected == null)
        {
            return;
        }

        if ((currentSelection.first != null) &&
        (currentSelection.first.name == evt.selected.name))
        {
            currentSelection.first = null;
            Debug.Log("deselected " + evt.selected.name);
        }
        else if ((currentSelection.second != null) &&
        (currentSelection.second.name == evt.selected.name))
        {
            currentSelection.second = null;
            Debug.Log("deselected " + evt.selected.name);
        }

        if (confirmaiton.color == Color.green)
        {
            confirmaiton.color = Color.white;
        }
    }

    bool Connects()
    {
        if ((connection_data[currentSelection.first].Contains(currentSelection.second)) ||
        (connection_data[currentSelection.second].Contains(currentSelection.first)))
        {
            return true;
        }
        return false;
    }

    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<SelectedPanel>(HandleSelection);
        EventDispatcher.Instance.RemoveListener<DeselectedPanel>(HandleDeselection);
    }
}
