using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    private int rand;
    public GameObject enemy1;
    public GameObject enemy2;

    // Use this for initialization
    void Start () {



        rand = Random.Range(0, 2);

        if(rand == 0)
        {
            GameObject toInstantiate = Instantiate(enemy1, transform.position,Quaternion.identity);
            toInstantiate.transform.parent = gameObject.transform;
        }

        else
        {
            GameObject toInstantiat = Instantiate(enemy2, transform.position, Quaternion.identity);
            toInstantiat.transform.parent = gameObject.transform;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
