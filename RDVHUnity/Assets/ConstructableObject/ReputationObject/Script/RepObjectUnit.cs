using UnityEngine;

public class RepObjectUnit : MonoBehaviour, IConstruire
{
    [SerializeField] int reputationGain, price, nbPersonnel;
    [SerializeField] bool gainRepContinu;

    private bool endTour;

    void Start()
    {
        //Pour les cabanes uniquements qui sont directement instanciées
        if (gainRepContinu)
        {
            Color col = GetComponent<SpriteRenderer>().color;
            col.a = 1;
            GetComponent<SpriteRenderer>().color = col;

            Lib.instance.SetReputation(reputationGain);
            Lib.instance.SetMoney(-price);
        }
    }

    void Update()
    {
        if (Lib.instance.p == Lib.phase.ARRIVAL)
        {
            if (!endTour)
            {
                if (gainRepContinu)
                {
                    Lib.instance.SetReputation(reputationGain * nbPersonnel);
                    Lib.instance.SetMoney(-price * nbPersonnel);
                }

                endTour = true;
            }
        }
        else
            endTour = false;
    }

    public void Construire()
    {
        Lib.instance.SetReputation(reputationGain);
        Lib.instance.SetMoney(-price);

        if (!gainRepContinu)
            this.enabled = false;

        Color col = GetComponent<SpriteRenderer>().color;
        col.a = 1;
        GetComponent<SpriteRenderer>().color = col;


    }
}
