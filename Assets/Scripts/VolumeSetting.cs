using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    void Start() {
        if (PlayerPrefs.HasKey("musicVolume")) {
            LoadVolume();
        } else {
            SetMusicVolume();
            SetSoundVolume();
        }
    }
    public void SetMusicVolume() {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20); // Because audio mixer change logarithmically
        PlayerPrefs.SetFloat("musicVolume", volume); // While slider change linearly
    }
    public void SetSoundVolume() {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Sound", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("soundVolume", volume);
    }

    private void LoadVolume() {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");

        SetMusicVolume();
        SetSoundVolume();
    }
}
