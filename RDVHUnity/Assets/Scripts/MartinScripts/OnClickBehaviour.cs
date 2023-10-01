using UnityEngine;

public class OnClickBehaviour : MonoBehaviour, IClick
{
    private SoundManager sManager;
    [SerializeField] SOSoundPool sPool;
    [SerializeField] ParticleSystem particle;

    void Start()
    {
        sManager = GameObject.Find("AudioManager").GetComponent<SoundManager>();
    }
    public void Click()
    {
        int i = sManager.PlaySound(sPool);
        print(i);
        if (i == 3)
        {
            
            particle.Play();
            
        }
            
    }
}
