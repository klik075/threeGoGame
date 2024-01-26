using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bgMusic;     // πË∞Ê¿Ωæ«

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = bgMusic;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
