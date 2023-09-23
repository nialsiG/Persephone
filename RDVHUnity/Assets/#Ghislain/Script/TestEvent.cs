using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    [SerializeField] GameObject _eventPanel;
    [SerializeField] int _minRange;
    [SerializeField] int _maxRange;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ManageEvent manageEvent = _eventPanel.GetComponent<ManageEvent>();
            manageEvent.GenerateEvent(_minRange, _maxRange);
        }
    }
}
