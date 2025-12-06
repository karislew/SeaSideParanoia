using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;
using UnityEngine.InputSystem;

public class OpenSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerInput playerInput;

    void Start()
    {
    }

    public void ToggleSettings()
    {
        EventDispatcher.Instance.RaiseEvent<ToggleSettings>(new ToggleSettings { });

        if (playerInput.currentActionMap.name.Equals("Game")) {
            EventDispatcher.Instance.RaiseEvent<ChangeMode>(new ChangeMode
            {
                newMode = Mode.Pause
            });
            playerInput.SwitchCurrentActionMap("Pause");
        } else if (playerInput.currentActionMap.name.Equals("Pause")) {
            EventDispatcher.Instance.RaiseEvent<RevertMode>(new RevertMode
            {
                modeToRevert = Mode.Pause
            });
            playerInput.SwitchCurrentActionMap("Game");
        }
    }
}
