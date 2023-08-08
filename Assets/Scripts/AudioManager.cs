using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip bgmusic;
    public AudioClip ginbak;
    public AudioSource audioSource;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = bgmusic;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.time <= 20f && gameManager.time >= 3)
        {
            if (audioSource.clip == bgmusic)
            {
                audioSource.Stop();
                audioSource.clip = ginbak;
                audioSource.Play();
            }
        }

        if (gameManager.time <= 0f)
        {
            if (audioSource.clip == ginbak)
            {
                audioSource.Stop();
                audioSource.clip = bgmusic;
                audioSource.Play();
            }
        }
    }

    public void resetAudio()
    {
        audioSource.clip = bgmusic;
        audioSource.Play();
    }
}
