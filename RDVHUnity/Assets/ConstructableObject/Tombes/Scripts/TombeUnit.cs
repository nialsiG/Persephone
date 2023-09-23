using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TombeUnit : MonoBehaviour, IConstruire
{
    [SerializeField] Camera cam = null;
    [SerializeField] SpriteRenderer r;
    [SerializeField] TextMeshProUGUI contenanceTxt, gainTxt;
    [SerializeField] Slider contenanceGauge;
    [SerializeField] Animator textAnim, tombAnim;

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

                    //rentrées d'argent par inhumation
                    Lib.instance.SetMoney(rdmDead * currentPrice);
                    //Gain de réputation par inhumation
                    Lib.instance.SetReputation(rdmDead * inhumationReputation);

                    counter = Random.Range(rdmCounter.x, rdmCounter.y);

                    tombAnim.SetTrigger("Add");



                    
                    gainTxt.text = "+" + (rdmDead * currentPrice).ToString();
                    textAnim.SetTrigger("Add");

                    


                    if (contenance + rdmDead > maxCurrentStock || contenance + rdmDead > contenanceMax)
                    {
                        contenance = maxCurrentStock;
                        stopArrival = true;
                        maxCurrentStock += contenance;

                        if (contenance > contenanceMax)
                            contenance = contenanceMax;


                    }
                    else
                        contenance += rdmDead;

                    contenanceTxt.text = contenance.ToString() + "/" + contenanceMax.ToString();

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

            maxCurrentStock = SetCurrentStock(tombName);

            stopArrival = false;
        }

    }

    int SetCurrentStock(string name)
    {
        int n = 0;

        switch (name)
        {
            case "Commune":
                n = 10 + ((currentPrice * (currentPrice + 1)) / 2) - 5 * currentPrice + Random.Range(-1, 2);
                break;
            case "Familiale":

                break;
            case "Caveau":

                break;
        }

        return n;
    }


    public void Construire()
    {
        //Enleve le cout de la tombe au compteur d'argent
        Lib.instance.SetMoney(-buildPrice);

        Color col = r.GetComponent<SpriteRenderer>().color;
        col.a = 1;
        r.GetComponent<SpriteRenderer>().color = col;

        contenanceTxt.gameObject.SetActive(true);
        contenanceGauge.gameObject.SetActive(true);
    }
}
