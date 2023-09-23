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
    [SerializeField] SOSoundPool _mainMusic;

    //Sounds
    [SerializeField] SOSoundPool _clickSound;
    [SerializeField] SOSoundPool _hoverSound;
    

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
        PlayMusic(_mainMusic);

    }

    public void PlayMusic(SOSoundPool music)
    {
        music.PlayMusic(musicAudioSource);
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
