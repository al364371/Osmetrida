using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class testScript : MonoBehaviour
{
    Tilemap tileMatrix;
    public RuleTile ruleTile;
    public int offsetX;
    public int offsetY;
    
    public int leftConnection;
    public int rightConnection;

    public int margin;
    public bool isStarting;
    public string type;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateConnections()
    {
        if(isStarting)
        {
            createStartingRoom();
        }
        else
        {
            assignRandomConnections();
        }
    }
    public void assignRandomConnections()
    {
        leftConnection = Random.Range(1,15 - margin);
        rightConnection = Random.Range(1,15 - margin);
    }

    public void createStartingRoom()
    {
        leftConnection = 39;
        rightConnection = Random.Range(1,15 - margin);
    }
    public void SpawnChamber()
    {
        tileMatrix = GetComponent<Tilemap>();
        for(int x= 0; x< 30; x++)
        {
            for(int y= 0; y < 16; y++)
            {
                if(x== 0 && (y < leftConnection || y > leftConnection + margin))
                {
                    tileMatrix.SetTile(new Vector3Int(x + offsetX,y + offsetY,0),ruleTile);
                }
                if(x == 29 && (y < rightConnection || y > rightConnection + margin))
                {
                    tileMatrix.SetTile(new Vector3Int(x + offsetX,y + offsetY,0),ruleTile);
                }
                if(y==0 || y== 15)
                {
                    tileMatrix.SetTile(new Vector3Int(x + offsetX,y + offsetY,0),ruleTile);
                }
            }
        }
    }
}
