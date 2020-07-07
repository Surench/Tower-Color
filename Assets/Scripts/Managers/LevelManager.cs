﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings
{
	public int currentLevel;
}

public class LevelConfigs
{
	public int floorsAmount;
	public int amoAmount;
	public int colorsAmount;
	public bool isBossLvl;
	public Vector3 playerStartingHight;
}

public class LevelManager : MonoBehaviour
{

	public static LevelConfigs levelConfigs = new LevelConfigs();

	public List<string> CanTags;

	public static int currentLevel;

	private float minimumFloorsAmount = 10;

	private void Awake()
	{
		InitLevelManager();
	}

	
	   
	public void InitLevelManager()
	{
		//currentLevel = DataManager.GetLevelSettings().currentLevel;
		currentLevel = 0;
		AllLevelCalculation();
	}

	void AllLevelCalculation() // calculate flors amount , amo amount , colors amount
	{
		//here game designer can count all configs for each lvl or every 5 lvl something gona change ..etc

		if (currentLevel < 200) //0-200 lvls
		{
			levelConfigs.floorsAmount = 15;
			levelConfigs.amoAmount = 15;
			levelConfigs.colorsAmount = 3;
			levelConfigs.isBossLvl = false;
			levelConfigs.playerStartingHight = new Vector3(0, 11.5f, 0);
		}

		if (currentLevel % 3 ==0) // every 3-th lvl gonna be Boss lvl
		{
			levelConfigs.isBossLvl = true;
		}

	}

	public string GetNewTag(int index)
	{
		string newTag = "";

		switch (index)
		{
			case 0:
				newTag = CanTags[index];
				break;
			case 1:
				newTag = CanTags[index];
				break;
			case 2:
				newTag = CanTags[index];
				break;
			default:
				Debug.LogError("Aout of Range");
				break;
		}

		return newTag;
	}


}

