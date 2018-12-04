using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundModifier : MonoBehaviour {

	// Use this for initialization
	public Slider volumeSlider;
	public Slider effectsSlider;

	
	// Update is called once per frame
	void Update () 
	{
		SoundVariables.musicVolume = volumeSlider.value;
		SoundVariables.effectsVolume = effectsSlider.value;
	}

    public void ToggleMuteMusic(bool mute)
    {
        SoundVariables.musicMuted = mute;
    }
}
