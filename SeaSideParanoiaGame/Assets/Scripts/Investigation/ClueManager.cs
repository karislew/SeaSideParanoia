using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;


public class ClueManager : Singleton<ClueManager>
{
    public string path = "Testing/SO";
    protected int count = 0;
    protected Dictionary<int, Clue> clues = new Dictionary<int, Clue>();
    protected Dictionary<int, bool> clueStatus = new Dictionary<int, bool>();
    protected Dictionary<Clue[], bool> connectionStatus = new Dictionary<Clue[], bool>();

    protected List<int> question1 = new List<int>();
    protected List<int> question2 = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        Clue[] clue_arr = Resources.LoadAll<Clue>(path);

        foreach (Clue clue in clue_arr)
        {
            count += 1;
            clues[clue.id] = clue;
            clueStatus[clue.id] = false;
            switch (clue.question)
            {
                case 1:
                    question1.Add(clue.id);
                    break;
                case 2:
                    question2.Add(clue.id);
                    break;
                default:
                    break;
            }
        }

        Debug.Log("--- Question One Asnwers ---");
        foreach (int ans in question1)
        {
            Debug.Log(ans);
        }
        Debug.Log("--- Question Two Asnwers ---");
        foreach (int ans in question2)
        {
            Debug.Log(ans);
        }

        EventDispatcher.Instance.AddListener<FoundClue>(UpdateStatus);
    }

    public int Count()
    {
        return count;
    }

    public Clue GetClue(int id)
    {
        Clue target = null;
        bool ret = clues.TryGetValue(id, out target);
        if (ret == false)
        {
            return null;
        }
        return target;
    }

    public int GetID(string clueName)
    {
        foreach (KeyValuePair<int, Clue> entry in clues)
        {
            if (entry.Value.name.Equals(clueName))
            {
                return entry.Key;
            }
        }

        return 0;
    }

    public int GetQuestion(int id)
    {
        Clue target = GetClue(id);
        if (target == null)
        {
            return -1;
        }
        return target.question;
    }

    public string GetDesciption(int id)
    {
        Clue target = GetClue(id);
        if (target == null)
        {
            return "";
        }
        return target.itemDescription;
    }

    public Sprite GetWorldSprite(int id)
    {
        Clue target = GetClue(id);
        if (target == null)
        {
            return null;
        }
        return target.worldSprite;
    }

    public Sprite GetJournalPage(int id)
    {
        Clue target = GetClue(id);
        if (target == null)
        {
            return null;
        }
        return target.journalPage;
    }

    public bool GetStatus(int id)
    {
        bool status = true;
        bool ret = clueStatus.TryGetValue(id, out status);
        if (ret == false)
        {
            // Tells game to *not* try to add clue to inventory
            return true;
        }
        return status;
    }

    public List<int> GetTrueAnswer(int question)
    {
        switch (question)
        {
            case 1:
                return question1;
            case 2:
                return question2;
        }

        return new List<int>();
    }

    void UpdateStatus(FoundClue evt)
    {
        bool status;
        bool ret = clueStatus.TryGetValue(evt.clueID, out status);
        if (ret == false)
        {
            return;
        }
        if (status == true)
        {
            return;
        }
        clueStatus[evt.clueID] = true;
    }

    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<FoundClue>(UpdateStatus);
    }
}
