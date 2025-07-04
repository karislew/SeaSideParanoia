using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GHEvtSystem
{
    /*** Murder Board Events ***/
    public class SelectedPanel : Event
    {
        public Clue selected;
        public BoardSlot caller;
    }

    public class DeselectedPanel : Event
    {
        public Clue selected;
    }

    public class ReverseSelection : Event
    {
        public BoardSlot caller;
    }
}
