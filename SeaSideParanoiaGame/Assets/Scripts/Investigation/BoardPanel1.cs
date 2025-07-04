using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GHEvtSystem;

public enum ButtonState1
{
    DISABLED,
    NORMAL,
    HIGHLIGHT,
    PRESSED,
    SELECTED
}

public class BoardPanel1 : MonoBehaviour
{
    public ButtonState state = ButtonState.NORMAL;
    private ButtonState previousState = ButtonState.NORMAL;
    /*
    public Sprite spriteDisabled;
    public Sprite spriteNormal;
    public Sprite spriteHighlighted;
    public Sprite spritePressed;
    public Sprite spriteSelected;
    */
    public Color colorDisabled = Color.gray;
    public Color colorNormal = Color.white;
    public Color colorHighlighted = Color.yellow;
    public Color colorPressed = Color.red;
    public Color colorSelected = Color.blue;


    public SpriteRenderer clueDisplay;
    public Image panelImage;
    public Color currentColor;
    public Clue clue;

    private SpriteRenderer spriteRenderer;

    private Dictionary<ButtonState, Color> stateColors = new Dictionary<ButtonState, Color>();

    
}
