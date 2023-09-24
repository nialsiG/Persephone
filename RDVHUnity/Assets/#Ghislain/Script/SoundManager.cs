using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //SoundQueue
    [SerializeField] int _soundQueueLength;
    List<AudioSource> soundQueue;
    private int currentIntInQueue;

    //Music
    AudioSource musicAudioSource;
    [SerializeField] SOSoundPool[] _music;
    private int musicIndex;

    //Sounds
    [SerializeField] SOSoundPool _clickSound;
    [SerializeField] SOSoundPool _hoverSound;
    
    //Singleton
    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Create soundqueue
        soundQueue = new List<AudioSource>();
        for (int i = 0; i < _soundQueueLength; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            soundQueue.Add(source);
        }

        //Create music audiosource
        musicAudioSource = gameObject.AddComponent<AudioSource>();

        //Play music
        PlayMusic(_music[0]);

    }

    private void Update()
    {
        
    }

    public void PlayMusic(SOSoundPool music)
    {
        music.PlayMusic(musicAudioSource);
    }

    public void ChangeMusic()
    {
        //start coroutine fade out
        StartCoroutine(FadeOut(musicAudioSource));
        //change music
        musicIndex += 1;
        PlayMusic(_music[musicIndex % _music.Length]);
        //start coroutine fade in
        StartCoroutine(FadeIn(musicAudioSource));
    }

    //Coroutines
    //...for decreasing volume when the music stops
    IEnumerator FadeOut(AudioSource audioSource)
    {
        for (float alpha = 1f; alpha >= 0f; alpha -= 0.1f)
        {
            audioSource.volume = alpha;
            if (alpha == 0) audioSource.Stop();
            yield return null;
        }
    }

    //...for increasing volume when the music starts
    IEnumerator FadeIn(AudioSource audioSource)
    {
        for (float alpha = 0f; alpha <= 1f; alpha += 0.1f)
        {
            audioSource.volume = alpha;
            yield return null;
        }
    }

    public void PlaySound(SOSoundPool soundPool)
    {
        //Select the next audiosource in queue
        currentIntInQueue += 1;
        Debug.Log("Currently playing audiosource n." + currentIntInQueue % (_soundQueueLength - 1));

        //option 1 : kill sound
        soundQueue[currentIntInQueue % (_soundQueueLength - 1)].Stop();
        //option 2 : let the sound play?
        //if (!soundQueue[currentIntInQueue].isPlayint) {}

        //...and play sound
        soundPool.PlaySound(soundQueue[currentIntInQueue % (_soundQueueLength - 1)]);
    }

    public void PlaySoundClick()
    {
        PlaySound(_clickSound);
    }

    public void PlaySoundHover()
    {
        PlaySound(_hoverSound);
    }


}
