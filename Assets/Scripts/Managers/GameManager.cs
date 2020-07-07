using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public ColorManager ColorManager;
	public _SceneManager SceneManager;
	public LevelManager LevelManager;
	public DataManager DataManager;
	public ScoreManager ScoreManager;
	public PlayerController PlayerController;
	public CameraController cameraController;
	public AkuAkuController akuController;

	[SerializeField] UnityEvent StartGameEvent;
	[SerializeField] UnityEvent GameOverEvent;
	[SerializeField] UnityEvent LevelPassedEvent;

	[SerializeField] GameObject menuPanel;	
	
	public static bool isGameOver;

	private void Awake()
	{
		instance = this;
		Application.targetFrameRate = 60;
	}

	private void Start()
	{		
		StartGame();
	}
	
	void StartGame()
	{
		menuPanel.SetActive(true);	
	}

	public void InitNewLevel()
	{
		StartGameEvent.Invoke();

		LevelManager.InitLevelManager(); //OK
		ColorManager.InitColorManager(); //OK
		ScoreManager.InitScoreManager(); //OK		
		SceneManager.InitSceneManager(); //OK
		akuController.InitAkuAku(); //OK

		cameraController.InitCameraController(); //OK
		PlayerController.InitPlayerController(); //OK

		isGameOver = false;
	}
		
	public void LevelWon()
	{
		LevelPassedEvent.Invoke();
		LevelManager.LevelPassed();
		cameraController.SetCameraWiningPos();
		PlayerController.GameFinished();
	}

	public void LevelLost()
	{
		PlayerController.GameFinished();
		isGameOver = true;
		GameOverEvent.Invoke();		
	}

	

}
