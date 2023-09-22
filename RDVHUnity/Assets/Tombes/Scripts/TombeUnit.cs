using UnityEngine;

public class TombeUnit : MonoBehaviour
{
    [SerializeField] string type;
    [SerializeField] int contenanceMax;

    public enum state { ACTIVE, DESACTIVE };
    public state s;


    private void Update()
    {
        if (s == state.ACTIVE)
        {

        }
    }

}
