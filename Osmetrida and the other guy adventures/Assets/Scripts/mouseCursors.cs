using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCursors : MonoBehaviour {

    public float timeBtwSpawn = 0.1f;
    public float timeFromSpawn;
    public GameObject trail;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;

        if(timeFromSpawn <= 0)
        {
            Instantiate(trail, transform.position, Quaternion.identity);
            timeFromSpawn = timeBtwSpawn;
        }
        else
        {
            timeFromSpawn -= Time.deltaTime;
        }
	}
}
