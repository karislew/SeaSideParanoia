using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GHEvtSystem;

/*
public enum ButtonState
{
    DISABLED,
    NORMAL,
    SELECTED
}
*/

public class BoardSlot : MonoBehaviour
{
    public ButtonState state = ButtonState.NORMAL;
    /*
    public Sprite spriteDisabled;
    public Sprite spriteNormal;
    public Sprite spriteHighlighted;
    public Sprite spritePressed;
    public Sprite spriteSelected;
    */
    public Color colorDisabled = Color.gray;
    public Color colorNormal = Color.white;
    public Color colorSelected = Color.blue;


    public Image clueDisplay;
    public Image slotImage;
    public Color currentColor;
    public Clue clue;

    private Dictionary<ButtonState, Color> stateColors = new Dictionary<ButtonState, Color>();
    private Button thisButton;


    // Start is called before the first frame update
    void Start()
    {
        stateColors.Add(ButtonState.DISABLED, colorDisabled);
        stateColors.Add(ButtonState.NORMAL, colorNormal);
        stateColors.Add(ButtonState.SELECTED, colorSelected);

        thisButton = GetComponent<Button>();
        if (thisButton != null)
        {
        }

        switch (state)
        {
            case ButtonState.DISABLED:
                currentColor = colorDisabled;
                break;
            case ButtonState.NORMAL:
                currentColor = colorNormal;
                break;
            case ButtonState.SELECTED:
                currentColor = colorSelected;
                break;
        }

        slotImage.color = currentColor;

        if (clue != null)
        {
            clueDisplay.sprite = clue.journal_page;
        }

        EventDispatcher.Instance.AddListener<ReverseSelection>(Deselect);

    }

    void OnClick()
    {
        if (thisButton.enabled == false)
        {
            return;
        }
        Debug.Log("click complete");

        // state != SELECTED
        if (gameObject != EventSystem.current.currentSelectedGameObject)
        {
            state = ButtonState.SELECTED;
            // TODO: raise event for selected panel
            EventDispatcher.Instance.RaiseEvent<SelectedPanel>(new SelectedPanel
            {
                selected = clue,
                caller = this
            });
        }
        // state == SELECTED
        else
        {
            state = ButtonState.NORMAL;
            // TODO: raise event for deselected panel
            EventDispatcher.Instance.RaiseEvent<DeselectedPanel>(new DeselectedPanel
            { selected = clue });
        }


        currentColor = stateColors[state];
        slotImage.color = currentColor;
    }

    void Deselect(ReverseSelection evt)
    {
        if (this != evt.caller)
        {
            return;
        }
        // TODO
        state = ButtonState.NORMAL;
        currentColor = stateColors[state];
        slotImage.color = currentColor;
    }

    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<ReverseSelection>(Deselect);
    }
    
}
