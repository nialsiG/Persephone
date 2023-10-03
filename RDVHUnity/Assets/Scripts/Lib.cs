using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class Lib : MonoBehaviour
{

    public static Lib instance;
    public enum phase { EVENT, BUILD, ARRIVAL  };
    public phase p;
    public enum state { TRACK, CLIP, EMPTY };
    public state s;

    public float moneyCounter, reputationCounter, bodyCounter;
    public GameObject CurrentObject = null;

    public int priceCommune, priceFamiliale, priceCaveau, semesterCounter, nbreCaveaux, totalTombe, totalFlower;
    public bool logeConstruite;
    public bool isAnyTomb;
    public bool isChapelle;
    public bool isMultiple;
    public bool changeMod;

    [SerializeField] GameObject Rapport;

    [Header("Game over")]
    [SerializeField] GameObject Vict;
    [SerializeField] GameObject defaite;
    [SerializeField] TextMeshProUGUI _textDefeat;
    [SerializeField] string _textDefeatNoMoney, _textDefeatNoTime;
    public int maxTrimester;
    private bool isGameEnd;

    //Events pour le tutoriel
    [Header("Tutorial events")]
    public UnityEvent nextTutoPhase;

    //Faire en sorte que la dette soit private, mais qu'on puisse la lire de partout
    private float _debt;
    public float Debt => _debt;
    [SerializeField] SOSoundPool SO;
    [SerializeField] Image bg;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        Rapport.SetActive(true);
    }

    private void Start()
    {
        _debt = moneyCounter;

        // Events for the tutorial
        isChapelle = false;
        isAnyTomb = false;
        nextTutoPhase.AddListener(GetComponent<ManageTutorial>().NextTutorialPhase);

    }

    private void Update()
    {
        if (moneyCounter > Debt && !isGameEnd)
        {
            StartCoroutine(Victory());
        }

        if (moneyCounter < 0 && !isGameEnd)
        {
            _textDefeat.text = _textDefeatNoMoney;
            Defeat();
        }

        if (semesterCounter >= maxTrimester && !isGameEnd)
        {
            _textDefeat.text = _textDefeatNoTime;
            Defeat();
        }

        StampAnim();
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

    //J'ai fait une coroutine ici pour laisser le temps aux anims des tombes de se finir
    IEnumerator Victory()
    {
        isGameEnd = true;
        yield return new WaitForSeconds(2);
        SoundManager.Instance.PlayVictory();
        SoundManager.Instance.StopAmbient();
        Vict.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Defeat()
    {
        isGameEnd = true;
        SoundManager.Instance.PlayDefeat();
        SoundManager.Instance.StopAmbient();
        defaite.SetActive(true);
        Time.timeScale = 0f;
    }

    public void StampAnim()
    {
        if (changeMod)
        {
            Color col = bg.color;
            if (Input.GetButtonDown("Select"))
            {
                if (col.a < 1)
                    col.a += 0.005f;                
                
                GameObject.Find("AudioManager").GetComponent<SoundManager>().PlaySound(SO);
            }

            if (col.a > 0.15f)
                col.a += 0.005f;
            if (col.a >= 0.9f && !isGameEnd)
            {
                isGameEnd = true;
                for (int i = 0; i < bg.gameObject.transform.childCount; i++)
                    bg.gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }

            bg.color = col;

        }
    }
}
