using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCursorManager : MonoBehaviour
{
    public Texture2D cursorDefault;
    public ControllerCursor controllerCursor;
    private Vector2 cursorHotspot;
    private const string gamepadScheme = "Gamepad";
    private const string keyboardMouseScheme = "Keyboard+Mouse";
    // Start is called before the first frame update
    void Start()
    {
        
        cursorHotspot = new Vector2(cursorDefault.width / 2, cursorDefault.height / 2);
        controllerCursor.previousControlScheme = keyboardMouseScheme;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (controllerCursor.previousControlScheme == keyboardMouseScheme)
        {
            //Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Cursor.SetCursor(cursorDefault, cursorHotspot, CursorMode.Auto);
            

        }
        if (controllerCursor.previousControlScheme == gamepadScheme)
        {
            
            Cursor.SetCursor(null, cursorHotspot, CursorMode.Auto);
            Cursor.visible = false;
        
        }
    }
}
