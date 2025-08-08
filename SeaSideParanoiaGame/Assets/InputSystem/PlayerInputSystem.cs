using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GHEvtSystem;
using System.Runtime.Serialization;

public class PlayerInputSystem : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] Journal journalObject;

    void Start() {
        playerInput = GetComponent<PlayerInput>();

        // Setting up actions manually bc we have to use CSharp Events now...
        // I couldn't figure out how to invoke the individual actions...
        playerInput.onActionTriggered += ToggleJournal;
        playerInput.onActionTriggered += PageLeft;
        playerInput.onActionTriggered += PageRight;
        playerInput.onActionTriggered += TogglePause;
    }
    
    public void ToggleJournal(InputAction.CallbackContext context) {
        if ((context.action.name != "OpenJournal" && context.action.name != "CloseJournal") || !context.performed)
        {
            return;
        }
        if (journalObject != null && journalObject.rotate)
        {
            Debug.Log("The page is still rotating fool");
            return;
        }
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

    public void PageLeft(InputAction.CallbackContext context) {
        if (context.action.name != "PageLeft" || !context.performed)
        {
            return;
        }
        
        EventDispatcher.Instance.RaiseEvent<TurnPage>(new TurnPage {
            left = true
        });
    }

    public void PageRight(InputAction.CallbackContext context) {
        if (context.action.name != "PageRight" || !context.performed)
        {
            return;
        }
        
        EventDispatcher.Instance.RaiseEvent<TurnPage>(new TurnPage {
            left = false
        });
    }

    public void TogglePause(InputAction.CallbackContext context) {
        if ((context.action.name != "OpenPause" && context.action.name != "ClosePause") || !context.performed)
        {
            return;
        }
        
        EventDispatcher.Instance.RaiseEvent<TogglePause>(new TogglePause { });
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
