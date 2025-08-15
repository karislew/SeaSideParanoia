using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject mainArea;
    public GameObject settingsMenu;

    // Start is called before the first frame update
    void Start()
    {
        EventDispatcher.Instance.AddListener<TogglePause>(HandlePauseToggle);
    }

    void HandlePauseToggle(TogglePause evt)
    {
        if (pauseMenu.activeSelf)
        {
            // already on, so turn off
            pauseMenu.SetActive(false);
        } else {
            // already off, so turn on
            pauseMenu.SetActive(true);
        }
        
        // TODO: remove, was only for testing :)
        AudioManager.Instance.Play("Sound01");
    }
    public void GotoStartScreen()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void GoToSettings()
    {
        mainArea.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void GoToMainArea()
    {
        settingsMenu.SetActive(false);
        mainArea.SetActive(true);
    }

    
    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<TogglePause>(HandlePauseToggle);
    }
}
