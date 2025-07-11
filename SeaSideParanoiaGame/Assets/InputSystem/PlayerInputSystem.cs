using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GHEvtSystem;

public class PlayerInputSystem : MonoBehaviour
{
    private PlayerInput playerInput; 

    void Start() {
        playerInput = GetComponent<PlayerInput>();
    }

    public void ToggleJournal(InputAction.CallbackContext context) {
        if (context.performed) {
            EventDispatcher.Instance.RaiseEvent<ToggleJournal>(new ToggleJournal { });
            if (playerInput.currentActionMap.name.Equals("Game")) {
                EventDispatcher.Instance.RaiseEvent<ChangeMode>(new ChangeMode
                {
                    newMode = Mode.Journal
                });
                playerInput.SwitchCurrentActionMap("Journal");
            } else if (playerInput.currentActionMap.name.Equals("Journal")) {
                EventDispatcher.Instance.RaiseEvent<ChangeMode>(new ChangeMode
                {
                    newMode = Mode.Game
                });
                playerInput.SwitchCurrentActionMap("Game");
            }
        }
    }

    public void PageLeft(InputAction.CallbackContext context) {
        if (context.performed) {
            EventDispatcher.Instance.RaiseEvent<TurnPage>(new TurnPage {
                left = true
            });
        }
    }

    public void PageRight(InputAction.CallbackContext context) {
        if (context.performed) {
            EventDispatcher.Instance.RaiseEvent<TurnPage>(new TurnPage {
                left = false
            });
        }
    }

    public void TogglePause() {
        //
    }
}
