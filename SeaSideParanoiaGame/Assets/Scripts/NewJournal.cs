using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GHEvtSystem;
using System.Drawing.Imaging;
public class NewJournal : MonoBehaviour
{
    public int left;
    public int right;

    public Transform leftPage;
    public Transform rightPage;

    public bool leftPageIsBlank;
    public bool rightPageIsBlank;
    public GameObject previousLeft = null;
    public GameObject previousRight = null;
    public GameObject blankPage;

    public List<GameObject> pages;



    // Start is called before the first frame update
    void Start()
    {
        left = 0;
        right = 1;
        leftPageIsBlank = false;
        rightPageIsBlank = false;
        pages[left].transform.SetParent(leftPage.transform);
        pages[left].transform.localPosition = Vector3.zero;
        pages[left].transform.localRotation = Quaternion.identity;
        pages[left].SetActive(true);

        pages[right].transform.SetParent(rightPage.transform);
        pages[right].transform.localPosition = Vector3.zero;
        pages[right].transform.localRotation = Quaternion.identity;
        pages[right].SetActive(true);


    }
}
