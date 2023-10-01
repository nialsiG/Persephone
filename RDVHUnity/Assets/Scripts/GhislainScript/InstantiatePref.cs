using UnityEngine;

public class InstantiatePref : MonoBehaviour
{
    [SerializeField] string type;
    [SerializeField] GameObject GO = null;
    [SerializeField] Transform chapelleTransform = null, cabaneTransform = null;

    private GameObject LogiGO;
    private int nbFlower;

    private void Update()
    {
        if (type == "Tombe")
        {
            if (Lib.instance.totalTombe == 16)
                this.gameObject.transform.parent.gameObject.SetActive(false);
        }

        if (type == "Fleur")
        {
            if (Lib.instance.totalFlower == 13)
                this.gameObject.transform.parent.gameObject.SetActive(false);
        }

        if (Input.GetButtonDown("Annuler"))
            if (Lib.instance.CurrentObject != null)
            {
                Lib.instance.s = Lib.state.EMPTY;
                Destroy(Lib.instance.CurrentObject);
                Lib.instance.CurrentObject = null;
            }
    }

    public void InstantiateOnMouse()
    {
        if (Lib.instance.s != Lib.state.TRACK)
        {
            Lib.instance.CurrentObject = Instantiate(GO, transform.position, Quaternion.identity);
            Lib.instance.s = Lib.state.TRACK;
        }
    }

    public void InstantiateCabane()
    {
        //Si la loge n'est pas construite
        if (!Lib.instance.logeConstruite)
        {
            //Stock la référence dans la variable et change le booléen de Lib
            LogiGO = Instantiate(GO, cabaneTransform.position, Quaternion.identity);
            Lib.instance.logeConstruite = true;
        }
        else   //Si la loge a deja ete construite ou incremente le nombre de personnel de la loge présente
            LogiGO.GetComponent<RepObjectUnit>().nbPersonnel++;
        
    }

    public void InstantiateChapelle()
    {
        Instantiate(GO, chapelleTransform.position, Quaternion.identity);
    }
}
