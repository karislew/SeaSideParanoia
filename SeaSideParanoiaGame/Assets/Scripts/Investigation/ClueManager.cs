using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
    public string path = "Testing/SO";
    protected Dictionary<string, Clue> clues;
    protected Dictionary<Clue, bool> clueStatus;
    protected Dictionary<Clue, List<Clue>> connectionData = new Dictionary<Clue, List<Clue>>();

    // Start is called before the first frame update
    void Start()
    {
        Clue[] clue_arr = Resources.LoadAll<Clue>(path);

        foreach (Clue clue in clue_arr)
        {
            clues[clue.name] = clue;
            connectionData[clue] = clue.connections;
            clueStatus[clue] = false;
        }
    }

    public Clue GetClue(string clueName)
    {
        Clue target = null;
        clues.TryGetValue(clueName, out target);
        return target;
    }

    public bool GetStatus(string clueName)
    {
        Clue target = GetClue(clueName);
        bool status = false;
        if (target == null)
        {
            clueStatus.TryGetValue(target, out status);
            return status;
        }
        // This will signal to game to *not* try to add clue to inventory
        return true;
    }
    
    public List<Clue> GetConnections(string clueName)
    {
        Clue target = GetClue(clueName);
        List<Clue> connections = new List<Clue>();
        if (target == null)
        {
            connectionData.TryGetValue(target, out connections);
            return connections;
        }
        return new List<Clue>();
    }
}
