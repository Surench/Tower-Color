using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public static bool isGameOver;


	public GameEvent InitNewGameLevelEvent;
	public GameEvent LevelPassedGameEvent;
	public GameEvent LevelLostGameEvent;
	public GameEvent LevelAndColorGameEvent;

	public ColorManager ColorManager;
	public _SceneManager SceneManager;
	public LevelManager LevelManager;
	public ScoreManager ScoreManager;
	public PlayerController PlayerController;
	public CameraController cameraController;
	public AkuAkuController akuController;


	[SerializeField] UnityEvent StartGameEvent;
	[SerializeField] UnityEvent GameOverEvent;
	[SerializeField] UnityEvent LevelPassedEvent;
	[SerializeField] GameObject menuPanel;
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
		isGameOver = false;

		StartGameEvent.Invoke();

		LevelAndColorGameEvent.Raise();

		InitNewGameLevelEvent.Raise();
	}
		
	public void LevelWon()
	{
		LevelPassedEvent.Invoke();

		LevelPassedGameEvent.Raise();		
	}

	public void LevelLost()
	{
		isGameOver = true;

		GameOverEvent.Invoke();

		LevelLostGameEvent.Raise();
	}

	

}
