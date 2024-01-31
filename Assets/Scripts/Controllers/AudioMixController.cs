using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BGMSlider;      // BGM ���� ������ Slider UI
    [SerializeField] private Slider SFXSlider;       // ȿ���� ���� ������ Slider UI
    [SerializeField] private Toggle BGMMute;    // Mute�� On/Off�� Toggle UI


    private void Awake()
    {
        // �����̴��� ���� ����� �� AddListener�� ���� �ڵ����� �Լ� ����
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
        // ����� ���� ����� �� AddListener�� ���� �ڵ����� �Լ� ����
        BGMMute.onValueChanged.AddListener(SetBGMMute);
    }

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs�� �̸� �����Ǿ� �ִ� BGM ���� ���� ���� ���
        if (PlayerPrefs.HasKey("Volume"))
        {
            // ����Ǿ� �ִ� ���������� �����ϱ�
            BGMSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        else
            BGMSlider.value = 0.5f;

        // PlayerPrefs�� �̸� �����Ǿ� �ִ� ȿ���� ���� ���� ���� ���
        if (PlayerPrefs.HasKey("SFX"))
            SFXSlider.value = PlayerPrefs.GetFloat("SFX");      // ����Ǿ� �ִ� ���������� �����ϱ�
        else
            SFXSlider.value = 0.5f;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Scene�� ���۵� ���� audioMixer�� ���� �����ϱ�
        //
        // audioMixer.SetFloat("audioMixer�� �����س��� Parameter", float ��)
        // audioMixer�� �̸� �����س��� parameter ���� �����ϴ� �ڵ�.
        // Mathf.Log10(BGMSlider.value) * 20 : ���ú��� �������̱� ������ �ش� ������� ���� ���.
        audioMixer.SetFloat("BGM", Mathf.Log10(BGMSlider.value) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(SFXSlider.value) * 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // BGM ���� ����
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        // ���� ���� ���� �� PlayerPrefs�� �����ϱ�
        PlayerPrefs.SetFloat("Volume", BGMSlider.value);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX", SFXSlider.value);
    }

    // ���� Mute�ϱ�
    private void SetBGMMute(bool mute)
    {
        // AudioListener : Audio�� ��� ��ü. ���� ī�޶� �޷��ִ�.
        AudioListener.volume = (mute ? 0 : 1);
    }
}
