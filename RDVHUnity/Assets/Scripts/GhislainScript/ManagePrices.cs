using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManagePrices : MonoBehaviour
{
    [Header("Caveau")]
    [SerializeField] TextMeshProUGUI _prixCaveau;
    [SerializeField] int _minPrixCaveau;
    [SerializeField] int _maxPrixCaveau;
    [Header("Tombe familiale")]
    [SerializeField] TextMeshProUGUI _prixFamiliale;
    [SerializeField] int _minPrixFamiliale;
    [SerializeField] int _maxPrixFamiliale;
    [Header("Tombe commune")]
    [SerializeField] TextMeshProUGUI _prixCommune;
    [SerializeField] int _minPrixCommune;
    [SerializeField] int _maxPrixCommune;
    private void Start()
    {
        UpdateTextFields();
    }


    public void IncreaseCaveau()
    {
        if (Lib.instance.priceCaveau < _maxPrixCaveau)
        {
            Lib.instance.priceCaveau += 1;
            SoundManager.Instance.PlayMoney();
        }
        else
        {
            SoundManager.Instance.PlayUICantModify();
        }
        UpdateTextFields();
    }
    public void DecreaseCaveau()
    {
        if(Lib.instance.priceCaveau > _minPrixCaveau)
        {
            Lib.instance.priceCaveau -= 1;
            SoundManager.Instance.PlayMoney();
        }
        else
        {
            SoundManager.Instance.PlayUICantModify();
        }
        UpdateTextFields();
    }

    public void IncreaseFamiliale()
    {
        if (Lib.instance.priceFamiliale < _maxPrixFamiliale)
        {
            Lib.instance.priceFamiliale += 1;
            SoundManager.Instance.PlayMoney();

        }
        else
        {
            SoundManager.Instance.PlayUICantModify();
        }
        UpdateTextFields();
    }
    public void DecreaseFamiliale()
    {
        if (Lib.instance.priceFamiliale > _minPrixFamiliale)
        {
            Lib.instance.priceFamiliale -= 1;
            SoundManager.Instance.PlayMoney();
        }
        else
        {
            SoundManager.Instance.PlayUICantModify();
        }
        UpdateTextFields();
    }

    public void IncreaseCommune()
    {
        if (Lib.instance.priceCommune < _maxPrixCommune)
        {
            Lib.instance.priceCommune += 1;
            SoundManager.Instance.PlayMoney();
        }
        else
        {
            SoundManager.Instance.PlayUICantModify();
        }
        UpdateTextFields();
    }
    public void DecreaseCommune()
    {
        if (Lib.instance.priceCommune > _minPrixCommune)
        {
            Lib.instance.priceCommune -= 1;
            SoundManager.Instance.PlayMoney();
        }
        else
        {
            SoundManager.Instance.PlayUICantModify();
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
