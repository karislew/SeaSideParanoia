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
    public Mode initialMode = Mode.Game;
    private Stack modeHistory = new Stack(4);


    // Start is called before the first frame update
    void Start()
    {
        modeHistory.Push(initialMode);
        EventDispatcher.Instance.AddListener<ChangeMode>(HandleModeChange);
        EventDispatcher.Instance.AddListener<RevertMode>(HandleRevertMode);
    }
    
    public Mode GetCurrentMode()
    {
        return (Mode)modeHistory.Peek();
    }

    void HandleModeChange(ChangeMode evt)
    {
        modeHistory.Push(evt.newMode);
        Debug.Log("changed mode to " + evt.newMode);
    }

    void HandleRevertMode(RevertMode evt)
    {
        // Double checking that the mode to revert is the current one (at top of stack)
        Mode currentMode = (Mode)modeHistory.Peek();
        if (evt.modeToRevert != currentMode)
        {
            Debug.LogWarning("Trying to revert " + evt.modeToRevert + " but is not current mode. Current mode is " + currentMode);
            return;
        }
        modeHistory.Pop();
        Debug.Log("reverted " + evt.modeToRevert + ", new current mode is " + modeHistory.Peek());
    }

    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<ChangeMode>(HandleModeChange);
        EventDispatcher.Instance.RemoveListener<RevertMode>(HandleRevertMode);
    }
}
