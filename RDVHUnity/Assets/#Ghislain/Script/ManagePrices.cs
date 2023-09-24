using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManagePrices : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _prixCaveau;
    [SerializeField] TextMeshProUGUI _prixFamiliale;
    [SerializeField] TextMeshProUGUI _prixCommune;

    [SerializeField] int _maxPrixCaveau;
    [SerializeField] int _maxPrixFamiliale;
    [SerializeField] int _maxPrixCommune;

    private void Start()
    {
        UpdateTextFields();


    }


    public void IncreaseCaveau()
    {
        if (Lib.instance.priceCaveau < _maxPrixCaveau)
        Lib.instance.priceCaveau += 1;
        UpdateTextFields();
    }
    public void DecreaseCaveau()
    {
        if(Lib.instance.priceCaveau > 0)
        {
            Lib.instance.priceCaveau -= 1;
        }
        UpdateTextFields();
    }

    public void IncreaseFamiliale()
    {
        if (Lib.instance.priceFamiliale < _maxPrixFamiliale)
            Lib.instance.priceFamiliale += 1;
        UpdateTextFields();
    }
    public void DecreaseFamiliale()
    {
        if (Lib.instance.priceFamiliale > 0)
        {
            Lib.instance.priceFamiliale -= 1;
        }
        UpdateTextFields();
    }

    public void IncreaseCommune()
    {
        if (Lib.instance.priceCommune < _maxPrixCommune)
            Lib.instance.priceCommune += 1;
        UpdateTextFields();
    }
    public void DecreaseCommune()
    {
        if (Lib.instance.priceCommune > 0)
        {
            Lib.instance.priceCommune -= 1;
        }
        UpdateTextFields();
    }

    private void UpdateTextFields()
    {
        _prixCaveau.text = Lib.instance.priceCaveau.ToString();
        _prixFamiliale.text = Lib.instance.priceFamiliale.ToString();
        _prixCommune.text = Lib.instance.priceCommune.ToString();
    }

}
