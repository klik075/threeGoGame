using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bgMusic;     // πË∞Ê¿Ωæ«
    [SerializeField] private AudioClip startSceneMusic;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            audioSource.clip = startSceneMusic;
        }
        else
            audioSource.clip = bgMusic;

        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLevelWasLoaded(int level)
    {
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
}
