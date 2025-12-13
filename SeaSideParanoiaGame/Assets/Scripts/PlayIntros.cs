using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayIntros : MonoBehaviour
{
    public float duration;
    public string video = "GH_SPLASH_FINAL.webm";
    
    private VideoPlayer video_player;

    // Start is called before the first frame update
    void Start()
    {
        video_player = GetComponent<VideoPlayer>();
        video_player.url = System.IO.Path.Combine(Application.streamingAssetsPath, video);

        PlayIntro();
    }

    public void PlayIntro()
    {
        StartCoroutine(StreamVideo());
        // TODO: move to next scene.
    }

    // Update is called once per frame
    IEnumerator StreamVideo()
    {
        video_player.Play();
        yield return new WaitForSeconds(duration);
        // TODO: play video
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
