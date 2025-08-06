using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GHEvtSystem;

/* TODO
- On control / player connect, check if input is a gamepad. If it is, spawn cursor.
  If it is not and cursor already exists, destroy cursor.
*/

public class ControllerInputTest : MonoBehaviour
{
    private InputActionMap currentActionMap;

    private PlayerInput playerInput;
    private PlayerInputActions playerActions;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerActions = new PlayerInputActions();
        playerActions.Enable();

        // Setting up actions manually bc we have to use CSharp Events now...
        // I couldn't figure out how to invoke the individual actions...
        playerInput.onActionTriggered += GP_SelectObject;
    }

    /*** GAME ***/
    // `ControllerCursor.cs` handles moving the cursor

    public void GP_SelectObject(InputAction.CallbackContext context)
    {
        if (context.action.name != "SelectObjects" || !context.performed)
        {
            return;
        }

        Debug.Log("object selected.");
        /* TODO
        Check cursor collision. If other collide is tagged as a clickable,
        do stuff.
        */
        EventDispatcher.Instance.RaiseEvent<GPClick>(new GPClick {});
    }

    public void SelectClue(InputAction.CallbackContext context)
    {
        if (context.action.name != "SelectClue" || !context.performed)
        {
            return;
        }

        // TODO
        EventDispatcher.Instance.RaiseEvent<GPClick>(new GPClick {});
    }
}
