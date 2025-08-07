using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hint : MonoBehaviour, IEnterable, IExitable
{
    // Start is called before the first frame update

    private void OnMouseEnter()
    {
        CallShowHint();
    }
    private void OnMouseExit()
    {
        CallHideHint();
    }

    public void OnGPEnter()
    {
        CallShowHint();
    }
    public void OnGPExit()
    {
        CallHideHint();
    }

    private void CallShowHint()
    {
        HintManager.instance.ShowHint();
    }
    private void CallHideHint()
    {
        HintManager.instance.HideHint();
    }
}
