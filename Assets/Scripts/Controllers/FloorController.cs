using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
	[SerializeField] List<CanController> canControllers;
	[SerializeField] GameObject floorCollider;

	public void InitFloor()
	{
		ResetFloor(); 
		InitCans();		
	}

	public void ResetFloor()
	{
		DeactivateFloorCollider();
		ResetCansTransforms();
	}

	void InitCans()
	{
		for (int i = 0; i < canControllers.Count; i++)
		{
			canControllers[i].InitCan();
		}
	}

	public void ActivateCans()
	{
		for (int i = 0; i < canControllers.Count; i++)
		{
			canControllers[i].UnBlockCan();
		}
	}

	public void MakeCansGray()
	{
		for (int i = 0; i < canControllers.Count; i++)  
		{
			canControllers[i].ChangeCanColorToGray();
		}
	}	

	public void ActivateFloorDetectingCollider()
	{
		floorCollider.SetActive(true);
	}

	public void DeactivateFloorCollider()
	{
		floorCollider.SetActive(false);
	}

	void ResetCansTransforms()
	{
		int angelY = 24;

		for (int i = 0; i < canControllers.Count; i++)
		{
			canControllers[i].gameObject.transform.localPosition = Vector3.zero;
			canControllers[i].gameObject.transform.rotation = Quaternion.Euler(0, angelY * i, 0);
		}
	}

}
