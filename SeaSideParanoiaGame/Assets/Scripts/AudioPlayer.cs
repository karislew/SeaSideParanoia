using UnityEngine.Audio;
using UnityEngine;

public class AudioPlayer : MonoBehaviour, IClickable, IEnterable
{
    public Sound clickSFX;
    public Sound enterSFX;
    
    void OnMouseDown()
    {
        PlayClickSFX();
    }
    void OnMouseEnter()
    {
        Debug.Log("hellp");
        PlayEnterSFX();
    }   

    public void OnGPClick()
    {
        PlayClickSFX();
    }
    public void OnGPEnter()
    {
        Debug.Log("hellp");
        PlayEnterSFX();
    }

    void PlayEnterSFX()
    {
        if (enterSFX == null || enterSFX.type != SoundType.SFX)
        {
            return;
        }

        AudioManager.Instance.Play(enterSFX.name);
        Debug.Log("play enter sfx");
    }

    void PlayClickSFX()
    {
        if (clickSFX == null || clickSFX.type != SoundType.SFX)
        {
            return;
        }
        
        AudioManager.Instance.Play(clickSFX.name);
        Debug.Log("play click sfx");
    }
}
