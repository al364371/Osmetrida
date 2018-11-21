using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMove : MonoBehaviour {

    public Camera cam;
    public GameObject player;

    Vector3 moveInput; 

    Vector2 inicio;
    Vector2 pos;

    public float offset;
    public float speed = -5;
    public float limite;


    // Use this for initialization
    void Start()
    {
        Debug.Log(cam.transform.position);
        inicio = new Vector2(cam.transform.position.x, cam.transform.position.y);
        transform.position = inicio;
        moveInput = player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (player.transform.position != moveInput)
        {
            moveInput = player.transform.position;
            offset += Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
            pos = new Vector2(cam.transform.position.x + offset, cam.transform.position.y);
            transform.position = pos;
        }

        Debug.Log("Offset de " + gameObject.name + ": " + offset);

        if(offset < -limite)
        {
            offset = limite;
        }
        if(offset > limite)
        {
            offset = -limite;
        }


    }
}
