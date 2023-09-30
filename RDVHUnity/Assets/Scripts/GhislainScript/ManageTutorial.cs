using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManageTutorial : MonoBehaviour
{
    [Header("Tutorial menu")]
    [SerializeField] GameObject _menu;
    [SerializeField] Image _image;
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _text;

    [Header("Content")]
    [SerializeField] SOevent[] _event;

    [Header("Objects affected by events")]
    [SerializeField] GameObject _buttonPortal;
    [SerializeField] GameObject _buttonChapel;
    [SerializeField] GameObject[] _buttonsTomb;
    [SerializeField] GameObject[] _buttonsOther;


    public enum _phase { None, Tuto1, Tuto2, Tuto3 }
    public _phase currentPhase { get; private set; }
    private void Start()
    {
        currentPhase = _phase.None;
    }
    public void NextTutorialPhase()
    {
        currentPhase++;
        Debug.Log("Tutoriel phase: " + currentPhase);

        switch (currentPhase)
        {
            case _phase.None:
                break;
            case _phase.Tuto1:
                StartCoroutine(DisplayTutorial(1f, false));
                _buttonPortal.GetComponent<Button>().interactable = false;
                foreach (GameObject b in _buttonsOther)
                {
                    b.SetActive(false);
                }
                foreach (GameObject b in _buttonsTomb)
                {
                    b.SetActive(false);
                }
                break;
            case _phase.Tuto2:
                StartCoroutine(DisplayTutorial(1f, false));
                _buttonChapel.SetActive(false);
                foreach (GameObject b in _buttonsTomb)
                {
                    b.SetActive(true);
                }
                break;
            case _phase.Tuto3:
                StartCoroutine(DisplayTutorial(1f, true));
                foreach (GameObject b in _buttonsOther)
                {
                    b.SetActive(true);
                }
                break;
            default:
                break;
        }

    }
    private IEnumerator DisplayTutorial(float time, bool portalInteractable)
    {
        yield return new WaitForSeconds(time);
        UpdateTutorial();
        _buttonPortal.GetComponent<Button>().interactable = portalInteractable;
    }

    private void UpdateTutorial()
    {
        _menu.SetActive(true);
        _image.sprite = _event[(int)currentPhase - 1].Image;
        _title.text = _event[(int)currentPhase - 1].Title;
        _text.text = _event[(int)currentPhase - 1].Texte;
        SoundManager.Instance.PlaySound(_event[(int)currentPhase - 1].Sound);
    }


}
