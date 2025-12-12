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
   private double vidLength;
    void Start()
    {
        PlayVideo();
      
        Debug.Log("VidPlayer Start called");
       
   
        
    }

    // Update is called once per frame

    public void PlayVideo()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer)
        {
            videoPlayer.time = 0;

            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            Debug.Log("Playing video from: " + videoPath);
            videoPlayer.url = videoPath;
            videoPlayer.Prepare();
         
            videoPlayer.Play();
            videoPlayer.loopPointReached += LoopPointReached;
        }
        
    }
    void Update()
    {
        if(videoPlayer){
      
        if ((videoPlayer.frame>0) && (videoPlayer.isPlaying==false))
        {
            StartCoroutine(Wait());
            //SceneManager.LoadScene("StartScene");
        }
        }
    }
    void LoopPointReached(VideoPlayer vp)
    {
        StartCoroutine(Wait());
        
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("StartScene");
    }
}
