using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;

public class Journal : MonoBehaviour
{
    [SerializeField] float pageSpeed = .5f;

    public List<Transform> pages;
    int index = -1;
    bool rotate = false;
    public GameObject lastPage;

    void Start()
    {
        InitialState();
        lastPage.SetActive(false);
        
        EventDispatcher.Instance.AddListener<TurnPage>(HandlePaging);
    }

    void HandlePaging(TurnPage evt)
    {
        if (!evt.left && rotate != true)
        {
            RotateForward();
        }
        if (evt.left && rotate != true)
        {
            RotateBack();
        }
        
    }
    
    public void InitialState()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].transform.rotation = Quaternion.identity;

        }
        pages[0].SetAsLastSibling();
    }

    public void RotateForward()
    {
        if (rotate == true) { return; }
        index++;
        if (index < -1 || index >= pages.Count)
        {
           
            index--;
            return;
        }
       

        Debug.Log("Index" + index + " Page Count " + pages.Count);

        float angle = 180f;
        StartCoroutine(UpdatePage());
        StartCoroutine(Rotate(angle, true));
    }

    public void RotateBack()
    {
        if (rotate == true || index < 0) { return; }
        float angle = 0f;
       
        StartCoroutine(UpdatePage());
        StartCoroutine(Rotate(angle, false));
    }

    IEnumerator Rotate(float angle, bool forward)
    {
        float value = 0f;
        while (true)
        {
            rotate = true;
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
            value += Time.deltaTime * pageSpeed;
            pages[index].rotation = Quaternion.Slerp(pages[index].rotation, targetRotation, value);
            float angle1 = Quaternion.Angle(pages[index].rotation, targetRotation);
            if (index == pages.Count - 1)
            {
                lastPage.SetActive(true);
            }
            if (index < pages.Count - 1)
            {
                lastPage.SetActive(false);
            }
            if (angle1 < .1f)
            {
                if (forward == false)
                {
                    index--;
                }
                rotate = false;
                break;
            }
            yield return null;
        }
    }

    IEnumerator UpdatePage() {
        yield return new WaitForEndOfFrame();
        pages[index].SetAsLastSibling();
    }

    void OnDestroy() {
        EventDispatcher.Instance.RemoveListener<TurnPage>(HandlePaging);
    }

}
