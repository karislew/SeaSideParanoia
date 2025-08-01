using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GHEvtSystem;

public class MurderBoardManager : MonoBehaviour
{
    public GameObject murderBoard;
    public GameObject settings;

    // Start is called before the first frame update
    void Start()
    {
        EventDispatcher.Instance.AddListener<ToggleMurderBoard>(HandleMurderBoardToggle);
        EventDispatcher.Instance.AddListener<ToggleSettings>(HandleSettingsToggle);
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
        
        // TODO: remove, was only for testing :)
        AudioManager.Instance.Play("Sound01");
    }

    void HandleSettingsToggle(ToggleSettings evt)
    {
        if (settings.activeSelf)
        {
            // already on, so turn off
            settings.SetActive(false);
        } else {
            // already off, so turn on
            settings.SetActive(true);
        }
        
        // TODO: remove, was only for testing :)
        AudioManager.Instance.Play("Sound01");
    }

    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<ToggleMurderBoard>(HandleMurderBoardToggle);
        EventDispatcher.Instance.RemoveListener<ToggleSettings>(HandleSettingsToggle);
    }
}
