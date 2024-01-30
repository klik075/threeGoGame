using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BGMSlider;      // 볼륨을 조절할 Slider
    [SerializeField] private Toggle BGMMute;    // Mute를 On / Off할 Toggle


    private void Awake()
    {
        // 슬라이더의 값이 변경될 때 AddListener를 통해 이벤트 구독
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        // 토클의 값이 변경될 때 AddListener를 통해 이벤트 구독
        BGMMute.onValueChanged.AddListener(SetBGMMute);
    }

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs에 Volume 값이 저장되어 있을 경우,
        if (PlayerPrefs.HasKey("Volume"))
        {
            // Slider의 값을 저장해 놓은 값으로 변경.
            BGMSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        else
            BGMSlider.value = 0.5f;     // PlayerPrefs에 Volume이 없을 경우

        // audioMixer.SetFloat("audioMixer에 설정해놓은 Parameter", float 값)
        // audioMixer에 미리 설정해놓은 parameter 값을 변경하는 코드.
        // Mathf.Log10(BGMSlider.value) * 20 : 데시벨이 비선형적이기 때문에 해당 방식으로 값을 계산.
        audioMixer.SetFloat("BGM", Mathf.Log10(BGMSlider.value) * 20);
    }

    public void SetBGMVolume(float volume)
    {
        // 변경된 Slider의 값 volume으로 audioMixer의 Volume 변경하기
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        // 변경된 Volume 값 저장하기
        PlayerPrefs.SetFloat("Volume", BGMSlider.value);
    }

    private void SetBGMMute(bool mute)
    {
        // AudioListener : Audio를 듣는 객체. 보통 카메라에 달려있다.
        AudioListener.volume = (mute ? 0 : 1);
    }
}
