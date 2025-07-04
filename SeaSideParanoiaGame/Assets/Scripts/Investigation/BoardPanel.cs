using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GHEvtSystem;

public enum ButtonState
{
    DISABLED,
    NORMAL,
    HIGHLIGHT,
    PRESSED,
    SELECTED
}

public class BoardPanel : MonoBehaviour
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

    void Start()
    {
        stateColors.Add(ButtonState.DISABLED, colorDisabled);
        stateColors.Add(ButtonState.NORMAL, colorNormal);
        stateColors.Add(ButtonState.HIGHLIGHT, colorHighlighted);
        stateColors.Add(ButtonState.PRESSED, colorPressed);
        stateColors.Add(ButtonState.SELECTED, colorSelected);

        spriteRenderer = GetComponent<SpriteRenderer>();
        panelImage = GetComponent<Image>();

        switch (state)
        {
            case ButtonState.DISABLED:
                currentColor = colorDisabled;
                break;
            case ButtonState.NORMAL:
                currentColor = colorNormal;
                break;
            case ButtonState.HIGHLIGHT:
                currentColor = colorHighlighted;
                break;
            case ButtonState.PRESSED:
                currentColor = colorPressed;
                break;
            case ButtonState.SELECTED:
                currentColor = colorSelected;
                break;
        }
        if (panelImage != null)
        {
            spriteRenderer.sprite = panelImage.sprite;
        }
        spriteRenderer.color = currentColor;

        if (clue != null)
        {
            clueDisplay.sprite = clue.journal_page;
        }

        EventDispatcher.Instance.AddListener<ReverseSelection>(Deselect);
    }

    void OnMouseEnter()
    {
        if (state == ButtonState.DISABLED)
        {
            return;
        }

        //Debug.Log("enter");
        previousState = state;
        state = ButtonState.HIGHLIGHT;
        currentColor = stateColors[state];
        spriteRenderer.color = currentColor;
    }

    void OnMouseExit()
    {
        if (state == ButtonState.DISABLED)
        {
            return;
        }
        //Debug.Log("exit");

        state = previousState;
        currentColor = stateColors[state];
        spriteRenderer.color = currentColor;
    }

    void OnMouseDown()
    {
        if (state == ButtonState.DISABLED)
        {
            return;
        }
        //Debug.Log("press");

        state = ButtonState.PRESSED;
        currentColor = stateColors[state];
        spriteRenderer.color = currentColor;
    }

    void OnMouseUp()
    {
        if (state == ButtonState.DISABLED)
        {
            return;
        }
        //Debug.Log("click complete");

        if (previousState == ButtonState.NORMAL)
        {
            state = ButtonState.SELECTED;
            previousState = state;
            // TODO: raise event for selected panel
            EventDispatcher.Instance.RaiseEvent<SelectedPanel>(new SelectedPanel
            {
                selected = clue,
                caller = this
            });
        }
        else if (previousState == ButtonState.SELECTED)
        {
            state = ButtonState.NORMAL;
            previousState = state;
            // TODO: raise event for deselected panel
            EventDispatcher.Instance.RaiseEvent<DeselectedPanel>(new DeselectedPanel
            { selected = clue });
        }


        currentColor = stateColors[state];
        spriteRenderer.color = currentColor;
    }

    void Deselect(ReverseSelection evt)
    {
        if (this != evt.caller)
        {
            return;
        }
        // TODO
        state = ButtonState.NORMAL;
        previousState = state;
        currentColor = stateColors[state];
        spriteRenderer.color = currentColor;
    }

    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<ReverseSelection>(Deselect);
    }
}
