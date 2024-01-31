using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// 효과음 재생을 위한 효과음 타입
public enum SFXClipType
{
    Attack,
    Hit,
    LevelUp,
    GameClear,
    GameOver
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource audioSource;       // BGM을 재생하기 위한 AudioManager에 추가된 AudioSource.
    [SerializeField] private AudioClip bgMusic;     // 배경음악
    [SerializeField] private AudioClip startSceneMusic;     // StartScene 배경음악

    [SerializeField] private AudioMixer audioMixer;     // 볼륨 조절을 위한 audioMixer

    [SerializeField] private AudioSource sfxPlayer;     // 효과음 재생을 위한 AudioSource. AudioManager의 하위 객체에 추가되어 있음.
    [SerializeField] private AudioClip[] sfxClips;      // 효과음 배열. Inspector 창에서 추가했음.

    private void Awake()
    {
        // AudioManager의 싱글톤화.
        // DontDestroyOnLoad를 통해 Scene이 변경되어도 AudioManager가 남아있기 때문에
        // 싱글톤을 위해 AudioManager의 instance가 새로 생길 경우, 새로 생긴 instance를 Destroy 해줌.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 게임 시작 시 재생할 배경음악 선택
        // 현재 Active되어 있는 Scene의 Build 번호에 따라 재생할 오디오 클립 변경
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            audioSource.clip = startSceneMusic;
        }
        else
            audioSource.clip = bgMusic;

        // PlayerPrefs에 이미 저장되어 있는 볼륨 설정이 있을 경우, 로드하기
        if (PlayerPrefs.HasKey("Volume"))
        {
            audioMixer.SetFloat("BGM", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
        }
        if(PlayerPrefs.HasKey("SFX"))
            audioMixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat("SFX")) * 20);

        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLevelWasLoaded(int level)
    {
        // Scene을 로드할 때마다 로드된 Scene에 맞춰 배경음악 재생
        switch (level)
        {
            case 0:
                audioSource.clip = startSceneMusic;
                break;
            case 1:
                audioSource.clip = bgMusic;
                break;
        }

        audioSource.Play();
    }

    public void PlayClip(SFXClipType clipType)
    {
        // 효과음 재생.
        // 효과음이 재생되어야 할 경우, 재생되어야 할 SFXClipType을 매개변수로 받아
        // 효과음 배열에서 해당하는 인덱스의 효과음을 출력.
        sfxPlayer.PlayOneShot(sfxClips[(int)clipType]);
    }
}
