using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SceneManager : MonoBehaviour
{
	[SerializeField] GameObject floorPrefab;

	[SerializeField] List<FloorController> lastStage;
	[SerializeField] List<FloorController> middStage;
	[SerializeField] List<FloorController> firstStage;

	public List<CanController> canControllersForDisable;
	public List<FloorController> floorControllers;

	public int searchCount;

	private float newFloorYpos;
	private int theLeastActiveFloor;

	
	public void InitSceneManager()
	{
		AddFloorsToList();
		ActivateFirstStage();
	}


	void AddFloorsToList()  
	{
		newFloorYpos = 0;

		for (int i = 0; i < floorControllers.Count; i++)
		{
			floorControllers[i].InitFloor(); //gonna activate Cans 

			float Yrotation = 0;

			if (i % 2 != 0) Yrotation = 10;

			if (i > 0) newFloorYpos += 1.12f;
			else newFloorYpos += 0.5f;


			floorControllers[i].transform.position = new Vector3(0, newFloorYpos, 0);
			floorControllers[i].transform.rotation = Quaternion.Euler(0, Yrotation, 0);

			floorControllers[i].gameObject.SetActive(true);
		}		
	}
	

	public void ActivateFirstStage()
	{		
		StartCoroutine(DiactivateMiddLastStage()); // All active floors make Gray
		ActivateNextStage(0); // gonna activate 1st stage
	}

	public void ActivateNextStage(int stageIndex)
	{
		//Stages 0 first ,1 midd , 2 last .. Not best function that i could havave i will fix ir
		//TODO think bertter way!

		if (stageIndex.Equals(0))
			StartCoroutine(ActivateNewStage(firstStage));
		else if (stageIndex.Equals(1)) // 
			StartCoroutine(ActivateNewStage(middStage));
		else if(stageIndex.Equals(2))
		    StartCoroutine(ActivateNewStage(lastStage));
	}
	
	IEnumerator ActivateNewStage(List<FloorController> newList)
	{
		for (int i = newList.Count - 1; i >= 0; i--)
		{			
			newList[i].ActivateCans();
			yield return new WaitForSeconds(0.1f);
		}

		for (int i = newList.Count - 1; i >= 0; i--)
		{
			newList[i].ActivateFloorDetectingCollider();
		}
	}
		

	IEnumerator DiactivateMiddLastStage()
	{
		for (int i = 0; i < lastStage.Count; i++)
		{
			lastStage[i].MakeCansGray();
			yield return new WaitForSeconds(0.1f);
		}

		for (int i = 0; i < middStage.Count; i++)
		{
			middStage[i].MakeCansGray();
			yield return new WaitForSeconds(0.1f);
		}
	}

	

	IEnumerator MakeOtherFalfActive ()
	{		
		//first Activating Cans
		for (int i = theLeastActiveFloor; i < LevelManager.levelConfigs.floorsAmount; i++)
		{
			floorControllers[i].ActivateCans();
			yield return new WaitForSeconds(0.1f);

		}

		//them activating Floor Collider to detec falling Cans and count them
		for (int i = theLeastActiveFloor; i < LevelManager.levelConfigs.floorsAmount; i++)
		{
			floorControllers[i].ActivateFloorDetectingCollider();
		}
	}

	

	public void AddSimilarCansToTheList(CanController newSimilarCan)
	{
		canControllersForDisable.Add(newSimilarCan);
	}

	internal static void LoadScene(string v)
	{
		throw new NotImplementedException();
	}

	public void SearchingOfSimilarCansDone()
	{
		searchCount++;

		if (searchCount < canControllersForDisable.Count)
		{
			SearchMoreSimilarCans();			
		}
		else
		{
			GameManager.instance.ScoreManager.AddScore(canControllersForDisable.Count);

			DesableSimilarCans();
			CleanSearchResults();			
		}
	}

	void SearchMoreSimilarCans()
	{
		canControllersForDisable[searchCount].SearchSimilarCans();
	}

	void DesableSimilarCans()
	{
		for (int i = 0; i < canControllersForDisable.Count; i++)
		{
			canControllersForDisable[i].DisableCan();
		}
	} 

	void CleanSearchResults()
	{
		canControllersForDisable.Clear();
		searchCount = 0;
	}


}
