﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager self;
	public ColorManager colorManager;
	public SceneManager sceneManager;

	private void Awake()
	{
		self = this;
	}

	
}
