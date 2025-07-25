using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Clue", menuName = "Seaside Paranoia", order = 0)]
public class Clue : ScriptableObject
{
    public new string name;
    [Tooltip("DO NOT EDIT. Starts at 1.")]
    public int id = 0;
    [Tooltip("The question the clue answers. Start at one, zero is reserved for none.")]
    public int question = 0;
    public string itemDescription;
    public Sprite worldSprite;
    public Sprite journalPage;
    //public List<Clue> connections;
}
