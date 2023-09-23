using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventText : MonoBehaviour
{
    [SerializeField] SOevent[] _events;
    [SerializeField] int _currentEventIndex;

    [SerializeField] TextMeshProUGUI _titleArea;
    [SerializeField] TextMeshProUGUI _textArea;
    [SerializeField] Image _imageArea;

    public void GenerateEvent(int rangeInf, int rangeSup)
    {
        //generate a random event...
        int randomIndex = Random.Range(rangeInf, rangeSup);
        _currentEventIndex = randomIndex;

        //...and update event panel
        UpdateEventPanel(_currentEventIndex);
    }

    public void UpdateEventPanel(int index)
    {
        _titleArea.text = _events[index].GetTitle();
        _textArea.text = _events[index].GetTexte();
        _imageArea.sprite = _events[index].GetImage();

    }

    void Start()
    {
        GenerateEvent(0, 1);
    }
}
