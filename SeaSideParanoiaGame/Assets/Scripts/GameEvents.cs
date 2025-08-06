using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GHEvtSystem
{
    /*** Murder Board Signals ***/
    public class StateChangeRequest : Event
    {
        public string clueName;
        public ButtonState currentState;
        public string callerName;
    }

    public class StateChangeResponse : Event
    {
        public string callerName;
        public ButtonState newState;
    }



    public class FoundClue : Event
    {
        public int clueID;
    }

    public class GuessClue : Event
    {
        public string clueName;
        public bool unGuessing;
    }

    /*** Question Page Signals ***/
    public class SlotUpdate : Event
    {
        public int question;
        public int clueID;
        public bool unGuessing;
    }

    /*** Player Input Signals ***/
    public class ToggleJournal : Event {}
    public class TurnPage : Event {
        public bool left;
    }
    public class ToggleMurderBoard : Event {}
    public class NextQuestion : Event {}
    public class ToggleSettings : Event {}

    /*** Mode Manager Signals ***/
    public class ChangeMode : Event
    {
        public Mode newMode;
    }
    public class RevertMode : Event
    {
        public Mode modeToRevert;
    }

    /*** Cursor Controls  ***/
    public class MoveCursor : Event
    {
        public Vector2 amount;
    }
    public class GPClick : Event {}
}
