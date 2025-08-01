using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeSettings : MonoBehaviour
{
    public float initialVolumeMaster = 0.8f;
    public float initialVolumeBGM = 0.5f;
    public float initialVolumeSFX = 0.5f;

    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;


    void Start()
    {
        masterSlider.value = initialVolumeMaster;
        bgmSlider.value = initialVolumeBGM;
        sfxSlider.value = initialVolumeSFX;

        myMixer.SetFloat("VolumeMaster", Mathf.Log10(initialVolumeMaster)*20);
        myMixer.SetFloat("VolumeBGM", Mathf.Log10(initialVolumeBGM)*20);
        myMixer.SetFloat("VolumeSFX", Mathf.Log10(initialVolumeSFX)*20);
    }


    public void SetMusicVolume()
    {
        float volumeMaster = masterSlider.value;
        float volumeBGM = bgmSlider.value;
        float volumeSFX = sfxSlider.value;

        myMixer.SetFloat("VolumeMaster", Mathf.Log10(volumeMaster)*20);
        myMixer.SetFloat("VolumeBGM", Mathf.Log10(volumeBGM)*20);
        myMixer.SetFloat("VolumeSFX", Mathf.Log10(volumeSFX)*20);
    }
}
