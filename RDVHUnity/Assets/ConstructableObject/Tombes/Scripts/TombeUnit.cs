using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TombeUnit : MonoBehaviour, IConstruire
{
    private Camera cam = null;
    [SerializeField] SpriteRenderer r;
    [SerializeField] TextMeshProUGUI contenanceTxt, gainTxt, repTxt;
    [SerializeField] Slider contenanceGauge;
    [SerializeField] Animator textAnim, tombAnim, repTxtAnim;

    [Header("Variables :")]
    [SerializeField] string tombName;
    [SerializeField] int contenanceMax;
    [SerializeField] int buildPrice, inhumationPrice;
    [SerializeField] Vector2 rdmCounter, rdmAjouterMort;
    [SerializeField] int maxCurrentStock;

    [SerializeField] int timeToEmpty;
    [SerializeField] float pourcentagePerte;
    private int indexTurn;

    private int contenance;
    private float counter = -1;
    private int currentPrice;

    private bool stopArrival, construite, beginTurn;

    private void Start()
    {
        if (cam == null)
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        //Initialise les valeurs de l'UI
        contenanceTxt.text = 0 + "/" + contenanceMax.ToString();
        contenanceGauge.maxValue = contenanceMax;
        contenanceGauge.value = 0;

        //Desactive les elements de l'UI
        contenanceTxt.gameObject.SetActive(false);
        contenanceGauge.gameObject.SetActive(false);

        counter = Random.Range(rdmCounter.x, rdmCounter.y);

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

                    if (!beginTurn)
                    {
                        beginTurn = true;

                        
                        indexTurn++;  

                        if (pourcentagePerte != 0)
                        {
                            if (indexTurn >= timeToEmpty)
                            {
                                contenance -= (int)(contenance / pourcentagePerte);
                                indexTurn = 0;
                            }
                                
                        }
                        
                        

                        if (contenance > contenanceMax / 2)
                        {
                            Lib.instance.SetReputation(1);
                            
                            repTxt.text = "+1";
                            repTxtAnim.SetTrigger("Add");
                        }
                    }
                    
                        

                    counter = Random.Range(rdmCounter.x, rdmCounter.y);

                    tombAnim.SetTrigger("Add");

                    gainTxt.text = "+" + (rdmDead * currentPrice).ToString();
                    textAnim.SetTrigger("Add");
                   


                    if (contenance + rdmDead >= maxCurrentStock || contenance + rdmDead >= contenanceMax)
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
            beginTurn = false;

            //Actualise le prix d'inhumation
            if (Lib.instance.p == Lib.phase.BUILD)
            {
                if (Lib.instance.s == Lib.state.TRACK && !construite)
                {
                    transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
                }

                currentPrice = Lib.instance.GetTombPrice(tombName);
            }
                

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
                n = (int)(14 - (currentPrice + Lib.instance.reputationCounter));
                break;
            case "Caveau":
                n = (int)(6 - currentPrice + Lib.instance.reputationCounter);
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
        tombAnim.SetTrigger("Add");

        contenanceTxt.gameObject.SetActive(true);
        contenanceGauge.gameObject.SetActive(true);
        construite = true;
        Lib.instance.s = Lib.state.TRACK;
    }
}
