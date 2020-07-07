using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
	[SerializeField] List<CanController> canControllers;
	[SerializeField] GameObject floorCollider;

	public void InitFloor()
	{
		InitCans();
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

	void InitCans()
	{
		floorCollider.SetActive(false);

		for (int i = 0; i < canControllers.Count; i++)  
		{
			canControllers[i].InitCan();
		}
	}

	public void SetFloorCollider()
	{
		floorCollider.SetActive(true);
	}

	public void ResetFloor()
	{
		floorCollider.SetActive(false);
	}

}
