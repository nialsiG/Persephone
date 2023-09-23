using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TombeUnit : MonoBehaviour, IConstruire
{
    [SerializeField] Camera cam = null;
    [SerializeField] TextMeshProUGUI contenanceTxt, gainTxt;
    [SerializeField] Slider contenanceGauge;
    [SerializeField] Animator textAnim;

    [Header("Variables :")]
    [SerializeField] string tombName;
    [SerializeField] int contenanceMax;
    [SerializeField] int buildPrice, inhumationPrice, inhumationReputation;
    [SerializeField] Vector2 rdmCounter, rdmAjouterMort;
    [SerializeField] int maxCurrentStock;

    private int contenance;
    private float counter = -1;
    private int currentPrice;

    private bool stopArrival;

    private void Start()
    {
        //Initialise les valeurs de l'UI
        contenanceTxt.text = 0 + "/" + contenanceMax.ToString();
        contenanceGauge.maxValue = contenanceMax;
        contenanceGauge.value = 0;

        //Desactive les elements de l'UI
        contenanceTxt.gameObject.SetActive(false);
        contenanceGauge.gameObject.SetActive(false);


        currentPrice = inhumationPrice;
    }


    private void Update()
    {
        if (Lib.instance.p == Lib.phase.ARRIVAL)
        {
            if (!stopArrival)
            {
                if (counter < 0)
                {
                    int rdmDead = (int)Random.Range(rdmAjouterMort.x, rdmAjouterMort.y);
                    contenance += rdmDead;

                    //rentrées d'argent par inhumation
                    Lib.instance.SetMoney(rdmDead * currentPrice);
                    //Gain de réputation par inhumation
                    Lib.instance.SetReputation(rdmDead * inhumationReputation);

                    counter = Random.Range(rdmCounter.x, rdmCounter.y);

                    if (contenance > maxCurrentStock || contenance > contenanceMax)
                    {
                        stopArrival = true;
                        maxCurrentStock += contenance;

                        if (contenance > contenanceMax)
                            contenance = contenanceMax;

                        contenanceTxt.text = contenance.ToString() + "/" + contenanceMax.ToString();
                        gainTxt.text = "+" + (rdmDead * currentPrice).ToString();
                        textAnim.SetTrigger("Add");
                    }

                }
                else
                    counter -= Time.deltaTime;

                contenanceGauge.value = contenance;
            }

        }
        else
        {
            //Actualise le prix d'inhumation
            if (Lib.instance.p == Lib.phase.BUILD)
                currentPrice = Lib.instance.GetTombPrice(tombName);
            stopArrival = false;
        }

    }


    public void Construire()
    {
        //Enleve le cout de la tombe au compteur d'argent
        Lib.instance.SetMoney(-buildPrice);

        Color col = GetComponent<SpriteRenderer>().color;
        col.a = 1;
        GetComponent<SpriteRenderer>().color = col;

        contenanceTxt.gameObject.SetActive(true);
        contenanceGauge.gameObject.SetActive(true);
    }
}
