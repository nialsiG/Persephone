using TMPro;
using UnityEngine;

public class RepObjectUnit : MonoBehaviour, IConstruire
{
    [SerializeField] SpriteRenderer r;
    [SerializeField] Animator textAnim, spriteAnim, moneyAnim;
    [SerializeField] TextMeshProUGUI repTxt, moneyTxt;

    [SerializeField] int reputationGain, price, nbPersonnel;
    [SerializeField] bool gainRepContinu;

    private bool endTour;

    void Start()
    {
        //Pour les cabanes uniquements qui sont directement instanciées
        if (gainRepContinu)
        {
            Color col = r.GetComponent<SpriteRenderer>().color;
            col.a = 1;
            r.GetComponent<SpriteRenderer>().color = col;

            Lib.instance.SetReputation(reputationGain);
            Lib.instance.SetMoney(-price);

            if (reputationGain != 0)
            {
                repTxt.text = "+" + reputationGain.ToString();
                textAnim.SetTrigger("Add");
            }
            
            moneyTxt.text = "-" + price.ToString();
            
            spriteAnim.SetTrigger("Add");
            moneyAnim.SetTrigger("Add");
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

                    if (reputationGain != 0)
                    {
                        repTxt.text = "+" + reputationGain.ToString();
                        textAnim.SetTrigger("Add");
                    }

                    moneyTxt.text = "-" + price.ToString();
                    spriteAnim.SetTrigger("Add");
                    moneyAnim.SetTrigger("Add");
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

        Color col = r.GetComponent<SpriteRenderer>().color;
        col.a = 1;
        r.GetComponent<SpriteRenderer>().color = col;

        textAnim.SetTrigger("Add");
        spriteAnim.SetTrigger("Add");
        repTxt.text = "+" + reputationGain.ToString();
    }
}
