using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section{

	public List<Vector2> chambers ;
	public int[] connections ;
	public bool isComplete = false;

	public Section()
	{
		connections  = new int[4];
		chambers = new List<Vector2>();

		for(int i=0; i<4 ; i++)
		{
			connections[i] = -1;
		}
	}

}
