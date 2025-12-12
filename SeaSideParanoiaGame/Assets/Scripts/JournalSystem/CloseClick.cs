using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;
using UnityEngine.InputSystem;

public class CloseClick : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerInput playerInput;

    void Start()
    {
    }

    public void ToggleJournal()
    {
        EventDispatcher.Instance.RaiseEvent<ToggleJournal>(new ToggleJournal { });

        if (playerInput.currentActionMap.name.Equals("Game")) {
            EventDispatcher.Instance.RaiseEvent<ChangeMode>(new ChangeMode
            {
                newMode = Mode.Journal
            });
            playerInput.SwitchCurrentActionMap("Journal");
        } else if (playerInput.currentActionMap.name.Equals("Journal")) {
            EventDispatcher.Instance.RaiseEvent<RevertMode>(new RevertMode
            {
                modeToRevert = Mode.Journal
            });
            playerInput.SwitchCurrentActionMap("Game");
        }   
    }
}
