using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VidPlayer : MonoBehaviour
{
    [SerializeField] string videoFileName;
    // Start is called before the first frame update
    private VideoPlayer videoPlayer;
    void Start()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.time = 0;
        
    }
    void LoopPointReached(VideoPlayer vp)
    {
        StartCoroutine(Wait());
        SceneManager.LoadScene("StartScene");
    }

    // Update is called once per frame
    void Update()
    {
        PlayVideo();
    
    }
    public void PlayVideo()
    {
       
        if (videoPlayer)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            Debug.Log("Playing video from: " + videoPath);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
        }
        
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
    }
}
