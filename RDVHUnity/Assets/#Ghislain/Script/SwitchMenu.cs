using UnityEngine;

public class SwitchMenu : MonoBehaviour
{

    [SerializeField] GameObject _previousMenu;
    [SerializeField] GameObject _nextMenu;

    [SerializeField] Animator buttonAnim, arrowAnim;
    private bool open;

    public void GoToNextMenu()
    {
        _previousMenu.SetActive(false);
        _nextMenu.SetActive(true);
    }

    public void EnableNextMenu()
    {
        _nextMenu.SetActive(true);
    }

    public void DisablePreviousMenu()
    {
        _previousMenu.SetActive(false);
    }

    public void StartAnim()
    {
        if (!open)
        {
            buttonAnim.SetTrigger("Up");
            arrowAnim.SetTrigger("Up");

            open = true;
        }
        else
        {
            buttonAnim.SetTrigger("Down");
            arrowAnim.SetTrigger("Down");
            open = false;
        }
    }
}
