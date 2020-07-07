using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListOfColors
{
	public List<Color> ColorPacks;	
}
public class ColorManager : MonoBehaviour
{

	[SerializeField] ListOfColors[] listOfColors;
	
	public List<Color> currentColorPack;

	public void InitColorManager()
	{
		currentColorPack.Clear();

		int random = Random.Range(0, listOfColors.Length);

		for (int i = 0; i < listOfColors[random].ColorPacks.Count; i++)
		{
			currentColorPack.Add(listOfColors[random].ColorPacks[i]);
		}
	}


	public Color GetNewColor(int index)
	{
		Color newColor = Color.black;

		switch (index)
		{
			case 0:
				newColor = currentColorPack[index];
				break;
			case 1:
				newColor = currentColorPack[index];
				break;
			case 2:
				newColor = currentColorPack[index];
				break;
			case 3:
				newColor = currentColorPack[index];
				break;
			default:
				Debug.LogError("Aout of Range");
				break;
		}

		return newColor;
	}
}
