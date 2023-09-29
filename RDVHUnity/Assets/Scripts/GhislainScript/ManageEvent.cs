using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ManageEvent : MonoBehaviour
{
    [SerializeField] GameObject _eventMenu;
    [SerializeField] SOevent[] _eventsFaciles;
    [SerializeField] SOevent[] _eventsMoyens;
    [SerializeField] SOevent[] _eventsDifficiles;
    private bool isEventsMoyensAdded;
    private bool isEventsDifficilesAdded;
    private List<SOevent> allEvents;

    [SerializeField] TextMeshProUGUI _titleArea;
    [SerializeField] TextMeshProUGUI _textArea;
    [SerializeField] Image _imageArea;

    [SerializeField] TextMeshProUGUI _choice1;
    [SerializeField] TextMeshProUGUI _choice2;

    [SerializeField] GameObject _button1;
    [SerializeField] GameObject _button2;
    //[SerializeField] GameObject _gameManager;

    private int changeToMoney1;
    private int changeToReputation1;    
    private int changeToMoney2;
    private int changeToReputation2;

    private void Start()
    {
        allEvents = new List<SOevent>();
        foreach (SOevent e in _eventsFaciles)
        {
            allEvents.Add(e);
        }
    }

    public void NewEvent()
    {
        //Get the turn
        int turn = Lib.instance.semesterCounter;

        //Set the range
        switch (turn)
        {
            case  > 15:
                if (!isEventsDifficilesAdded)
                {
                    isEventsDifficilesAdded = true;
                    foreach (SOevent e in _eventsDifficiles)
                    {
                        allEvents.Add(e);
                    }
                }
                break;
            case  > 10:
                if (!isEventsMoyensAdded)
                {
                    isEventsMoyensAdded = true;
                    foreach (SOevent e in _eventsMoyens)
                    {
                        allEvents.Add(e);
                    }
                }
                break;
            default:
                break;
        }
        //...and generate random event
        GenerateEvent(0, allEvents.Count);

    }


    public void GenerateEvent(int minRange, int maxRange)
    {
        //enable panel
        _eventMenu.SetActive(true);

        //generate a random event
        int random = Random.Range(minRange, maxRange);
        
        //Update event panel
        UpdateEventPanel(random);

        //play event sound
        if (allEvents[random].Sound != null)
        {
            SoundManager.Instance.PlaySound(allEvents[random].Sound);
        }

        //LASTLY remove the event from the list
        allEvents.Remove(allEvents[random]);
    }

    public void UpdateEventPanel(int index)
    {
        _titleArea.text = allEvents[index].Title;
        _textArea.text = allEvents[index].Texte;
        _imageArea.sprite = allEvents[index].Image;


        if (allEvents[index].TexteChoix2 == "")
        {
            _button2.SetActive(false);
        }
        else
        {
            _button2.SetActive(true);
            _choice2.text = allEvents[index].TexteChoix2;
            changeToMoney2 = allEvents[index].ChangeToMoney2;
            changeToReputation2 = allEvents[index].ChangeToReputation2;
        }


        if (allEvents[index].TexteChoix1 == "")
        {
            if (allEvents[index].TexteChoix2 == "")
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
            _choice1.text = allEvents[index].TexteChoix1;
            changeToMoney1 = allEvents[index].ChangeToMoney1;
            changeToReputation1 = allEvents[index].ChangeToReputation1;
        }

    }


    public void ChooseNumberOne()
    {
        Debug.Log("Money + " + changeToMoney1);
        Debug.Log("Reputation + " + changeToReputation1);
        Lib.instance.SetMoney(changeToMoney1);
        Lib.instance.SetReputation(changeToReputation1);

        Lib.instance.p = Lib.phase.BUILD;

        _eventMenu.SetActive(false);
    }

    public void ChooseNumberTwo()
    {
        Debug.Log("Money + " + changeToMoney2);
        Debug.Log("Reputation + " + changeToReputation2);
        Lib.instance.SetMoney(changeToMoney2);
        Lib.instance.SetReputation(changeToReputation2);

        Lib.instance.p = Lib.phase.BUILD;

        _eventMenu.SetActive(false);
    }

}
