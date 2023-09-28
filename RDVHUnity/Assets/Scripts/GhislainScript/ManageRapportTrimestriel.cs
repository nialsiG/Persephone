using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageRapportTrimestriel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _trimesterField;
    [SerializeField] TextMeshProUGUI _moneyField;
    [SerializeField] TextMeshProUGUI _debtField;
    [SerializeField] TextMeshProUGUI _reputationField;
    [SerializeField] TextMeshProUGUI _bodycountField;
    [SerializeField] GameObject _gameManager;
    [SerializeField] Animator StampAnim;

    private float previousMoney;
    private float previousReputation;
    private float previousBodycount;

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

        _debtField.text = Lib.instance.Debt.ToString();
        _moneyField.text = (money + "(" + (money - previousMoney) + ")").ToString();
        _reputationField.text = (reput + "(" + (reput - previousReputation) + ")").ToString();
        _bodycountField.text = (bodies + "(" + (bodies - previousBodycount) + ")").ToString();
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
