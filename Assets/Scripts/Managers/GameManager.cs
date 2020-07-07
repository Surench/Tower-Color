using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
		ScoreManager.InitScoreManager(); //OK
		ColorManager.InitColorManager(); //OK
		SceneManager.InitSceneManager(); //OK

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
		GameOverEvent.Invoke();
		PlayerController.GameFinished();
		isGameOver = true;
	}
}
