using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundReader : MonoBehaviour {

	// Use this for initialization
	public AudioSource musicController;
	public AudioSource soundController;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		musicController.volume = SoundVariables.musicVolume;
		soundController.volume = SoundVariables.effectsVolume;
        if (SoundVariables.musicMuted)
        {
            musicController.volume = 0;
            soundController.volume = 0;
        }
	}
}
