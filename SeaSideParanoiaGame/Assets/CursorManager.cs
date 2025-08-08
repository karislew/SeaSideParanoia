using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    [SerializeField] ModeManager modeManager;
    private Image thisImage;
    public Sprite pencil;
    public Sprite game;
    public Sprite murderBoard;
    // Start is called before the first frame update
    void Start()
    {
        thisImage = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        string currentMode = modeManager.currentMode.ToString();
        if (currentMode == "Journal")
        {
            thisImage.sprite = pencil;
        }
        if (currentMode == "Game")
        {
            thisImage.sprite = game;
        }
        if (currentMode == "Murder Board")
        {
            thisImage.sprite = murderBoard;
        }
        
        
    }
}
