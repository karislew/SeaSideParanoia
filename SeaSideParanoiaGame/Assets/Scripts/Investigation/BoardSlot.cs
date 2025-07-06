using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GHEvtSystem;

public enum ButtonState
{
    DISABLED,
    NORMAL,
    SELECTED
}

public class BoardSlot : MonoBehaviour
{
    public ButtonState state = ButtonState.DISABLED;

    public Image clueDisplay;
    public Image selectionHighlight;
    public string clueName;
    private Dictionary<ButtonState, float> stateOpacity = new Dictionary<ButtonState, float>();


    // Start is called before the first frame update
    void Start()
    {
        stateOpacity[ButtonState.NORMAL] = 0.0f;
        stateOpacity[ButtonState.SELECTED] = 0.63f;
        
        EventDispatcher.Instance.AddListener<StateChangeResponse>(HandleStateChangeResponse);
    }

    Color ChangeAlpha(Color color, float alpha)
    {
        return new Color(
            color.r,
            color.g,
            color.b,
            alpha
        );
    }

    public void DoSomething()
    {
        if (state == ButtonState.DISABLED)
        {
            Debug.Log("button not enabled!");
            return;
        }
        Debug.Log("click complete");

        EventDispatcher.Instance.RaiseEvent<StateChangeRequest>(new StateChangeRequest
        {
            clueName = clueName,
            callerName = this.name,
            currentState = state
        });
    }

    void HandleStateChangeResponse(StateChangeResponse evt)
    {
        if (!evt.callerName.Equals(this.name) && !evt.callerName.Equals("all"))
        {
            return;
        }

        state = evt.newState;
        selectionHighlight.color = ChangeAlpha(selectionHighlight.color, stateOpacity[state]);
    }

    public void ShowClue(Clue clue)
    {
        clueName = clue.name;
        clueDisplay.sprite = clue.worldSprite;
        clueDisplay.color = ChangeAlpha(clueDisplay.color, 1.0f);
        state = ButtonState.NORMAL;
    }

    void OnDestroy()
    {
        EventDispatcher.Instance.AddListener<StateChangeResponse>(HandleStateChangeResponse);
    }
    
}
