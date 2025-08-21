using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_GManager : MonoBehaviour
{
    public Texture2D cursorDefault;
    public Texture2D cursorMurderBoard;
    public Texture2D cursorJournal;
    public Texture2D hint;
    public ModeManager modeManager;
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
        string currentMode = modeManager.currentMode.ToString();
        if (controllerCursor.previousControlScheme == keyboardMouseScheme)
        {
            //Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Cursor.SetCursor(cursorDefault, cursorHotspot, CursorMode.Auto);


            if (currentMode == "Game" || currentMode == "Dialogue")
            {
                if (HintManager.instance.hintShowing == true && HintManager.instance!=null)
                {
                    Cursor.SetCursor(null, cursorHotspot, CursorMode.Auto);
                }
                else
                {
                    Cursor.SetCursor(cursorDefault, cursorHotspot, CursorMode.Auto);
                }
            }
            if (currentMode == "Journal")
            {
                Cursor.SetCursor(cursorJournal, cursorHotspot, CursorMode.Auto);
            }
            if (currentMode == "Murder Board")
            {

                Cursor.SetCursor(cursorMurderBoard, cursorHotspot, CursorMode.Auto);
            }
            
            

        }
        if (controllerCursor.previousControlScheme == gamepadScheme)
        {
            
            Cursor.SetCursor(null, cursorHotspot, CursorMode.Auto);
            Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
