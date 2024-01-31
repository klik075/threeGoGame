using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// ȿ���� ����� ���� ȿ���� Ÿ��
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
    [SerializeField] private AudioSource audioSource;       // BGM�� ����ϱ� ���� AudioManager�� �߰��� AudioSource.
    [SerializeField] private AudioClip bgMusic;     // �������
    [SerializeField] private AudioClip startSceneMusic;     // StartScene �������

    [SerializeField] private AudioMixer audioMixer;     // ���� ������ ���� audioMixer

    [SerializeField] private AudioSource sfxPlayer;     // ȿ���� ����� ���� AudioSource. AudioManager�� ���� ��ü�� �߰��Ǿ� ����.
    [SerializeField] private AudioClip[] sfxClips;      // ȿ���� �迭. Inspector â���� �߰�����.

    private void Awake()
    {
        // AudioManager�� �̱���ȭ.
        // DontDestroyOnLoad�� ���� Scene�� ����Ǿ AudioManager�� �����ֱ� ������
        // �̱����� ���� AudioManager�� instance�� ���� ���� ���, ���� ���� instance�� Destroy ����.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // ���� ���� �� ����� ������� ����
        // ���� Active�Ǿ� �ִ� Scene�� Build ��ȣ�� ���� ����� ����� Ŭ�� ����
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            audioSource.clip = startSceneMusic;
        }
        else
            audioSource.clip = bgMusic;

        // PlayerPrefs�� �̹� ����Ǿ� �ִ� ���� ������ ���� ���, �ε��ϱ�
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
        // Scene�� �ε��� ������ �ε�� Scene�� ���� ������� ���
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
        // ȿ���� ���.
        // ȿ������ ����Ǿ�� �� ���, ����Ǿ�� �� SFXClipType�� �Ű������� �޾�
        // ȿ���� �迭���� �ش��ϴ� �ε����� ȿ������ ���.
        sfxPlayer.PlayOneShot(sfxClips[(int)clipType]);
    }
}
