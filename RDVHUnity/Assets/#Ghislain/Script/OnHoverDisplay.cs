using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnHoverDisplay : MonoBehaviour
{
    [SerializeField] GameObject _menuContextuel;
    [SerializeField] TextMeshProUGUI _textArea;
    [SerializeField] string _text;

    private void Start()
    {
        _textArea.text = _text;
        _menuContextuel.SetActive(false);

    }

    public void EnableMenuContextuel()
    {
        _menuContextuel.SetActive(true);
        StartCoroutine(HideAfter(7.0f));
    }
    public void DisableMenuContextuel()
    {
        _menuContextuel.SetActive(false);
    }

    private IEnumerator HideAfter(float time)
    {
        yield return new WaitForSeconds(time);
        if (_menuContextuel.activeSelf)
        {
            _menuContextuel.SetActive(false);
        }
    }
}
