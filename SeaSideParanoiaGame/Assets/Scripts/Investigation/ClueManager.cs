using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;

public struct Connection
{
    string clueA;
    string clueB;
    bool isConnected;

    public Connection(string a, string b, bool connected)
    {
        clueA = a;
        clueB = b;
        isConnected = connected;
    }

    public bool IsInHere(string clueName)
    {
        if (clueName.Equals(clueA) || clueName.Equals(clueB))
        {
            return true;
        }

        return false;
    }

    public bool IsConnected()
    {
        return isConnected;
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
        Debug.Log(clueA + ", " + clueB + ", " + isConnected);
    }
}

public class ClueManager : Singleton<ClueManager>
{
    public string path = "Testing/SO";
    protected int count = 0;
    protected Dictionary<string, Clue> clues = new Dictionary<string, Clue>();
    protected Dictionary<string, bool> clueStatus = new Dictionary<string, bool>();
    //protected Dictionary<string, List<Clue>> connectionData = new Dictionary<string, List<Clue>>();
    protected List<Connection> connectionData = new List<Connection>();
    protected Dictionary<Clue[], bool> connectionStatus = new Dictionary<Clue[], bool>();

    // Start is called before the first frame update
    void Start()
    {
        Clue[] clue_arr = Resources.LoadAll<Clue>(path);

        foreach (Clue clue in clue_arr)
        {
            count += 1;
            clues[clue.name] = clue;
            clueStatus[clue.name] = false;
            //connectionData[clue.name] = clue.connections;
            foreach (Clue connectedClue in clue.connections)
            {
                if (!Connects(clue.name, connectedClue.name))
                {
                    connectionData.Add(
                        new Connection(clue.name, connectedClue.name, false)
                    );
                }
            }
        }

        foreach (Connection conn in connectionData)
        {
            conn.Print();
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

    public bool GetConnectionStatus(string clueName1, string clueName2)
    {
        foreach (Connection conn in connectionData)
        {
            if (conn.Equals(clueName1, clueName2))
            {
                return conn.IsConnected();
            }
        }

        return false;
    }

    // Checks if the pair of clues are connected
    public bool Connects(string clueA, string clueB)
    {
        if (!clues.ContainsKey(clueA) || !clues.ContainsKey(clueB))
        {
            Debug.Log("Either " + clueA + " or " + clueB + " is not a clue. Maybe the names are misspelled?");
            return false;
        }

        foreach (Connection con in connectionData)
        {
            if (con.Equals(clueA, clueB))
            {
                return true;
            }
        }
        return false;
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
