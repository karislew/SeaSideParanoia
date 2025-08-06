using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.LowLevel;


public class ControllerCursor : MonoBehaviour
{
    [SerializeField]
    private float speed = 1000;
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private RectTransform cursorTransform;
    [SerializeField]
    private RectTransform canvasTransform;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private float padding = 35f;

    //private Collider2D collider;
    private Mouse virtualMouse;
    private bool previousMouseState;
    private Camera mainCamera;
    private Mouse currentMouse;

    private string previousControlScheme = "";
    private const string gamepadScheme = "Gamepad";
    private const string keyboardMouseScheme = "Keyboard+Mouse";

    void OnEnable()
    {
        mainCamera = Camera.main;
        currentMouse = Mouse.current;

        if (virtualMouse == null)
        {
            virtualMouse = (Mouse) InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }

        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        if (cursorTransform != null)
        {
            Vector2 position = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, position);
        }

        InputSystem.onAfterUpdate += UpdateMotion;
        playerInput.onControlsChanged += OnControlsChanged;
        Debug.Log("virtual mouse enabled");
    }

    void OnDisable()
    {
        InputSystem.onAfterUpdate -= UpdateMotion;
        playerInput.onControlsChanged -= OnControlsChanged;

        if (virtualMouse != null && virtualMouse.added)
        {
            InputSystem.RemoveDevice(virtualMouse);
        }
    }

    void UpdateMotion()
    {
        if (virtualMouse == null || Gamepad.current == null)
        {
            return;
        }

        Vector2 deltaValue = Gamepad.current.leftStick.ReadValue();
        deltaValue *= speed * Time.deltaTime;

        Vector2 currentPosition = virtualMouse.position.ReadValue();
        Vector2 newPosition = currentPosition + deltaValue;

        newPosition.x = Mathf.Clamp(newPosition.x, padding, Screen.width - padding);
        newPosition.y = Mathf.Clamp(newPosition.y, padding, Screen.height - padding);

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, deltaValue);

        bool aButtonIsPressed = Gamepad.current.aButton.IsPressed();
        if (previousMouseState != aButtonIsPressed)
        {
            virtualMouse.CopyState<MouseState>(out var mouseState);
            mouseState.WithButton(MouseButton.Left, aButtonIsPressed);
            InputState.Change(virtualMouse, mouseState);
            previousMouseState = aButtonIsPressed;
        }

        AnchorCursor(newPosition);
    }

    void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasTransform,
            position,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera,
            out anchoredPosition
        );
        cursorTransform.anchoredPosition = anchoredPosition;
    }

    void OnControlsChanged(PlayerInput input)
    {
        if (playerInput.currentControlScheme == keyboardMouseScheme &&
        previousControlScheme != keyboardMouseScheme)
        {
            cursorTransform.gameObject.SetActive(false);
            Cursor.visible = true;
            currentMouse.WarpCursorPosition(virtualMouse.position.ReadValue());
            previousControlScheme = keyboardMouseScheme;
        }
        else if (playerInput.currentControlScheme == gamepadScheme &&
        previousControlScheme != gamepadScheme)
        {
            cursorTransform.gameObject.SetActive(true);
            Cursor.visible = false;
            InputState.Change(virtualMouse.position, currentMouse.position.ReadValue());
            AnchorCursor(currentMouse.position.ReadValue());
            previousControlScheme = gamepadScheme;
        }
    }

    public bool IsGamepadScheme()
    {
        return playerInput.currentControlScheme == gamepadScheme ? true : false;
    }

    public Vector2 GetVirtualMousePosition()
    {
        return virtualMouse.position.ReadValue();
    }

    public Vector2 GetPosition()
    {
        if (cursorTransform.gameObject.activeSelf)
        {
            return virtualMouse.position.ReadValue();
        }
        
        return currentMouse.position.ReadValue();
    }
}
