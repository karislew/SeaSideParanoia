using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    void Start()
    {
        InteractableManager.GetInstance().AddInteractable(this);
    }
    private void OnDestroy()
    {
        InteractableManager.GetInstance().RemoveInteractable(this);
    }
    public abstract void OnInteract();
    
}
