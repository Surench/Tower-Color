using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SceneManager : MonoBehaviour
{
	[SerializeField] GameObject floorPrefab;

	public List<CanController> canControllersForDisable;
	public List<FloorController> floorControllers;

	[SerializeField] List<FloorController> lastStage;
	[SerializeField] List<FloorController> middStage;
	[SerializeField] List<FloorController> firstStage;

	public int searchCount;

	private float newFloorYpos;
	private int theLeastActiveFloor;

	private void Start()
	{
		
	}

	public void InitScene()
	{
		AddFloorsToList();
		AcrivateFirstStage();
	}

	void AddFloorsToList()  
	{
		for (int i = 0; i < LevelManager.levelConfigs.floorsAmount; i++) // 
		{
			GameObject obj = Instantiate(floorPrefab, Vector3.zero, Quaternion.identity, transform);
			FloorController newFloor = obj.GetComponent<FloorController>();
			floorControllers.Add(newFloor);
		}

		newFloorYpos = 0;

		for (int i = 0; i < floorControllers.Count; i++)
		{

			float Yrotation = 0;

			if (i % 2 != 0) Yrotation = 10;

			if (i > 0) newFloorYpos += 1.12f;
			else newFloorYpos += 0.5f;


			floorControllers[i].transform.position = new Vector3(0, newFloorYpos, 0);
			floorControllers[i].transform.rotation = Quaternion.Euler(0, Yrotation, 0);

			floorControllers[i].transform.parent = transform;

			floorControllers[i].InitFloor();//gonna activate Cans 

			floorControllers[i].gameObject.SetActive(true);

			if (i <= 3) lastStage.Add(floorControllers[i]);
			else if (i <= 7) middStage.Add(floorControllers[i]);
			else firstStage.Add(floorControllers[i]);

		}
	}
	

	public void AcrivateFirstStage()
	{		
		StartCoroutine(MakeHalfGrayRoutin()); // All active floors make Gray
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
			newList[i].SetFloorCollider();
		}
	}
		

	IEnumerator MakeHalfGrayRoutin()
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
			floorControllers[i].SetFloorCollider();
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
