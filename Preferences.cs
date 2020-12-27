using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preferences : MonoBehaviour
{
    private AudioSource[] audio;
    private float fon;
    private float music;

    void Start()
    {
        audio = GameObject.Find("Canvas").GetComponents<AudioSource>();
    }

    void Update()
    {
        fon = GameObject.Find("FonSlider").GetComponent<Slider>().value;
        music = GameObject.Find("MusicSlider").GetComponent<Slider>().value;
        audio[0].volume = fon;
        audio[3].volume = music;
    }

    public void SetVolumeFon(float vol) {
        fon = vol;
    }

    public void SetVolumeMusic(float vol)
    {
        music = vol;
    }
}
