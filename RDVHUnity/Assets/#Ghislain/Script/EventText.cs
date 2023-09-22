using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventText : MonoBehaviour
{
    [SerializeField] SOevent _event;

    [SerializeField] TextMeshProUGUI _titleArea;
    [SerializeField] TextMeshProUGUI _textArea;
    [SerializeField] Image _imageArea;

    void Start()
    {
        _titleArea.text = _event.GetTitle();
        _textArea.text = _event.GetTexte();
        _imageArea.sprite = _event.GetImage();
    }
}
