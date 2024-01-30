using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BGMSlider;      // ������ ������ Slider
    [SerializeField] private Toggle BGMMute;    // Mute�� On / Off�� Toggle


    private void Awake()
    {
        // �����̴��� ���� ����� �� AddListener�� ���� �̺�Ʈ ����
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        // ��Ŭ�� ���� ����� �� AddListener�� ���� �̺�Ʈ ����
        BGMMute.onValueChanged.AddListener(SetBGMMute);
    }

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs�� Volume ���� ����Ǿ� ���� ���,
        if (PlayerPrefs.HasKey("Volume"))
        {
            // Slider�� ���� ������ ���� ������ ����.
            BGMSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        else
            BGMSlider.value = 0.5f;     // PlayerPrefs�� Volume�� ���� ���

        // audioMixer.SetFloat("audioMixer�� �����س��� Parameter", float ��)
        // audioMixer�� �̸� �����س��� parameter ���� �����ϴ� �ڵ�.
        // Mathf.Log10(BGMSlider.value) * 20 : ���ú��� �������̱� ������ �ش� ������� ���� ���.
        audioMixer.SetFloat("BGM", Mathf.Log10(BGMSlider.value) * 20);
    }

    public void SetBGMVolume(float volume)
    {
        // ����� Slider�� �� volume���� audioMixer�� Volume �����ϱ�
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        // ����� Volume �� �����ϱ�
        PlayerPrefs.SetFloat("Volume", BGMSlider.value);
    }

    private void SetBGMMute(bool mute)
    {
        // AudioListener : Audio�� ��� ��ü. ���� ī�޶� �޷��ִ�.
        AudioListener.volume = (mute ? 0 : 1);
    }
}
