using UnityEngine;

public class InstantiatePref : MonoBehaviour
{
    [SerializeField] GameObject GO = null;
    [SerializeField] Transform chapelleTransform = null, cabaneTransform = null;

    public void InstantiateOnMouse()
    {
        Lib.instance.CurrentObject = Instantiate(GO, transform.position, Quaternion.identity);
    }

    public void InstantiateCabane()
    {
        Instantiate(GO, cabaneTransform.position, Quaternion.identity);
    }

    public void InstantiateChapelle()
    {
        Instantiate(GO, chapelleTransform.position, Quaternion.identity);
    }
}
