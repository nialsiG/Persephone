using UnityEngine;

public class PretreAnim : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer s;
    private float counter;

    private void Start()
    {
        //Initialise le compteur avec un entier aléatoire entre 3 et 10
        counter = Random.Range(20, 50);
    }

    private void Update()
    {
        //Lorsque le compteur est inférieur à zero on relance une anim
        if (counter < 0)
        {
            //On vérifie que l'on est bien sur l'anim idle pour éviter d'override une autre
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle")
            {
                //On choisit un nombre aléatoire entre 0 et 2
                int rdm = Random.Range(0, 2);

                //On séléctionne une animation aléatoire
                if (rdm == 0)
                    anim.SetTrigger("Look");
                else
                {
                    anim.SetTrigger("GoOut");
                    //On change le layer pour que le sprite soit devant la chapelle
                    s.sortingOrder = 3;
                }
            }

            //On réinitialise le compteur avec les valeurs aléatoires
            counter = Random.Range(20, 50);
        }
        else 
            counter -= Time.deltaTime;

        //Si l'animation en cours est GoBehind on change le layer pour que le sprite passe derriere la chapelle
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "GoBehind")
            s.sortingOrder = 1;
    }
}
