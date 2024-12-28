using System.ComponentModel;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject straightRoad; 
    public GameObject crossRoad;
    public GameObject deadEnd;
    public int width = 100;
    public int depth = 100;

    private void Start()
    {
       //nested for loops
       //to handle the two dimensions of the grid z and x 
       for (int z = 0;z < depth; z += 20)
        {
            for(int x = 0;x < width;x += 20)
            {
                Vector3 position = new Vector3(x,0,z);

                //instantiate pieces
                var crossRoadObj = Instantiate(crossRoad);
                var straightObjNonRotated=Instantiate(straightRoad);
                var straightObjRotated = Instantiate(straightRoad);
                var straightDeadEndObjNonRotated = Instantiate(deadEnd);
                var straightDeadEndObjRotated = Instantiate(deadEnd);

                //rotate the straight and dead end
                straightObjRotated.transform.Rotate(0f, 90f, 0f);
                straightDeadEndObjRotated.transform.Rotate(0f,90f, 0f); 

                //position the cross roads
                crossRoadObj.transform.position = position;
                crossRoad.transform.rotation = Quaternion.identity;

                //position the straights
                straightObjNonRotated.transform.position = new Vector3(position.x,0f,position.z+10);
                straightObjRotated.transform.position = new Vector3(position.x + 10, 0f, position.z);

                //position a dead end along the z and x with 15% odds
                var randomNumber = Random.Range(0, 100);
                if (randomNumber < 25)
                {
                    straightDeadEndObjNonRotated.transform.position = new Vector3(position.x, 0f, z + 10);
                }
                else
                {
                    straightDeadEndObjRotated.transform.position = new Vector3(x + 10, 0f, position.z);

                }
                
            }
        }
    }
}
