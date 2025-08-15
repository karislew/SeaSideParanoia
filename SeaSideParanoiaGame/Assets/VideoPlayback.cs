using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class VideoPlayback : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    void Awake()
    {
        
    }
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.time = 0;
        videoPlayer.loopPointReached += LoopPointReached;
    }
    // Start is called before the first frame update
    void LoopPointReached(VideoPlayer vp)
    {
        StartCoroutine(Wait());
        SceneManager.LoadScene("StartScene");
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
