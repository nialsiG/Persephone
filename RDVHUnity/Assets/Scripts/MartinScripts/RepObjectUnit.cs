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
            Lib.instance.SetReputation(reputationGain);
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

    void Update()
    {
        if (Lib.instance.p == Lib.phase.ARRIVAL)
        {
            if (!endTour)
            {
                if (gainRepContinu)
                {
                    Lib.instance.SetMoney(-salaire * nbPersonnel);

                    if (reputationGain != 0)
                    {
                        Lib.instance.SetReputation(reputationGain);
                        textAnim.SetTrigger("Add");
                    }
                    
                    if (salaire != 0)
                    {
                        moneyTxt.text = "-" + (salaire * nbPersonnel).ToString();
                        spriteAnim.SetTrigger("Add");
                        moneyAnim.SetTrigger("Add");
                        logoAnim.SetTrigger("Add");
                    }
                    
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
        Lib.instance.SetReputation(reputationGain);
        Lib.instance.SetMoney(-price);

        if (!gainRepContinu)
            this.enabled = false;

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
