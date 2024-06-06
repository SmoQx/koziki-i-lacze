using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Musicplay : MonoBehaviour
{
    public AudioSource AudioSource;
    public Slider volumeSlider;
    public GameObject ObjectMusic;

    // Value from the slider, and it converts to volume level
    private float MusicVolume = 1f;

    private void Start()
    {
        ObjectMusic = GameObject.FindWithTag("music");
        AudioSource = ObjectMusic.GetComponent<AudioSource>();


        MusicVolume = PlayerPrefs.GetFloat("volume");
        AudioSource.volume = MusicVolume;
        volumeSlider.value = MusicVolume;
    }

    private void Update()
    {
        AudioSource.volume = MusicVolume;
        MusicVolume = volumeSlider.value;
        PlayerPrefs.SetFloat("volume", MusicVolume);
        Debug.Log(volumeSlider.value);
        Debug.Log(AudioSource.volume);
    }

    public void VolumeUpdater(float volume)
    {
        MusicVolume = volume;
    }

    public void MusicReset()
    {
        PlayerPrefs.DeleteKey("volume");
        AudioSource.volume = 1;
        volumeSlider.value = 1;
    }
}