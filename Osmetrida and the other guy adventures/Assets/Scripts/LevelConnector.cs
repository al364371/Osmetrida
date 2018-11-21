using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConnector : MonoBehaviour {

	public int newSection;
	void OnTriggerEnter2D(Collider2D collider)
	{
		MapManagerEvents.DrawSection(newSection);
	}
}
