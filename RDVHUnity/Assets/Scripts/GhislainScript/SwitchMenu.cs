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

    //Lance les anims pour la fermeture du menu construction
    public void CloseBuildMenu()
    {
        if (buttonAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "UpBuildMenu")
        {
            buttonAnim.SetTrigger("Down");
            arrowAnim.SetTrigger("Down");
            open = false;
        }
        
    }

    public void StartAnim()
    {
        //Vérifie que l'animation joué soit bien l'idle (que le menu soit hors de l'ecran)
        if (buttonAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "IdleBuildMenu")
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
