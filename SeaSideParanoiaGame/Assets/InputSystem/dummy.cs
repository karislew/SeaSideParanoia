using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dummy : MonoBehaviour, IPointerEnterHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.Log("hi");
    }

    public void Something()
    {
        Debug.Log("ih");
    }
}
