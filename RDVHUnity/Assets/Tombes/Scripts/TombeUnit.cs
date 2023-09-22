using UnityEngine;

public class TombeUnit : MonoBehaviour
{
    [SerializeField] string type;
    [SerializeField] int contenanceMax;

    private int contenance;
    private float counter = -1;
    [SerializeField] Vector2 rdmCounter, rdmAjouterMort;

    [SerializeField] int currentStock;

    public Lib.phase p;

    private bool stopArrival;


    private void Start()
    {
        p = Lib.phase.ARRIVAL;
    }

    private void Update()
    {
        if (p == Lib.phase.ARRIVAL)
        {
            if (stopArrival)
            {
                if (counter < 0)
                {
                    contenance += (int)Random.Range(rdmAjouterMort.x, rdmAjouterMort.y);
                    counter = Random.Range(rdmCounter.x, rdmCounter.y);

                    if (contenance > currentStock || contenance > contenanceMax)
                        stopArrival = true;
                }
                else
                    counter -= Time.deltaTime;
            }
            
        }
    }

}
