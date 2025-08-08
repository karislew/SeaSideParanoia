using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioPlayer : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IClickable, IEnterable
{
    public Sound bgm;
    public Sound clickSFX;
    public Sound enterSFX;

    void Start()
    {
        if (bgm == null)
        {
            return;
        }

        AudioManager.Instance.Play(bgm.name);
    }

    void OnMouseDown()
    {
        PlayClickSFX();
    }
    void OnMouseEnter()
    {
        PlayEnterSFX();
    }   
    
    public void OnPointerDown(PointerEventData evt)
    {
        PlayClickSFX();
    }
    public void OnPointerEnter(PointerEventData evt)
    {
        PlayEnterSFX();
    }   

    public void OnGPClick()
    {
        PlayClickSFX();
    }
    public void OnGPEnter()
    {
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
