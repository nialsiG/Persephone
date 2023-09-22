using UnityEngine;

public class TombeUnit : MonoBehaviour
{
    [SerializeField] int contenanceMax;
    [SerializeField] Sprite tombSprite;
    [SerializeField] Vector2 rdmCounter, rdmAjouterMort;
    [SerializeField] int maxCurrentStock;

    private int contenance;
    private float counter = -1;

    private bool stopArrival;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = tombSprite;
    }


    private void Update()
    {
        if (Lib.instance.p == Lib.phase.ARRIVAL)
        {
            if (!stopArrival)
            {
                if (counter < 0)
                {
                    contenance += (int)Random.Range(rdmAjouterMort.x, rdmAjouterMort.y);
                    counter = Random.Range(rdmCounter.x, rdmCounter.y);

                    if (contenance > maxCurrentStock || contenance > contenanceMax)
                        stopArrival = true;
                }
                else
                    counter -= Time.deltaTime;
            }

        }
        else
            stopArrival = false;
    }

}
