using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMenu : MonoBehaviour
{

    [SerializeField] GameObject _previousMenu;
    [SerializeField] GameObject _nextMenu;

    public void GoToNextMenu()
    {
        _previousMenu.SetActive(false);
        _nextMenu.SetActive(true);
    }
}
