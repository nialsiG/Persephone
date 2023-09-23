using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManageEvent : MonoBehaviour
{
    [SerializeField] SOevent[] _events;
    private int _previousEventIndex;
    public int PreviousIndex => _previousEventIndex;

    [SerializeField] TextMeshProUGUI _titleArea;
    [SerializeField] TextMeshProUGUI _textArea;
    [SerializeField] Image _imageArea;

    [SerializeField] TextMeshProUGUI _choice1;
    [SerializeField] TextMeshProUGUI _choice2;

    private int changeToMoney1;
    private int changeToReputation1;    
    private int changeToMoney2;
    private int changeToReputation2;

    public void GenerateEvent(int minRange, int maxRange)
    {
        //enable panel
        gameObject.SetActive(true);

        //generate a random event
        int random = Random.Range(minRange, maxRange + 1);
        _previousEventIndex = random;

        //Update event panel
        UpdateEventPanel(_previousEventIndex);

        //play event sound
    }

    public void UpdateEventPanel(int index)
    {
        _titleArea.text = _events[index].Title;
        _textArea.text = _events[index].Texte;
        _imageArea.sprite = _events[index].Image;

        _choice1.text = _events[index].TexteChoix1;
        changeToMoney1 = _events[index].ChangeToMoney1;
        changeToReputation1 = _events[index].ChangeToReputation1;

        _choice2.text = _events[index].TexteChoix2;
        changeToMoney2 = _events[index].ChangeToMoney2;
        changeToReputation2 = _events[index].ChangeToReputation2;
    }


    public void ChooseNumberOne()
    {
        Debug.Log("Money + " + changeToMoney1);
        Debug.Log("Reputation + " + changeToReputation1);
        
        gameObject.SetActive(false);
    }

    public void ChooseNumberTwo()
    {
        Debug.Log("Money + " + changeToMoney2);
        Debug.Log("Reputation + " + changeToReputation2);

        gameObject.SetActive(false);
    }

}
