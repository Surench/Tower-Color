using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public static bool isGameOver;

	private void Awake()
	{
		instance = this;
		Application.targetFrameRate = 60;
	}

	private void Start()
	{
		isGameOver = false;
		StartGame();
	}


	void StartGame()
	{
		cameraController.InitCamera();
		SceneManager.InitScene();
		PlayerController.InitPlayer();
	}

	public void LevelWon()
	{
		cameraController.SetCameraWiningPos();
		isGameOver = true;
	}
}
