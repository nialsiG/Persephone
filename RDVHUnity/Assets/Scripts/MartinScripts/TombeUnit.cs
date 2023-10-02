using TMPro;
using UnityEngine;

public class TombeUnit : MonoBehaviour, IConstruire
{
    private Camera cam = null;
    [SerializeField] SpriteRenderer r;
    [SerializeField] TextMeshProUGUI contenanceTxt, gainTxt, repTxt;
    [SerializeField] Animator textAnim, tombAnim, repTxtAnim, logoAnim;

    [Header("Variables :")]
    [SerializeField] string tombName;
    [SerializeField] int contenanceMax;
    [SerializeField] Color colorHalfFull;
    [SerializeField] Color colorFull;
    [SerializeField] int buildPrice;
    [SerializeField] Vector2 rdmCounter, rdmAjouterMort;
    [SerializeField] int maxCurrentStock;
    private int currentStock;
    private bool isAboveHalf;
    
    [SerializeField] int timeToEmpty;
    [SerializeField] float pourcentagePerte;
    private int indexTurn;

    private int contenance;
    private float counter = -1;
    private int currentPrice;

    public int Price => currentPrice;

    private bool stopArrival, construite, beginTurn;

    private void Start()
    {
        if (cam == null)
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        //Initialise les valeurs de l'UI
        contenanceTxt.text = 0 + "/" + contenanceMax.ToString();
        
        //Desactive les elements de l'UI
        contenanceTxt.gameObject.SetActive(false);

        //counter = Random.Range(rdmCounter.x, rdmCounter.y);
    }


    private void Update()
    {
        if (Lib.instance.p == Lib.phase.ARRIVAL && construite)
        {
            if (!beginTurn) //permet de s'assurer que les évènements ne sont appelés qu'une fois par phase d'arrival
            {
                beginTurn = true;
                indexTurn++;
                maxCurrentStock = SetCurrentStock(tombName);
                currentStock = 0;

                // D'abord on décide s'il faut vider les tombes communes
                // ...déplacé ici car il faut d'abord vider les tombes, puis les remplir
                if (tombName == "Commune")
                {
                    if (contenance >= contenanceMax)
                    {
                        contenance -= (int)(contenanceMax / Random.Range(pourcentagePerte, 8));
                        //contenance -= maxCurrentStock;
                        //contenanceTxt.color = colorHalfFull;
                        indexTurn = timeToEmpty;
                    }
                }

                // 2. Ensuite si la tombe n'est pas pleine, on reset les paramètres nécessaires
                if (contenance < contenanceMax)
                {
                    counter = Random.Range(rdmCounter.x, rdmCounter.y); //le compteur entre les arrivées
                    currentStock = 0; //le stock de morts arrivés ce tour-ci
                    
                    //Enfin, on laisse entrer les morts
                    stopArrival = false;
                }

            }
            
            // 3. Si elle est pas pleine, on calcule le nombre de morts qui arrivent et on enclenche le processus
            if (!stopArrival)
            {
                counter -= Time.deltaTime;
                
                //Arrivée progressive
                if (counter < 0)
                {
                    // Buffer du nombre de morts qui devraient arriver durant ce batch
                    int rdmDead = (int)Random.Range(rdmAjouterMort.x, rdmAjouterMort.y);

                    //Soit le nombre de mort est atteint
                    if (currentStock + rdmDead >= maxCurrentStock)
                    {
                        AddDead(maxCurrentStock - currentStock);
                        stopArrival = true;
                    }

                    //...OU si la tombe est pleine...
                    else if (contenance + rdmDead >= contenanceMax)
                    {
                        stopArrival = true;
                        AddDead(contenanceMax - contenance);
                        contenanceTxt.color = colorFull; //attention, apres AddDead() car cette dernière change la couleur si > contenanceMax/2
                    }

                    //...soit on rajoute des morts
                    else
                    {
                        AddDead(rdmDead);
                        //...et on restarte le compteur pour l'arrivée progressive
                        counter = Random.Range(rdmCounter.x, rdmCounter.y);
                    }
                }
    
            }
            
        }
        
        //Si c'est pas la phase d'arrival, on peut actualiser le prix d'inhumation
        else
        {
            beginTurn = false;
            
            if (Lib.instance.p == Lib.phase.BUILD)
            {
                if (Lib.instance.s == Lib.state.TRACK && !construite)
                {
                    transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
                }

                //Actualise le prix d'inhumation
                currentPrice = Lib.instance.GetTombPrice(tombName);   
            }
        }
    }

    private void AddDead(int amount)
    {
        currentStock += amount;
        contenance += amount;
        Lib.instance.bodyCounter += amount; //on rajoute des morts au total du cimetière
        Lib.instance.SetMoney(amount * currentPrice); //rentrées d'argent par inhumationn, note : déplacé plus bas car on ne veut pas gagner d'argent si les morts ne rentrent pas
                                                      
        //Animation
        tombAnim.SetTrigger("Add");
        gainTxt.text = "+" + (amount * currentPrice).ToString();
        textAnim.SetTrigger("Add");
        logoAnim.SetTrigger("Add");

        //Son
        SoundManager.Instance.PlayTombPopup();

        //Update de la fenêtre de contenance sur les tombes
        contenanceTxt.text = contenance.ToString() + "/" + contenanceMax.ToString();

        //Tombe plus qu'à moitié plein ?
        if (contenance >= contenanceMax / 2)
        {
            contenanceTxt.color = colorHalfFull;

            if (tombName == "Caveau" && !isAboveHalf)
            {
                isAboveHalf = true;
                if (Lib.instance.reputationCounter < 20)
                    Lib.instance.SetReputation(1);
                repTxt.text = "+1";
                repTxtAnim.SetTrigger("Add");
            }
        }
        else
            contenanceTxt.color = Color.white;
    }

    int SetCurrentStock(string name)
    {
        int n = 0;

        switch (name)
        {
            case "Commune":
                //n = 30 - 4 * currentPrice;
                n = (int)Mathf.Ceil(5 * (3 - Mathf.Sqrt(currentPrice)));
                break;
            case "Familiale":
                //n = (int)(22 - 2 * currentPrice + Lib.instance.reputationCounter);
                n = (int)Mathf.Ceil(1.5f * Lib.instance.reputationCounter / currentPrice);
                break;
            case "Caveau":
                //n = (int)(Lib.instance.reputationCounter / 2);
                n = (int)Mathf.Ceil(1 + Lib.instance.reputationCounter / 8 - currentPrice / 20);
                break;
        }

        return n;
    }


    public void Construire()
    {
        //Enleve le cout de la tombe au compteur d'argent
        Lib.instance.SetMoney(-buildPrice);

        if (!Lib.instance.isAnyTomb)
        {
            Lib.instance.isAnyTomb = true;
            Lib.instance.nextTutoPhase.Invoke();
        }

        Lib.instance.totalTombe++;

        Color col = r.GetComponent<SpriteRenderer>().color;
        col.a = 1;
        r.GetComponent<SpriteRenderer>().color = col;
        tombAnim.SetTrigger("Add");

        contenanceTxt.gameObject.SetActive(true);
        construite = true;
        Lib.instance.s = Lib.state.EMPTY;

        SoundManager.Instance.PlayUIBuildTomb();
    }
}
