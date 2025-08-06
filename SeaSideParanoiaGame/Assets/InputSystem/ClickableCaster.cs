using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;

public class ClickableCaster : MonoBehaviour
{
    [SerializeField]
    private CursorCaster cursorCaster;

    void Awake()
    {
        EventDispatcher.Instance.AddListener<GPClick>(HandleGPClick);
    }

    public void Cast()
    {
        Debug.Log("casting...");
        RaycastHit hit = cursorCaster.Cast();
        Transform hitTransform = hit.transform;

        if(hitTransform != null && hitTransform.TryGetComponent(out IClickable clickable))
        {
            clickable.OnGPClick();
        }
    }

    void HandleGPClick(GPClick evt)
    {
        Cast();
    }

    /* TODO
    Call this function when cursor collider detects collision

    void HandleGPHover(GPHover evt)
    {
        RaycastHit hit = cursorCaster.Cast();
        Transform hitTransform = hit.transform;

        if(hitTransform != null && hitTransform.TryGetComponent(out IClickable clickable))
        {
            clickable.OnGPHover();
        }
    }
    */

    void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener<GPClick>(HandleGPClick);
    }
}
