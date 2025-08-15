using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
   // Start is called before the first frame update
   public static HintManager instance;
    private Camera cam;
   [SerializeField] GameObject hintImage;
    private bool hintShowing =false;
    private void Awake()
    {
        if (instance != null)
        {


            Debug.Log("More than one instance");
        }
        instance = this;
        cam = Camera.main;
    }
   void Start()
   {
       gameObject.SetActive(false);
   }


    // Update is called once per frame
    void Update()
    {
     
        transform.position = Input.mousePosition;
        //transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(transform.position);

       

   }


    public void ShowHint()
    {
        //transform.position = UIposition.position;
        gameObject.SetActive(true);
        Cursor.visible = false;
        
        hintShowing = true;
    }
    public void HideHint()
    {
        gameObject.SetActive(false);
        hintShowing = false;
        Cursor.visible = true;
   }
}

