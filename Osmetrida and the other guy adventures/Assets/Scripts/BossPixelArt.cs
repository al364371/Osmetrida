using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPixelArt : MonoBehaviour {

    public Transform BallAttack;
    public float offset = 2f;
    private Vector2 posSpawn;

	void Start () {
		
	}
	

	void Update () {
		
	}

    void SpawnProjectile()
    {
        posSpawn = new Vector2(gameObject.transform.position.x + offset, gameObject.transform.position.y);
        Instantiate(BallAttack, posSpawn, Quaternion.identity);
        posSpawn = new Vector2(gameObject.transform.position.x - offset, gameObject.transform.position.y);
        Instantiate(BallAttack, posSpawn, Quaternion.identity);
        offset += 2;
    }
}
