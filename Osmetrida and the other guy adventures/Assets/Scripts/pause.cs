using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour {

    public bool active;
    Canvas canvas;

	// Use this for initialization
	void Start () {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("return"))
        {
            ToggleGameActive(!active);
        }
	}

    public void ToggleGameActive(bool toSetActive)
    {
        active = toSetActive;
        canvas.enabled = active;
        Time.timeScale = (active) ? 0 : 1f;
    }
}
