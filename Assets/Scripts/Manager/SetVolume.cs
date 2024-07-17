using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SetVolume : MonoBehaviour
{

    public AudioMixer audioMixer;

    public Slider audioSlider;
    private void Awake()
    {
        audioMixer.SetFloat("Music", -20);
    }
    public void AudioControl()
    {
        float sound = audioSlider.value;
        if (sound == -40f)
            audioMixer.SetFloat("Music", -80);
        else
            audioMixer.SetFloat("Music", sound);
    }
    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
}