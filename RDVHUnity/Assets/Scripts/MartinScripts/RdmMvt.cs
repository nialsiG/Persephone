using System.Collections;
using UnityEngine;

public class RdmMvt : MonoBehaviour
{
    [SerializeField] Vector2 limitHor, limitVer;
    public float counter = -1, speed;
    private Vector3 dir;

    private bool changeDir;

    private void Update()
    {
        if (counter < 0)
        {
            if (!changeDir && transform.position.x > limitHor.x && transform.position.x < limitHor.y && transform.position.y > limitVer.x && transform.position.x < limitVer.y)
            {
                dir = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
                dir.Normalize();
                changeDir = true;

                StartCoroutine(Stop());                
            }
            else if (!changeDir)
            {
                dir = Vector3.zero - transform.position;
                dir.Normalize();
            }


        }
        else
            counter -= Time.deltaTime;

        GetComponent<SpriteRenderer>().flipX = (dir.x > 0);


        if (!changeDir)
            transform.Translate(dir * speed * Time.deltaTime);
        else
        {

        }
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(Random.Range(2, 3.5f));
        changeDir = false;
        counter = Random.Range(2, 5);
    }
}
