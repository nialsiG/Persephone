using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPortal : MonoBehaviour
{
    [SerializeField] GameObject _rapportTrimestriel;
    [SerializeField] GameObject _bgTempo;

    public void GoToRapportTrimestriel()
    {
        StartCoroutine(NextTrimester());
        
        //Put a transparent BG over the scene during the arrival not to let the player click
        _bgTempo.SetActive(true);
        Lib.instance.p = Lib.phase.ARRIVAL;
    }

    private IEnumerator NextTrimester()
    {
        yield return new WaitForSeconds(4.0f);
        _bgTempo.SetActive(false);
        _rapportTrimestriel.SetActive(true);
        _rapportTrimestriel.GetComponent<ManageRapportTrimestriel>().UpdateTrimester();
        SoundManager.Instance.PlayUIPaperOpen();
    }


}
