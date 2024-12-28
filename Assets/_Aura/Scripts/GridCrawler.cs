using UnityEngine;

public class GridCrawler : MonoBehaviour
{
    public GameObject crawler;
    int width = 5000;
    int depth = 5000;
    Vector3Int crawlerPos;

    private void Update()
    {
        //change in x with each update
        var dx = Random.Range(-1, 2);
        var dz = Random.Range(-1, 2);

        if (Random.Range(0, 2) == 0)
        {
            //move in z direction
            //limit to city depth
            if(crawlerPos.z + dz * 20 > depth || crawlerPos.z + dz * 20 < 0)
            {
                dz *= -1;
            }
            crawlerPos += new Vector3Int(0, 0, dz * 20);
        }
        else
        {
            //move in x direction
            //limit to city width
            if(crawlerPos.x + dx*20 > width || crawlerPos.x + dx * 20 < 0)
            {
                dx*= -1;    
            }
            crawlerPos += new Vector3Int(dx * 20, 0,0);

        }

        crawler.transform.position = crawlerPos;
    }
}
