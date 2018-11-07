using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript2 : MonoBehaviour
{
    public GameObject chamber;
    private GameObject lastChamber;
    public int randomMargin;
    public int chambers;
    public int chamberSize;

    private GameObject[] intialChambers;
    private GameObject[] leftRightChambers;
    // Start is called before the first frame update
    void Start()
    {
        LoadResources();
        SpawnInitialChamber();
        float lastHeight = 0;
        for(int i=0; i< chambers; i++)
        {
            float initialHeight = lastChamber.GetComponent<testScript>().rightConnection;
            Vector3 chamberPosition = new Vector3(chamberSize * i, lastHeight + initialHeight, 0);
            int chosenChamber = Random.Range(0, leftRightChambers.Length);
            chamber = leftRightChambers[chosenChamber];
            GameObject spawnedChamber = Instantiate(chamber, chamberPosition, Quaternion.identity);
            spawnedChamber.transform.parent = gameObject.transform;
            spawnedChamber.layer = 8;
            spawnedChamber.GetComponent<testScript>().InitiateConnections();
            float leftConnectionHeight = spawnedChamber.GetComponent<testScript>().leftConnection;
            float margin = lastChamber.GetComponent<testScript>().margin;
            chamberPosition.y -= leftConnectionHeight;
            spawnedChamber.transform.position = chamberPosition;
            spawnedChamber.GetComponent<testScript>().SpawnChamber();
            lastChamber = spawnedChamber;
            lastHeight = chamberPosition.y;
        }
    }

    void LoadResources()
    {
        intialChambers = Resources.LoadAll<GameObject>("Prefabs/Camaras/initialchambers");
        leftRightChambers = Resources.LoadAll<GameObject>("Prefabs/Camaras/leftRightChambers"); 
    }

    void SpawnInitialChamber()
    {
        Vector3 chamberPosition = new Vector3(-chamberSize, 0, 0);
        int chosenInitial = Random.Range(0, intialChambers.Length);
        chamber = intialChambers[chosenInitial];
        lastChamber = Instantiate(chamber, chamberPosition, Quaternion.identity);
        lastChamber.transform.parent = gameObject.transform;
        lastChamber.GetComponent<testScript>().InitiateConnections();
        lastChamber.GetComponent<testScript>().SpawnChamber();
        lastChamber.layer = 8;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
