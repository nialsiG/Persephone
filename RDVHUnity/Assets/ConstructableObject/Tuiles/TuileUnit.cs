using UnityEngine;

public class TuileUnit : MonoBehaviour
{
    [SerializeField] Camera cam = null;

    [SerializeField] float magnetismDistance;

    private bool alreadyBuilt, inBox;
    public bool magnetism;
    private void Update()
    {
        if (Lib.instance.p == Lib.phase.BUILD)
        {
            if (Lib.instance.CurrentObject != null)
            {
                Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
                
                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(pos.x, pos.y)) < magnetismDistance)
                {
                    if (inBox)
                    {
                        magnetism = true;
                        Lib.instance.CurrentObject.transform.position = transform.position;

                        if (Input.GetButtonDown("Select"))
                        {
                            alreadyBuilt = true;
                            if (Lib.instance.CurrentObject.GetComponent<IConstruire>() != null)
                                Lib.instance.CurrentObject.GetComponent<IConstruire>().Construire();

                            Lib.instance.CurrentObject = null;                            
                        }
                    }

                }
                else
                {
                    magnetism = false;
                    Lib.instance.CurrentObject.transform.position = new Vector3(pos.x, pos.y, 0);
                }
            }

                

            GetComponent<Collider2D>().enabled = !alreadyBuilt;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && !alreadyBuilt && !inBox)
        {
            inBox = true;
            print("aa");
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && inBox)
            inBox = false;
    }
}
