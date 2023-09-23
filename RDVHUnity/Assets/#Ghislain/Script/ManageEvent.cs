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

    [SerializeField] GameObject _button1;
    [SerializeField] GameObject _button2;
    [SerializeField] GameObject _gameManager;

    private int changeToMoney1;
    private int changeToReputation1;    
    private int changeToMoney2;
    private int changeToReputation2;

    public void NewEvent()
    {
        //Get turn
        int turn = Lib.instance.semesterCounter;

        //Set the range
        int minRange = 0;
        int maxRange = 0;
        switch (turn)
        {
            case 0:
                minRange = 0;
                maxRange = 0;
                break;
            case 1:
                minRange = 1;
                maxRange = 1;
                break;
            case 2:
                minRange = 2;
                maxRange = 2;
                break;
            case > 3:
                minRange = 0;
                maxRange = 0;
                break;
            default:
                break;
        }
        //...and generate random event
        GenerateEvent(minRange, maxRange);

    }


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
        if (_events[_previousEventIndex].Sound != null)
        {
            SoundManager.Instance.PlaySound(_events[_previousEventIndex].Sound);
        }
    }

    public void UpdateEventPanel(int index)
    {
        _titleArea.text = _events[index].Title;
        _textArea.text = _events[index].Texte;
        _imageArea.sprite = _events[index].Image;


        if (_events[index].TexteChoix2 == "")
        {
            _button2.SetActive(false);
        }
        else
        {
            _button2.SetActive(true);
            _choice2.text = _events[index].TexteChoix2;
            changeToMoney2 = _events[index].ChangeToMoney2;
            changeToReputation2 = _events[index].ChangeToReputation2;
        }


        if (_events[index].TexteChoix1 == "")
        {
            if (_events[index].TexteChoix2 == "")
            {
                _choice1.text = "Suivant";
                changeToMoney1 = 0;
                changeToReputation1 = 0;
            }
            else _button1.SetActive(false);
        }
        else
        {
            _button1.SetActive(true);
            _choice1.text = _events[index].TexteChoix1;
            changeToMoney1 = _events[index].ChangeToMoney1;
            changeToReputation1 = _events[index].ChangeToReputation1;
        }

    }


    public void ChooseNumberOne()
    {
        Debug.Log("Money + " + changeToMoney1);
        Debug.Log("Reputation + " + changeToReputation1);
        Lib.instance.SetMoney(changeToMoney1);
        Lib.instance.SetReputation(changeToReputation1);

        OnChoosingAnswer();
    }

    public void ChooseNumberTwo()
    {
        Debug.Log("Money + " + changeToMoney2);
        Debug.Log("Reputation + " + changeToReputation2);
        Lib.instance.SetMoney(changeToMoney2);
        Lib.instance.SetReputation(changeToReputation2);

        OnChoosingAnswer();
    }

    private void OnChoosingAnswer()
    {
        //Update the counters
        _gameManager.GetComponent<ManageCounters>().UpdateCounters();
        //Update semester count
        Lib.instance.semesterCounter += 1;
        //disable panel
        gameObject.SetActive(false);
    }

}
