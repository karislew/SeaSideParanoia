using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;


public class Selected1
{
    public Clue first = null;
    public Clue second = null;
}

public class InvestigationSystem1 : MonoBehaviour
{
    public string path = "Testing/SO";
    public Dictionary<Clue, List<Clue>> connection_data = new Dictionary<Clue, List<Clue>>();
    public Dictionary<Clue, bool> clue_status;

    public SpriteRenderer confirmaiton;

    private Selected1 currentSelection = new Selected1();

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
       
    }

    void HandleDeselection(DeselectedPanel evt)
    {
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
