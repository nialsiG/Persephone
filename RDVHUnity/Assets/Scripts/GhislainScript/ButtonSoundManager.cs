using UnityEngine;

public class ButtonSoundManager : MonoBehaviour
{
    //Sounds
    [SerializeField] SOSoundPool _clickSound;
    [SerializeField] SOSoundPool _hoverSound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Create music audiosource
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void PlaySound(SOSoundPool sound)
    {
        //option 1 : kill sound
        audioSource.Stop();
        //...and play sound
        sound.PlaySound(audioSource);
    }

    public void PlayClick()
    {
        PlaySound(_clickSound);
    }
    public void PlayHover()
    {
        PlaySound(_hoverSound);
    }
}
