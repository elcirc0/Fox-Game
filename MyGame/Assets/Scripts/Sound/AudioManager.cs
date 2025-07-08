using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    float SettingsSlider1;
    float SettingsSlider2;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayMusic("Theme");
    }
    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.Name == name);

        if (sound == null)
            Debug.Log("«вук не найден");
        else
        {
            musicSource.clip = sound.AudioClip;
            musicSource.Play();
        }

    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.Name == name);

        if (sound == null)
            Debug.Log("«вук не найден");

        else sfxSource.PlayOneShot(sound.AudioClip);

    }

    public void ChangeMusicValue()
    {
        SettingsSlider1 = UiController.instance.SettingsSlider1.value;
        musicSource.volume = SettingsSlider1;
        //musicSource.volume = SaveData.SettingsSlider1;
    }
    public void ChangeSoundsValue()
    {
        SettingsSlider2 = UiController.instance.SettingsSlider2.value;
        sfxSource.volume = SettingsSlider2;
        //sfxSource.volume = SaveData.SettingsSlider2;
    }
}
