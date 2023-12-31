using System.Collections;
using TMPro;
using UnityEngine;

public class RepObjectUnit : MonoBehaviour, IConstruire
{
    private Camera cam = null;
    [Header("Game objects")]
    [SerializeField] SpriteRenderer r;
    [SerializeField] Animator textAnim, spriteAnim, moneyAnim, logoAnim;
    [SerializeField] TextMeshProUGUI repTxt, moneyTxt;

    public int nbPersonnel;
    public enum _type { Loge, Chapelle, Fleur }
    [Header("Data")]
    [SerializeField] _type type;
    [SerializeField] int reputationGain, price, salaire;
    [SerializeField] bool gainRepContinu;

    //To find price
    public int Price => price;

    private bool endTour, construite;
    [Header("Random sprite")]
    [SerializeField] bool rdmSprite, followMouse;
    [SerializeField] Sprite[] tabSprite = new Sprite[3];

    void Start()
    {
        if (cam == null)
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        if (rdmSprite)
        {
            r.sprite = tabSprite[Random.Range(0, tabSprite.Length)];
        }

        //Pour les cabanes uniquements qui sont directement instanciées
        if (!followMouse)
        {
            SoundManager.Instance.PlayUIBuildStructure();
            Color col = r.color;
            col.a = 1;
            r.color = col;

            Lib.instance.SetMoney(-price);

            if (reputationGain != 0)
            {
                //repTxt.text = "+" + reputationGain.ToString();
                textAnim.SetTrigger("Add");
            }
            
            moneyTxt.text = "-" + price.ToString();
            
            spriteAnim.SetTrigger("Add");
            moneyAnim.SetTrigger("Add");
            logoAnim.SetTrigger("Add");
        }


        //Chapelle
        if (type == _type.Chapelle && !Lib.instance.isChapelle)
        {
            if (Lib.instance.reputationCounter + reputationGain < 20)
                Lib.instance.SetReputation(reputationGain);
            else
                Lib.instance.reputationCounter = 20;
            //repTxt.text = "+" + reputationGain.ToString();
            textAnim.SetTrigger("Add");

            Debug.Log("Chapelle construite !");
            Lib.instance.isChapelle = true;
            Lib.instance.nextTutoPhase.Invoke();
        }
    }

    public void AddPersonnel()
    {
        Lib.instance.SetMoney(-salaire * nbPersonnel);
    }

    IEnumerator GainRepContinu()
    {
        yield return new WaitForSeconds(3f);
        if (reputationGain != 0)
        {
            if (Lib.instance.reputationCounter + reputationGain < 20)
                Lib.instance.SetReputation(reputationGain);
            else
                Lib.instance.reputationCounter = 20;
            textAnim.SetTrigger("Add");
        }

        if (salaire != 0)
        {
            Lib.instance.SetMoney(-salaire * nbPersonnel);
            moneyTxt.text = "-" + (salaire * nbPersonnel).ToString();
            spriteAnim.SetTrigger("Add");
            moneyAnim.SetTrigger("Add");
            logoAnim.SetTrigger("Add");
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
                    //Added a coroutine so the player gains money before !
                    StartCoroutine(GainRepContinu());
                }

                endTour = true;
            }
        }
        else
        {
            if (Lib.instance.p == Lib.phase.BUILD)
            {
                if (Lib.instance.s == Lib.state.TRACK && !construite && followMouse)
                {
                    transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
                }

            }

            endTour = false;
        }
            
    }

    public void Construire()
    {
        if (Lib.instance.reputationCounter + reputationGain < 20)
            Lib.instance.SetReputation(reputationGain);
        else
            Lib.instance.reputationCounter = 20;
        Lib.instance.SetMoney(-price);

        if (!gainRepContinu)
            this.enabled = false;

        if (type == RepObjectUnit._type.Fleur)        
            Lib.instance.totalFlower++;
            

        Color col = r.GetComponent<SpriteRenderer>().color;
        col.a = 1;
        r.GetComponent<SpriteRenderer>().color = col;
        construite = true;
        textAnim.SetTrigger("Add");
        spriteAnim.SetTrigger("Add");

        Lib.instance.s = Lib.state.EMPTY;

        SoundManager.Instance.PlayUIBuildFlower();
    }
}
