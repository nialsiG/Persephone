using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageCounters : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _reputationCounter;
    [SerializeField] TextMeshProUGUI _moneyCounter;
    [SerializeField] TextMeshProUGUI _debtCounter;

    public void UpdateCounters()
    {
        _reputationCounter.text = "Réputation:\n" + Lib.instance.reputationCounter.ToString();
        _moneyCounter.text = "Argent:\n" + Lib.instance.moneyCounter.ToString();
        _debtCounter.text = "Dette:\n" + Lib.instance.Debt.ToString();
    }

    public void ResetCounter()
    {

    }

    private void Start()
    {
        UpdateCounters();
    }
}
