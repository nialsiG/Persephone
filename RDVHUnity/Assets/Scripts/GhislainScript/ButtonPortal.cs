using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPortal : MonoBehaviour
{
    [SerializeField] GameObject _rapportTrimestriel;
    [SerializeField] GameObject _bgTempo;
    [SerializeField] Sprite portalSprite, contourPortal;
    private bool isWaiting;
    private float counter;

    private void Start()
    {
        isWaiting = false;
        counter = 0;
    }
    private void Update()
    {
        if (isWaiting)
        {
            counter -= Time.deltaTime;

            if (counter < 0)
            {
                isWaiting = false;
                
                // Show rapport trimestriel:
                _bgTempo.SetActive(false);
                _rapportTrimestriel.SetActive(true);
                _rapportTrimestriel.GetComponent<ManageRapportTrimestriel>().UpdateTrimester();
                SoundManager.Instance.PlayEndTurn();
            }
        }
    }

    public void GoToRapportTrimestriel()
    {
        //StartCoroutine(NextTrimester());
        isWaiting = true;
        counter = 4; //4 seconds
        //Put a transparent BG over the scene during the arrival not to let the player click
        _bgTempo.SetActive(true);
        Lib.instance.p = Lib.phase.ARRIVAL;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /*private IEnumerator NextTrimester()
    {
        yield return new WaitForSeconds(4.0f);
        _bgTempo.SetActive(false);
        _rapportTrimestriel.SetActive(true);
        _rapportTrimestriel.GetComponent<ManageRapportTrimestriel>().UpdateTrimester();
        SoundManager.Instance.PlayUIPaperOpen();
    }*/


}
