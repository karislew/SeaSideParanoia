using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;

public enum Mode {
    Game,
    Journal,
    Pause,
    Dialogue,
}

public class ModeManager : Singleton<ModeManager>
{
    private Mode currentMode = Mode.Game;


    // Start is called before the first frame update
    void Start()
    {
        EventDispatcher.Instance.AddListener<ChangeMode>(HandleModeChange);
    }
    
    public Mode GetCurrentMode()
    {
        return currentMode;
    }

    void HandleModeChange(ChangeMode evt)
    {
        currentMode = evt.newMode;
        Debug.Log("changed mode to " + currentMode);
    }

    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<ChangeMode>(HandleModeChange);
    }
}
