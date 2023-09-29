using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] float _musicVolume;
    [SerializeField] float _ambientVolume;
    [SerializeField] float _soundVolume;
    [SerializeField] GameObject _volumeSlider;
    private float generalVolume;

    [Header("Music and ambient")]
    [SerializeField] SOSoundPool[] _music;
    [SerializeField] SOSoundPool _theme;
    [SerializeField] SOSoundPool _ambient;
    [SerializeField] SOSoundPool _victory;
    [SerializeField] SOSoundPool _defeat;
    
    [Header("Sound queue")]
    [SerializeField] int _soundQueueLength;
    [SerializeField] SOSoundPool _buildingMenuOpen;
    [SerializeField] SOSoundPool _buildingMenuClose;
    [SerializeField] SOSoundPool _buildTomb;
    [SerializeField] SOSoundPool _buildFlower;
    [SerializeField] SOSoundPool _buildStructure;
    [SerializeField] SOSoundPool _paperOpen;
    [SerializeField] SOSoundPool _paperClose;
    [SerializeField] SOSoundPool _paperModif;
    [SerializeField] SOSoundPool _cantModify;

    AudioSource ambientAudioSource;
    List<AudioSource> musicAudioSource;
    List<AudioSource> soundQueue;
    private int currentIntInQueue;
    private int musicIntInQueue;
    private int musicIndex;

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
            source.volume = _soundVolume;
            soundQueue.Add(source);
        }

        //Create ambient audiosource
        ambientAudioSource = gameObject.AddComponent<AudioSource>();
        ambientAudioSource.volume = _ambientVolume;

        //Create music audiosource
        musicAudioSource = new List<AudioSource>();
        for (int i = 0; i < 2; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.volume = _musicVolume * i;
            musicAudioSource.Add(source);
        }
        musicIndex = 0;
        musicIntInQueue = 1;

        //Set initial volume
        ChangeVolume();

        //Play theme
        PlayMusic(_theme);
    }

    public void ChangeVolume()
    {
        Slider slider = _volumeSlider.GetComponent<Slider>();
        generalVolume = slider.value / slider.maxValue;

        ambientAudioSource.volume = _ambientVolume * generalVolume;

        foreach (AudioSource s in musicAudioSource)
        {
            s.volume = _musicVolume * generalVolume;
        }

        foreach (AudioSource s in soundQueue)
        {
            s.volume = _soundVolume * generalVolume;
        }
    }

    public void PlayAmbient()
    {
        _ambient.PlayMusic(ambientAudioSource);
    }

    public void PlayMusic(SOSoundPool music)
    {
        music.PlayMusic(musicAudioSource[musicIntInQueue % _music.Length]);
    }

    //FONCTION PROBLEMATIQUE
    public void ChangeMusic()
    {
        //start coroutine fade out
        StartCoroutine(FadeOut(musicAudioSource[musicIntInQueue % 2]));
        //change music
        musicIndex += 1;
        musicIntInQueue += 1;
        PlayMusic(_music[musicIndex % _music.Length]);
        //start coroutine fade in
        StartCoroutine(FadeIn(musicAudioSource[musicIntInQueue % 2]));
    }

    //FONCTION PROBLEMATIQUE 
    public void ChangeMusic(SOSoundPool music)
    {
        //start coroutine fade out
        StartCoroutine(FadeOut(musicAudioSource[musicIntInQueue % 2]));
        //change music
        musicIntInQueue += 1; 
        PlayMusic(music);
        //start coroutine fade in
        StartCoroutine(FadeIn(musicAudioSource[musicIntInQueue % 2]));
    }

    //Coroutines
    //...for decreasing volume when the music stops
    IEnumerator FadeOut(AudioSource audioSource)
    {
        for (float v = (_musicVolume * generalVolume); v >= 0f; v -= 0.001f)
        {
            audioSource.volume = v;
            yield return null;
        }
    }

    //...for increasing volume when the music starts
    IEnumerator FadeIn(AudioSource audioSource)
    {
        for (float v = 0f; v <= (_musicVolume * generalVolume); v += 0.001f)
        {
            audioSource.volume = v;
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

    public void PlayVictory()
    {
        ChangeMusic(_victory);
    }

    public void PlayDefeat()
    {
        ChangeMusic(_defeat);
    }

    public void PlayUIBuildTomb()
    {
        PlaySound(_buildTomb);
    }
    public void PlayUIBuildFlower()
    {
        PlaySound(_buildFlower);
    }
    public void PlayUIBuildStructure()
    {
        PlaySound(_buildStructure);
    }
    public void PlayUIBuildingMenuOpen()
    {
        PlaySound(_buildingMenuOpen);
    }
    public void PlayUIBuildingMenuClose()
    {
        PlaySound(_buildingMenuClose);
    }
    public void PlayUIPaperOpen()
    {
        PlaySound(_paperOpen);
    }
    public void PlayUIPaperClose()
    {
        PlaySound(_paperClose);
    }    
    public void PlayUIPaperModif()
    {
        PlaySound(_paperModif);
    }    
    public void PlayUICantModify()
    {
        PlaySound(_cantModify);
    }



}
