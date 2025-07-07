using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Clue", menuName = "Test", order = 0)]
public class Clue : ScriptableObject
{
    public new string name;
    public string itemDescription;
    public Sprite worldSprite;
    public Sprite journalPage;
    public List<Clue> connections;
}
