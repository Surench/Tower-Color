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

	[SerializeField] GameObject LowPolyWaterBasic;
	[SerializeField] GameObject LowPolyWaterBossLvl;


	public List<Color> currentColorPack;

	public void InitColorManager()
	{
		PickaRandomColorForScen();
		CheckIsBossLvel();
	}

	void PickaRandomColorForScen()
	{
		currentColorPack.Clear();

		int random = Random.Range(0, listOfColors.Length);

		for (int i = 0; i < listOfColors[random].ColorPacks.Count; i++)
		{
			currentColorPack.Add(listOfColors[random].ColorPacks[i]);
		}
	}

	void CheckIsBossLvel()
	{
		if (LevelManager.levelConfigs.isBossLvl)
		{			
			LowPolyWaterBasic.SetActive(false);
			LowPolyWaterBossLvl.SetActive(true);
			GameManager.instance.cameraController.SetBossLevel();
		}
		else
		{
			LowPolyWaterBasic.SetActive(true);
			LowPolyWaterBossLvl.SetActive(false);
			GameManager.instance.cameraController.SetBasicLevel();
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
