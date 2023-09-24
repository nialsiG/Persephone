using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CounterScript : MonoBehaviour
{
    [SerializeField] Slider repSlider;
    [SerializeField] TextMeshProUGUI moneyCounter;

    [SerializeField] GameObject Vict, defaite;

    private void Start()
    {
        repSlider.maxValue = 20;
        repSlider.value = 0;

    }

    private void Update()
    {
        if (Lib.instance.moneyCounter >= 3001)
        {
            Vict.SetActive(true);
        }

        if (Lib.instance.moneyCounter < 0)
        {
            defaite.SetActive(true);
        }

        repSlider.value = Lib.instance.reputationCounter;
        moneyCounter.text = Lib.instance.moneyCounter.ToString();
    }
}
