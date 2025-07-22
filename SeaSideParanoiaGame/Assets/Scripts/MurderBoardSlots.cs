using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MurderBoardSlots : MonoBehaviour
{
    // Start is called before the first frame update
    public static MurderBoardSlots instance;
    public GameObject slotPrefab;
    public GameObject slotParent;

    void Awake()
    {
        if (instance != null)
        {


            return;
        }
        instance = this;
    }

    public void CreateSlot(Clue newClue)
    {
        GameObject newSlot = Instantiate(slotPrefab, slotParent.transform);
        newSlot.GetComponent<Image>().sprite = newClue.worldSprite;
        
    }
}
