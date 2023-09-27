using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CounterScript : MonoBehaviour
{
    [SerializeField] Slider repSlider;
    [SerializeField] TextMeshProUGUI moneyCounter;
    [SerializeField] GameObject Vict, defaite;

    private bool isGameEnd;

    private void Start()
    {
        repSlider.maxValue = 20;
        repSlider.value = 0;
        isGameEnd = false;
    }

    private void Update()
    {
        if (Lib.instance.moneyCounter > Lib.instance.Debt && !isGameEnd)
        {
            isGameEnd = true;
            SoundManager.Instance.PlayVictory();
            Vict.SetActive(true);
        }

        if (Lib.instance.moneyCounter < 0 && !isGameEnd)
        {
            isGameEnd = true;
            SoundManager.Instance.PlayDefeat();
            defaite.SetActive(true);
        }

        repSlider.value = Lib.instance.reputationCounter;
        moneyCounter.text = Lib.instance.moneyCounter.ToString();
    }
}
