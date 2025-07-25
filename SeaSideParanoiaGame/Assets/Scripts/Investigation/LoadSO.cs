using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class LoadSO : MonoBehaviour
{
    public string path = "Testing/SO";
    Dictionary<Clue, List<Clue>> connection_data = new Dictionary<Clue, List<Clue>>();

    // Start is called before the first frame update
    void Start()
    {
        Clue[] clues = Resources.LoadAll<Clue>(path);

        foreach (Clue clue in clues)
        {
            //connection_data[clue] = clue.connections;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
