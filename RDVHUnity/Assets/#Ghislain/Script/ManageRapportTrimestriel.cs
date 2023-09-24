using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageRapportTrimestriel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _trimesterField;
    [SerializeField] TextMeshProUGUI _textField;
    [SerializeField] string _texteAccroche;
    [SerializeField] GameObject _eventMenu;

    private float previousMoney;
    private float previousReputation;
    private float previousDebt;

    public void UpdateTrimester()
    {
        float money = Lib.instance.moneyCounter;
        float reput = Lib.instance.reputationCounter;
        float debt = Lib.instance.dept;
        _textField.text = (_texteAccroche + 
            "\nArgent: " + money + "(" + (money - previousMoney) + ")" + "Livres." + 
            "\nDette: " + debt + "(" + (debt - previousDebt) + ")" + "Livres." +
            "\nRéputation: " + reput + "(" + (reput - previousReputation) + ")" + "Livres.").ToString();
        _trimesterField.text = ("TRIMESTRE " + Lib.instance.semesterCounter + 1).ToString();
    }

    public void NextTrimester()
    {
        //register this semester's data
        previousMoney = Lib.instance.moneyCounter;
        previousReputation = Lib.instance.reputationCounter;
        previousDebt = Lib.instance.dept;

        //Update semester count
        Lib.instance.semesterCounter += 1;

        //Decide the range of the next events


        //Disable trimester panel
        gameObject.SetActive(false);
        _eventMenu.SetActive(true);
        _eventMenu.GetComponent<ManageEvent>().NewEvent();
    }
}
