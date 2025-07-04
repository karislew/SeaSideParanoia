using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clue", menuName = "Test", order = 0)]
public class Clue : ScriptableObject
{
    public new string name;
    public Sprite journal_page;
    public List<Clue> connections;
}
