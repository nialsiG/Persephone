using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPortal : MonoBehaviour
{
    [SerializeField] GameObject _rapportTrimestriel;

    public void GoToRapportTrimestriel()
    {
        _rapportTrimestriel.SetActive(true);
        _rapportTrimestriel.GetComponent<ManageRapportTrimestriel>().UpdateTrimester();

        Lib.instance.p = Lib.phase.ARRIVAL;
    }


}
