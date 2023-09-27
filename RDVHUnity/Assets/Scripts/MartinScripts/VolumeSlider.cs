using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider slider;

   

    public void UpdateStep()
    {
        for (int i = (int)slider.minValue; i <= slider.maxValue; i++)
        {
            if (slider.value < i && slider.value > i - 1)
            {
                slider.value = i;
            }
            else if (slider.value > i && slider.value < i + 1)
                slider.value = i + 1;

        }
    }

}
