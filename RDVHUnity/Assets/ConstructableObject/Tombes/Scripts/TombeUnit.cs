using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TombeUnit : MonoBehaviour, IConstruire
{
    [SerializeField] Camera cam = null;
    [SerializeField] TextMeshProUGUI maxContenanceTxt;
    [SerializeField] Slider contenanceGauge;

    [Header("Variables :")]
    [SerializeField] int contenanceMax;
    [SerializeField] int price, inhumationPrice, inhumationReputation;
    [SerializeField] Vector2 rdmCounter, rdmAjouterMort;
    [SerializeField] int maxCurrentStock;

    private int contenance;
    private float counter = -1;

    private bool stopArrival;

    private void Start()
    {
        //Initialise les valeurs de l'UI
        maxContenanceTxt.text = contenanceMax.ToString();
        contenanceGauge.maxValue = contenanceMax;
        contenanceGauge.value = 0;

        //Desactive les elements de l'UI
        maxContenanceTxt.gameObject.SetActive(false);
        contenanceGauge.gameObject.SetActive(false);

        
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
                    Lib.instance.SetMoney(rdmDead * inhumationPrice);
                    //Gain de réputation par inhumation
                    Lib.instance.SetReputation(rdmDead * inhumationReputation);

                    counter = Random.Range(rdmCounter.x, rdmCounter.y);

                    if (contenance > maxCurrentStock || contenance > contenanceMax)
                    {
                        stopArrival = true;
                        maxCurrentStock += contenance;
                    }

                }
                else
                    counter -= Time.deltaTime;

                contenanceGauge.value = contenance;
            }

        }
        else
        {
            stopArrival = false;
        }

    }


    public void Construire()
    {
        //Enleve le cout de la tombe au compteur d'argent
        Lib.instance.SetMoney(-price);

        Color col = GetComponent<SpriteRenderer>().color;
        col.a = 1;
        GetComponent<SpriteRenderer>().color = col;

        maxContenanceTxt.gameObject.SetActive(true);
        contenanceGauge.gameObject.SetActive(true);
    }
}
