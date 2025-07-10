using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GHEvtSystem
{
    /*** Murder Board Events ***/
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
        public string clueName;
    }

    /*** Player Input Signals ***/
    public class ToggleJournal : Event {}
    public class TurnPage : Event {
        public bool left;
    }
}
