using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SetActiveVideo : MonoBehaviour
{
    public GameObject vidPlayer;
    public GameObject vidCanvas;
    // Start is called before the first frame update
    
    void Start()
    {
        vidCanvas.SetActive(true);
        vidPlayer.SetActive(false);
        StartCoroutine(StartingVidComponent());
    }

    // Update is called once per frame
    IEnumerator StartingVidComponent()
    {
        yield return new WaitForSeconds(1.5f);
        vidPlayer.SetActive(true);
        yield return new WaitForSeconds(1.6f);
        vidCanvas.SetActive(false);
    }
}
