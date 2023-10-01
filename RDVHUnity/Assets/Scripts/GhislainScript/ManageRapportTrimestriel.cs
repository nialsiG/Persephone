using System.Collections;
using UnityEngine;
using TMPro;

public class ManageRapportTrimestriel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _trimesterField;
    [SerializeField] TextMeshProUGUI _moneyField, _variationMoneyField;
    [SerializeField] TextMeshProUGUI _debtField;
    [SerializeField] TextMeshProUGUI _reputationField, _variationReputField;
    [SerializeField] TextMeshProUGUI _bodycountField, _variationBodyCountField;
    [SerializeField] GameObject _gameManager;
    [SerializeField] Animator StampAnim;

    private float previousMoney;
    private float previousReputation;
    private float previousBodycount;

    private void Start()    //Il faut que le rapport soit actif dans la scene sinon il prend pas les valeurs de départ
    {
        //Initialise les compteurs courants
        previousMoney = Lib.instance.moneyCounter;
        previousReputation = Lib.instance.reputationCounter;
        previousBodycount = Lib.instance.bodyCounter;

        //Desactive l'objet une fois que les compteurs sont initialisés
        this.gameObject.SetActive(false);
    }

    public void NextTrimester()
    {
        //register this semester's data
        previousMoney = Lib.instance.moneyCounter;
        previousReputation = Lib.instance.reputationCounter;
        previousBodycount = Lib.instance.bodyCounter;

        //Update semester count
        Lib.instance.semesterCounter += 1;

        //Change phase
        Lib.instance.p = Lib.phase.EVENT;

        //Disable trimester panel and start next event
        StartCoroutine(NextEvent());

        //Lance l'animation du tampon
        StampAnim.SetTrigger("Stamp");
    }

    public void UpdateTrimester()
    {
        float money = Lib.instance.moneyCounter;
        float reput = Lib.instance.reputationCounter;
        float bodies = Lib.instance.bodyCounter;

        _debtField.text = "Vous avez remboursé " + Lib.instance.moneyCounter.ToString() + "<sprite name=\"Livre\" color=#B90000> de votre prêt";

        int mVar = (int)(money - previousMoney);
        string mVarString = mVar > 0 ? "+" + mVar.ToString() : mVar.ToString();
        _moneyField.text = money.ToString() + "<sprite name=\"Livre\" color=#000000>";
        _variationMoneyField.text = mVarString + "<sprite name=\"Livre\" color=#000000>";

        int rVar = (int)(reput - previousReputation);
        string rVarString = rVar > 0 ? "+" + rVar.ToString() : rVar.ToString();
        _reputationField.text = reput.ToString() + "<sprite name=\"Chapeau\" color=#000000>";
        _variationReputField.text = rVarString + "<sprite name=\"Chapeau\" color=#000000>";

        int bVar = (int)(bodies - previousBodycount);
        string bVarString = bVar > 0 ? "+" + bVar.ToString() : bVar.ToString();
        _bodycountField.text = bodies.ToString() + "<sprite name=\"Cercueil\" color=#000000>";
        _variationBodyCountField.text = bVarString + "<sprite name=\"Cercueil\" color=#000000>";

        _trimesterField.text = (Lib.instance.semesterCounter + 1 + "/" + Lib.instance.maxTrimester).ToString();
    }

    private IEnumerator NextEvent()
    {
        yield return new WaitForSeconds(1.0f);
        _gameManager.GetComponent<ManageEvent>().NewEvent();
        SoundManager.Instance.PlayUIPaperOpen();

        //Revient a l'idle de l'animation du tampon
        StampAnim.SetTrigger("Back");
        
        //and lastly remove game object
        gameObject.SetActive(false);
    }

}
