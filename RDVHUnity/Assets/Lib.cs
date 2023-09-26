using UnityEngine;

public class Lib : MonoBehaviour
{

    public static Lib instance;
    public enum phase { EVENT, BUILD, ARRIVAL  };
    public phase p;

    public enum state { TRACK, CLIP, EMPTY };
    public state s;

    public float moneyCounter, reputationCounter, bodyCounter;
    public GameObject CurrentObject = null;

    public int priceCommune, priceFamiliale, priceCaveau, semesterCounter, nbreCaveaux;

    //Faire en sorte que la dette soit private, mais qu'on puisse la lire de partout
    private float _debt;
    public float Debt => _debt;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        _debt = moneyCounter;
    }

    public void SetReputation(int n)
    {
        reputationCounter += n;
        if(reputationCounter < 0)
        {
            reputationCounter = 0;
        }
    }

    public void SetMoney(int n)
    {
        moneyCounter += n;
    }

    public void SetCurrentObject(GameObject GO)
    {
        CurrentObject = GO;
    }

    public void SetTombPrice(string name, int n)
    {
        switch (name)
        {
            case "Commune":
                priceCommune = n;
                break;
            case "Familiale":
                priceFamiliale = n;
                break;
            case "Caveau":
                priceCaveau = n;
                break;
        }
    }

    public int GetTombPrice(string name)
    {
        int i = 0;

        switch (name)
        {
            case "Commune":
                i = priceCommune;
                break;
            case "Familiale":
                i = priceFamiliale;
                break;
            case "Caveau":
                i = priceCaveau;
                break;
        }

        return i;
    }
}
