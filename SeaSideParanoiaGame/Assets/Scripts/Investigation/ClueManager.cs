using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;

public class ClueManager : Singleton<ClueManager>
{
    public string path = "Testing/SO";
    protected int count = 0;
    protected Dictionary<string, Clue> clues = new Dictionary<string, Clue>();
    protected Dictionary<string, bool> clueStatus = new Dictionary<string, bool>();
    protected Dictionary<string, List<Clue>> connectionData = new Dictionary<string, List<Clue>>();

    // Start is called before the first frame update
    void Start()
    {
        Clue[] clue_arr = Resources.LoadAll<Clue>(path);

        foreach (Clue clue in clue_arr)
        {
            count += 1;
            clues[clue.name] = clue;
            clueStatus[clue.name] = false;
            connectionData[clue.name] = clue.connections;
        }

        EventDispatcher.Instance.AddListener<FoundClue>(UpdateStatus);
    }

    public int Count()
    {
        return count;
    }

    public Clue GetClue(string clueName)
    {
        Clue target = null;
        bool ret = clues.TryGetValue(clueName, out target);
        if (ret == false)
        {
            return null;
        }
        return target;
    }

    public string GetDesciption(string clueName)
    {
        Clue target = GetClue(clueName);
        if (target == null)
        {
            return "";
        }
        return target.itemDescription;
    }

    public Sprite GetWorldSprite(string clueName)
    {
        Clue target = GetClue(clueName);
        if (target == null)
        {
            return null;
        }
        return target.worldSprite;
    }

    public Sprite GetJournalPage(string clueName)
    {
        Clue target = GetClue(clueName);
        if (target == null)
        {
            return null;
        }
        return target.journalPage;
    }

    public bool GetStatus(string clueName)
    {
        bool status = true;
        bool ret = clueStatus.TryGetValue(clueName, out status);
        if (ret == false)
        {
            // Tells game to *not* try to add clue to inventory
            return true;
        }
        return status;
    }

    public List<Clue> GetConnections(string clueName)
    {
        List<Clue> connections = new List<Clue>();
        bool ret = connectionData.TryGetValue(clueName, out connections);
        if (ret == false)
        {
            return new List<Clue>();
        }
        return connections;
    }

    void UpdateStatus(FoundClue evt)
    {
        bool status;
        bool ret = clueStatus.TryGetValue(evt.clueName, out status);
        if (ret == false)
        {
            return;
        }
        if (status == true)
        {
            return;
        }
        clueStatus[evt.clueName] = true;
    }

    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<FoundClue>(UpdateStatus);
    }
}
