using UnityEngine;

public class Lib : MonoBehaviour
{

    public static Lib instance;
    public enum phase { BUILD, ARRIVAL, EVENT };
    public phase p;

    public enum state { TRACK, CLIP, EMPTY };
    public state s;

    public float moneyCounter, reputationCounter;
    public GameObject CurrentObject = null;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void SetReputation(int n)
    {
        reputationCounter += n;
    }

    public void SetMoney(int n)
    {
        moneyCounter += n;
    }

    public void SetCurrentObject(GameObject GO)
    {
        CurrentObject = GO;
    }
}
