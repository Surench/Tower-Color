using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
	public List<Color> Colors;


	public Color GetNewBallColor(int index)
	{
		Color newColor = Color.black;

		switch (index)
		{
			case 0:
				newColor = Colors[index];
				break;
			case 1:
				newColor = Colors[index];
				break;
			case 2:
				newColor = Colors[index];
				break;
			default:
				Debug.LogError("Aout of Range");
				break;
		}

		return newColor;
	}
}
