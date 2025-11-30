using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class VideoPlayback : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    [SerializeField] string videoFileName;
    void Awake()
    {
        
    }
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        videoPlayer.url = videoPath;
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += PrepareCompleted;
        videoPlayer.time = 0;
        //videoPlayer.Play();
        //videoPlayer.loopPointReached += LoopPointReached;
      
    }
    void PrepareCompleted(VideoPlayer videoPlayer)
    {
        videoPlayer.Play();
    }
    // Start is called before the first frame update
    void LoopPointReached(VideoPlayer vp)
    {
        StartCoroutine(Wait());
        SceneManager.LoadScene("StartScene");
    }
    void Update()
    {
        if ((videoPlayer.frame>0) && (videoPlayer.isPlaying==false))
        {
            StartCoroutine(Wait());
            SceneManager.LoadScene("StartScene");
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
    }
}
