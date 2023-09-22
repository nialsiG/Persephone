using UnityEngine;

public class Lib : MonoBehaviour
{

    public static Lib instance;
    public enum phase { BUILD, ARRIVAL, EVENT };
    public phase p;

    public float moneyCounter, reputationCounter;

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
}
