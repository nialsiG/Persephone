using UnityEngine;

public class InstantiatePref : MonoBehaviour
{
    [SerializeField] GameObject GO = null;
    [SerializeField] Transform chapelleTransform = null, cabaneTransform = null;

    private GameObject LogiGO;
    
    public void InstantiateOnMouse()
    {
        Lib.instance.CurrentObject = Instantiate(GO, transform.position, Quaternion.identity);
    }

    public void InstantiateCabane()
    {
        //Si la loge n'est pas construite
        if (!Lib.instance.logeConstruite)
        {
            //Stock la r�f�rence dans la variable et change le bool�en de Lib
            LogiGO = Instantiate(GO, cabaneTransform.position, Quaternion.identity);
            Lib.instance.logeConstruite = true;
        }
        else   //Si la loge a deja ete construite ou incremente le nombre de personnel de la loge pr�sente
            LogiGO.GetComponent<RepObjectUnit>().nbPersonnel++;
        
    }

    public void InstantiateChapelle()
    {
        Instantiate(GO, chapelleTransform.position, Quaternion.identity);
    }
}
