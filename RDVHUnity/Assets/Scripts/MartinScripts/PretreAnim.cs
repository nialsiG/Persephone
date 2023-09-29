using UnityEngine;

public class PretreAnim : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer s;
    private float counter;

    private void Start()
    {
        //Initialise le compteur avec un entier al�atoire entre 3 et 10
        counter = Random.Range(20, 50);
    }

    private void Update()
    {
        //Lorsque le compteur est inf�rieur � zero on relance une anim
        if (counter < 0)
        {
            //On v�rifie que l'on est bien sur l'anim idle pour �viter d'override une autre
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle")
            {
                //On choisit un nombre al�atoire entre 0 et 2
                int rdm = Random.Range(0, 2);

                //On s�l�ctionne une animation al�atoire
                if (rdm == 0)
                    anim.SetTrigger("Look");
                else
                {
                    anim.SetTrigger("GoOut");
                    //On change le layer pour que le sprite soit devant la chapelle
                    s.sortingOrder = 3;
                }
            }

            //On r�initialise le compteur avec les valeurs al�atoires
            counter = Random.Range(20, 50);
        }
        else 
            counter -= Time.deltaTime;

        //Si l'animation en cours est GoBehind on change le layer pour que le sprite passe derriere la chapelle
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "GoBehind")
            s.sortingOrder = 1;
    }
}
