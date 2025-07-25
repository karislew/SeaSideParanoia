using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GHEvtSystem;

public class MurderBoardManager : MonoBehaviour
{
    public GameObject murderBoard;

    // Start is called before the first frame update
    void Start()
    {
        EventDispatcher.Instance.AddListener<ToggleMurderBoard>(HandleMurderBoardToggle);
    }
    
    void HandleMurderBoardToggle(ToggleMurderBoard evt)
    {
        if (murderBoard.activeSelf)
        {
            // already on, so turn off
            murderBoard.SetActive(false);
        } else {
            // already off, so turn on
            murderBoard.SetActive(true);
        }
    }

    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<ToggleMurderBoard>(HandleMurderBoardToggle);
    }
}
