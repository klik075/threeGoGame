using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Toggle BGMMute;


    private void Awake()
    {
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        BGMMute.onValueChanged.AddListener(SetBGMMute);
    }

    // Start is called before the first frame update
    void Start()
    {
        BGMSlider.value = 0.5f;
        audioMixer.SetFloat("BGM", BGMSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    private void SetBGMMute(bool mute)
    {
        AudioListener.volume = (mute ? 0 : 1);
    }
}
