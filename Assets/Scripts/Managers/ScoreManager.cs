using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	[SerializeField] Slider levelSlider;
	[SerializeField] Slider feverSlider;

	[SerializeField] Text currentPercentageText;
	[SerializeField] Text currLvlText;
	[SerializeField] Text nextLvlTex;

	enum GameLvelStage
	{
		Start,
		HalfWay,
		AlmostToWin,
		Won
	}

	private GameLvelStage currentLevelStage;

	private float totalCansAmount; // in specific lvl
	private float totalKnockdownCans; // how much Cans you distroyed
	private float levelSliderValue;

	private int ferevScore;
	private int maxFeverScoreAmount = 5;


	private void Start()
	{
		InitScoreManager();
	}

	public void InitScoreManager()
	{
		currentLevelStage = GameLvelStage.Start;
		
		totalCansAmount = GetTatalCansAmountForTheLvl();
		RestFeverSlider();
		SetLvlTexts();

	}

	void SetLvlTexts()
	{
		currLvlText.text = (LevelManager.currentLevel + 1).ToString();
		nextLvlTex.text = (LevelManager.currentLevel + 2).ToString();
	}

	
	public void AddScore(int amount)
	{
		totalKnockdownCans += amount;
		
		UpdateSlider();
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

	void EnterFeverMode()
	{
		GameManager.instance.PlayerController.EnterFerev();
		RestFeverSlider();
	}
	

	void CheckTotalScore()
	{
		if ((levelSliderValue >=0.40f && levelSliderValue <= 0.6f) && (currentLevelStage != GameLvelStage.HalfWay)) // Half Way
		{
			UptadeLevelStageToHalfWay();
		}
		else if((levelSliderValue >=0.65f && levelSliderValue <= 0.8f) && (currentLevelStage != GameLvelStage.AlmostToWin)) // Almost To win
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
		GameManager.instance.SceneManager.ActivateNextStage(1);
		GameManager.instance.PlayerController.UpdatePlayerPosition();
		
	}

	void UptadeLevelStageTolmosToWin()
	{
		currentLevelStage = GameLvelStage.AlmostToWin;		
		GameManager.instance.SceneManager.ActivateNextStage(2);
		GameManager.instance.PlayerController.UpdatePlayerPosition();

	}

	void LevelWon()
	{
		levelSlider.value = 1;
		currentLevelStage = GameLvelStage.Won;
		
		GameManager.instance.LevelWon();
		
	}

	void UpdateSlider()
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
		float newFalu = 0;
		float t;

		if (startValu >0) t = 0;
		else t = 1;

		while (t<1)
		{
			t = (Time.time - startTime) / duration;
			newFalu = Mathf.Lerp(startValu, endValu, t);

			feverSlider.value = newFalu;
		    yield return new WaitForEndOfFrame();
		}
	}


	float GetTatalCansAmountForTheLvl()
	{
		return LevelManager.levelConfigs.floorsAmount * 15;
	}

}
