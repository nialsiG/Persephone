using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPortal : MonoBehaviour
{
    [SerializeField] GameObject _rapportTrimestriel;
    [SerializeField] GameObject _bgTempo;

    public void GoToRapportTrimestriel()
    {
        Lib.instance.p = Lib.phase.ARRIVAL;
        _bgTempo.SetActive(true);
        StartCoroutine(NextTrimester());
    }

    private IEnumerator NextTrimester()
    {
        yield return new WaitForSeconds(3.0f);
        _bgTempo.SetActive(false);
        _rapportTrimestriel.SetActive(true);
        _rapportTrimestriel.GetComponent<ManageRapportTrimestriel>().UpdateTrimester();
        SoundManager.Instance.PlayUIPaperOpen();
    }


}
