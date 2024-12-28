
using System.Collections.Generic;
using UnityEngine;

public class GridCrawler2 : MonoBehaviour
{
    public GameObject crawler;
    public GameObject straightRoadPiece;
    public GameObject cornerRoadPiece;

    //size of city restriction values
    int width = 100;
    int depth = 100;

    //crawling parameters
    Vector3Int crawlerPos;
    Vector3 dir = new Vector3(0, 0, 1);
    Vector3 neutral = new Vector3(0,0,1);

    //testing crawling operation restrictions
    //allows us to limit crawling for easier testing
    int counter = 50;
    bool doneCrawling = false;

    //list to hold all road pieces laid on the map
    List<RoadPiece> roadPieces = new List<RoadPiece> ();
    void Update()
    {
        if (doneCrawling)
            return;

        if(counter > 0)
        {

            Crawl();
            counter--;
        }
        else
        {
            doneCrawling = true;
        }
    }

    private void Crawl()
    {
        int randomTurn = UnityEngine.Random.Range(0, 3);
        GameObject go;
        float rotationToApply;
        RoadPiece newRoad;

        if (randomTurn == 0)
        {
            dir = Quaternion.Euler(0, -90, 0) * dir;
            //calculate rotation to apply
            rotationToApply = Vector3.SignedAngle(neutral, dir, transform.up) + 90;
            //instantiate place and rotate a corner piece appropriately
            go = Instantiate(cornerRoadPiece, crawlerPos, Quaternion.identity);
            go.transform.Rotate(0f,rotationToApply,0f);

            //create new road piece data
            newRoad = new RoadPiece()
            {
                position = crawlerPos,
                yRotation = (int)Mathf.Round(go.transform.rotation.eulerAngles.y / 90) * 90,
                roadObject = go,
                type = RoadPiece.RoadType.CORNER
            };

        }
        else if (randomTurn == 1)
        {
            dir = Quaternion.Euler(0, 90, 0) * dir;
            rotationToApply = Vector3.SignedAngle(neutral, dir, transform.up) + 180;
            //instantiate place and rotate a corner piece appropriately
            go = Instantiate(cornerRoadPiece, crawlerPos, Quaternion.identity);
            go.transform.Rotate(0f, rotationToApply, 0f);

            //create new road piece data
            newRoad = new RoadPiece()
            {
                position = crawlerPos,
                yRotation = (int)Mathf.Round(go.transform.rotation.eulerAngles.y / 90) * 90,
                roadObject = go,
                type = RoadPiece.RoadType.CORNER
            };
        }
        else
        {
            rotationToApply = Vector3.SignedAngle(neutral, dir, transform.up);
            //instantiate place and rotate a corner piece appropriately
            go = Instantiate(straightRoadPiece, crawlerPos, Quaternion.identity);
            go.transform.Rotate(0f, rotationToApply, 0f);

            //create new road piece data
            newRoad = new RoadPiece()
            {
                position = crawlerPos,
                yRotation = (int)Mathf.Round(go.transform.rotation.eulerAngles.y / 90) * 90,
                roadObject = go,
                type = RoadPiece.RoadType.STRAIGHT
            };
        }

        //add the new roadpiece to the list of roadpiece data
        AddWithNoDuplication(newRoad);

        //spawn an extra straight piece to bridge gaps
        Vector3Int straightPos = crawlerPos +  Vector3Int.RoundToInt(dir * 10 );
        rotationToApply = Vector3.SignedAngle(neutral, dir, transform.up);
        go = Instantiate(straightRoadPiece, straightPos, Quaternion.identity);
        go.transform.Rotate(0f, rotationToApply, 0f);

        //create new road piece data
        newRoad = new RoadPiece()
        {
            position = straightPos,
            yRotation = (int)Mathf.Round(go.transform.rotation.eulerAngles.y / 90) * 90,
            roadObject = go,
            type = RoadPiece.RoadType.STRAIGHT,
        };


        //add the new roadpiece to the list of roadpiece data
        AddWithNoDuplication(newRoad);

        crawlerPos += Vector3Int.RoundToInt(dir * 20);
        crawler.transform.position = crawlerPos;
    }

    private void AddWithNoDuplication(RoadPiece _piece)
    {
        bool found = false;
        foreach (var piece in roadPieces)
        {
            if (piece.Equals(_piece))
            {
                found = true;
                break;
            }
        }

        if (!found)
        {
            roadPieces.Add(_piece);
        }
        else
        {
            DestroyImmediate(_piece.roadObject);
        }
    }
}
