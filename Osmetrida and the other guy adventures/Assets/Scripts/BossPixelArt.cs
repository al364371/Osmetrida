using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPixelArt : MonoBehaviour {

    public Transform BallAttack;
    public float offsetx = 2f;
    public float offsety = 0.342f;
    private Vector2 posSpawn;

	void Start () {
		
	}
	

	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<Health>().HurtPlayer(1);
        }
    }

    void SpawnProjectile()
    {
        posSpawn = new Vector2(gameObject.transform.position.x + offsetx, gameObject.transform.position.y + offsety);
        Instantiate(BallAttack, posSpawn, Quaternion.identity);
        posSpawn = new Vector2(gameObject.transform.position.x - offsetx, gameObject.transform.position.y + offsety);
        Instantiate(BallAttack, posSpawn, Quaternion.identity);
        offsetx += 2;
    }
}
