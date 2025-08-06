using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorCaster : MonoBehaviour
{
    [SerializeField]
    private ControllerCursor controllerCursor;

    private Camera mainCamera;
    private Mouse currentMouse;
    
    void Awake()
    {
        mainCamera = Camera.main;
        currentMouse = Mouse.current;
    }
    
    public RaycastHit Cast()
    {
        RaycastHit hit;
        Ray ray;
        if (controllerCursor.IsGamepadScheme())
        {
            ray = mainCamera.ScreenPointToRay(controllerCursor.GetVirtualMousePosition());
        }
        else
        {
            ray = mainCamera.ScreenPointToRay(currentMouse.position.ReadValue());
        }

        Physics.Raycast(ray, out hit);

        return hit;
    }
}
