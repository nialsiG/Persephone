using UnityEngine;

public class OnClickBehaviour : MonoBehaviour, IClick
{
    private SoundManager sManager;
    [SerializeField] SOSoundPool sPool;
    [SerializeField] ParticleSystem particle;
    [SerializeField] bool unique;

    void Start()
    {
        sManager = GameObject.Find("AudioManager").GetComponent<SoundManager>();
    }
    public void Click()
    {
        int i = sManager.PlaySound(sPool);
        print(i);
        if (i == 1)
        {
            particle.Clear();
            particle.Play();

        }

        if (unique)
            Lib.instance.changeMod = true;

    }
}
