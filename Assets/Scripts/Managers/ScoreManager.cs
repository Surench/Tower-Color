﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	[SerializeField] Slider levelSlider;
	[SerializeField] Slider feverSlider;

	[SerializeField] Image firstCircleLevelimg;
	[SerializeField] Image secondCircleLevelimg;
	[SerializeField] Image levelSliderFill;

	[SerializeField] Text currentPercentageText;
	[SerializeField] Text currLvlText;
	[SerializeField] Text nextLvlTex;
	[SerializeField] TextMeshProUGUI bulletAmountText;

	public GameEvent UpdateToMiddStage;
	public GameEvent UpdateToFinalStage;
	public GameEvent EnterFever;
	public GameEvent ExitFever;


	public enum GameLvelStage
	{
		Start,
		HalfWay,
		AlmostToWin,
		Won
	}

	public GameLvelStage currentLevelStage;

	private float totalCansAmount; // in specific lvl
	private float totalKnockdownCans; // how much Cans you distroyed
	private float levelSliderValue;

	private int ferevScore;
	private int maxFeverScoreAmount = 5;
	private int totalAmoAmount;

	

	public void InitScoreManager()
	{
		currentLevelStage = GameLvelStage.Start;

		totalAmoAmount = LevelManager.levelConfigs.amoAmount;

		totalCansAmount = GetTatalCansAmountForTheLvl(); //Total Cans amount in level

		ResetScores();
		RestFeverSlider();
		ResetLevelSlider();
		SetLvlTexts();				
	}

	void ResetScores()
	{
		ferevScore = 0;
		levelSliderValue = 0;
		totalKnockdownCans = 0;
	}


	void SetLvlTexts()
	{
		currLvlText.text = (LevelManager.currentLevel + 1).ToString();
		nextLvlTex.text = (LevelManager.currentLevel + 2).ToString();
		bulletAmountText.text = "X " + totalAmoAmount.ToString();
		currentPercentageText.text = "0%";
	}

		
	public void AddScore(int amount)
	{
		totalKnockdownCans += amount;
		
		UpdateLevelSlider();
		CheckTotalScore();
	}


	public void AddFeverScore()
	{
		if(!PlayerController.enteredFever) ++ferevScore;

		float feverValue = (float)ferevScore / (float)maxFeverScoreAmount;

		feverSlider.value = feverValue;

		if (ferevScore == maxFeverScoreAmount)
			EnterFeverMode();
	}

	public void DecreaseAmoAmount()
	{
		--totalAmoAmount;

		bulletAmountText.text = "X " + totalAmoAmount.ToString();
		
		if (totalAmoAmount <= 0) LevelLost();		
	}


	void EnterFeverMode()
	{
		EnterFever.Raise();
	}

	public void ExitFeverMode()
	{
		ExitFever.Raise();
	}
	

	void CheckTotalScore()
	{
		if ((levelSliderValue >=0.35f && levelSliderValue <= 0.6f) && (currentLevelStage != GameLvelStage.HalfWay)) // Half Way
		{
			UptadeLevelStageToHalfWay();
		}
		else if((levelSliderValue >=0.60f && levelSliderValue <= 0.8f) && (currentLevelStage != GameLvelStage.AlmostToWin)) // Almost To win
		{
			UptadeLevelStageTolmosToWin();			
		}
		else if (levelSliderValue >= 0.9f) // Won
		{
			if (!GameManager.isGameOver)
			{
				LevelWon();
				GameManager.isGameOver = true;
			}
		}
	}

	void UptadeLevelStageToHalfWay()
	{
		currentLevelStage = GameLvelStage.HalfWay;

		UpdateToMiddStage.Raise();
	}

	void UptadeLevelStageTolmosToWin()
	{
		currentLevelStage = GameLvelStage.AlmostToWin;

		UpdateToFinalStage.Raise();		
	}

	void LevelWon()
	{
		levelSlider.value = 1;
		currentLevelStage = GameLvelStage.Won;
		
		GameManager.instance.LevelWon();		
	}

	void LevelLost()
	{
		GameManager.instance.LevelLost();
	}

	void ResetLevelSlider()
	{
		levelSlider.value = 0;
	}

	void UpdateLevelSlider()
	{
		levelSliderValue = totalKnockdownCans / totalCansAmount;
		levelSlider.value = levelSliderValue;

		float percentagePassed = Mathf.RoundToInt(levelSliderValue * 100);
		currentPercentageText.text = percentagePassed.ToString() + "%";
	}

	public void RestFeverSlider()
	{
		StartCoroutine(ResetFeverSlider()); 
	}

	IEnumerator ResetFeverSlider()
	{
		ferevScore = 0;

		float startTime = Time.time;
		float duration = 1f;		

		float endValu = 0;
		float startValu = feverSlider.value;
		float t =0;

		if (startValu <=0) t = 1;
		
		while (t<1)
		{
			t = (Time.time - startTime) / duration;
			feverSlider.value = Mathf.Lerp(startValu, endValu, t);

		    yield return new WaitForEndOfFrame();
		}
	}


	float GetTatalCansAmountForTheLvl()
	{
		return LevelManager.levelConfigs.floorsAmount * 15;
	}

}
