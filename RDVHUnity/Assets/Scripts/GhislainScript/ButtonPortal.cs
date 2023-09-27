using System.Collections;
using UnityEngine;

public class ButtonPortal : MonoBehaviour
{
    [SerializeField] GameObject _rapportTrimestriel;
    [SerializeField] GameObject _bgTempo;
    [SerializeField] Sprite portalSprite, contourPortal;

    public void GoToRapportTrimestriel()
    {
        StartCoroutine(NextTrimester());
        
        //Put a transparent BG over the scene during the arrival not to let the player click
        _bgTempo.SetActive(true);
        Lib.instance.p = Lib.phase.ARRIVAL;
    }

    private IEnumerator NextTrimester()
    {
        print("aaa");
        yield return new WaitForSeconds(4.0f);
        _bgTempo.SetActive(false);
        _rapportTrimestriel.SetActive(true);
        _rapportTrimestriel.GetComponent<ManageRapportTrimestriel>().UpdateTrimester();
        SoundManager.Instance.PlayUIPaperOpen();
    }


}
