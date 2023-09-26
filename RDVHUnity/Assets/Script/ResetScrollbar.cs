using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetScrollbar : MonoBehaviour
{
    [SerializeField] GameObject _scrollbar;

    //Call this function everytime the player opens the building menu
    public void SetPositionToLeft()
    {
        _scrollbar.GetComponent<Scrollbar>().value = 0;
    }    
    
    //Call this function during the tutorial, to better locate the chapel?
    public void SetPositionToRight()
    {
        _scrollbar.GetComponent<Scrollbar>().value = 1;
    }
}
