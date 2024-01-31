using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BGMSlider;      // BGM 볼륨 조절할 Slider UI
    [SerializeField] private Slider SFXSlider;       // 효과음 볼륨 조절할 Slider UI
    [SerializeField] private Toggle BGMMute;    // Mute를 On/Off할 Toggle UI


    private void Awake()
    {
        // 슬라이더의 값이 변경될 때 AddListener를 통해 자동으로 함수 실행
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
        // 토글의 값이 변경될 때 AddListener를 통해 자동으로 함수 실행
        BGMMute.onValueChanged.AddListener(SetBGMMute);
    }

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs에 미리 설정되어 있는 BGM 볼륨 값이 있을 경우
        if (PlayerPrefs.HasKey("Volume"))
        {
            // 저장되어 있는 볼륨값으로 세팅하기
            BGMSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        else
            BGMSlider.value = 0.5f;

        // PlayerPrefs에 미리 설정되어 있는 효과음 볼륨 값이 있을 경우
        if (PlayerPrefs.HasKey("SFX"))
            SFXSlider.value = PlayerPrefs.GetFloat("SFX");      // 저장되어 있는 볼륨값으로 세팅하기
        else
            SFXSlider.value = 0.5f;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Scene이 시작될 때의 audioMixer의 볼륨 세팅하기
        //
        // audioMixer.SetFloat("audioMixer에 설정해놓은 Parameter", float 값)
        // audioMixer에 미리 설정해놓은 parameter 값을 변경하는 코드.
        // Mathf.Log10(BGMSlider.value) * 20 : 데시벨이 비선형적이기 때문에 해당 방식으로 값을 계산.
        audioMixer.SetFloat("BGM", Mathf.Log10(BGMSlider.value) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(SFXSlider.value) * 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // BGM 볼륨 변경
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        // 볼륨 설정 변경 시 PlayerPrefs에 저장하기
        PlayerPrefs.SetFloat("Volume", BGMSlider.value);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX", SFXSlider.value);
    }

    // 볼륨 Mute하기
    private void SetBGMMute(bool mute)
    {
        // AudioListener : Audio를 듣는 객체. 보통 카메라에 달려있다.
        AudioListener.volume = (mute ? 0 : 1);
    }
}
