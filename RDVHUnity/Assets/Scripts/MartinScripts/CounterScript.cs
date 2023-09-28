using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CounterScript : MonoBehaviour
{
    [SerializeField] Slider repSlider;
    [SerializeField] TextMeshProUGUI moneyCounter;

    private void Start()
    {
        repSlider.maxValue = 20;
        repSlider.value = 0;
    }

    private void Update()
    {
        repSlider.value = Lib.instance.reputationCounter;
        moneyCounter.text = Lib.instance.moneyCounter.ToString();
    }
}
