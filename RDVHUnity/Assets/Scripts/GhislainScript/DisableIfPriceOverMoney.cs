using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisableIfPriceOverMoney : MonoBehaviour
{
    private Button _button;
    [SerializeField] int _price;

    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_price > Lib.instance.moneyCounter)
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
        }

    }
}
