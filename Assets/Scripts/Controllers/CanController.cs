using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanController : MonoBehaviour
{
	[SerializeField] Renderer visualRender;
	[SerializeField] GameObject visualGameObject;
	[SerializeField] Transform rightPoint;
	[SerializeField] Transform leftPoint;

	public bool ActiveCan;

	private int selfCanIndex;
	private string selfTag;
	
		
	private void Start()
	{
		InitCan();
	}

	private void Update()
	{
		//DebugDrawRay();
	}

	void InitCan()
	{
		DeactivateSelfCan();
		SetNewTag();
		SetNewColor();		
	}


	public void BallHit()
	{
		ActivateSelfCan();
		SearchSimilarCans();
	}

	public void SearchSimilarCans()
	{
		FindeNearSimilarCan();
		SearchDone();
	}


	public void FindeNearSimilarCan()
	{
		DoRaycast(visualGameObject.transform.position, visualGameObject.transform.TransformDirection(Vector3.right)); //Raycast Right
		DoRaycast(visualGameObject.transform.position, visualGameObject.transform.TransformDirection(-Vector3.right)); //Raycast Left
		DoRaycast(rightPoint.position, rightPoint.up); //Right Up
		DoRaycast(rightPoint.position, -rightPoint.up); // Right Down
		DoRaycast(leftPoint.position, leftPoint.up); // Left UP
		DoRaycast(leftPoint.position, -leftPoint.up); // Left Down
	}

	void SearchDone()
	{
		GameManager.self.sceneManager.SearchingOfSimilarCansDone();
	}

	void DebugDrawRay()
	{
		Debug.DrawRay(visualGameObject.transform.position, visualGameObject.transform.TransformDirection(Vector3.right) * 5, Color.green);
		Debug.DrawRay(visualGameObject.transform.position, visualGameObject.transform.TransformDirection(-Vector3.right) * 5, Color.green);
		//Debug.DrawRay(rightPoint.position, rightPoint.up * 1, Color.red);
		//Debug.DrawRay(rightPoint.position, -rightPoint.up * 1, Color.red);
		//Debug.DrawRay(leftPoint.position, leftPoint.up * 1, Color.red);
		//Debug.DrawRay(leftPoint.position, -leftPoint.up * 1, Color.red);
	}

	CanController similarCan;
	void DoRaycast(Vector3 origin,Vector3 direction)
	{
		RaycastHit hit;
		
		if (Physics.Raycast(origin, direction * 1, out hit, 1))
		{			
			if (hit.collider.tag.Equals(selfTag))
			{
				similarCan = hit.collider.GetComponentInParent<CanController>();

				if ((similarCan !=null) && (!similarCan.ActiveCan))
				{
					similarCan.ActivateSelfCan();					
				}				
			}
		}
	}





	public void ActivateSelfCan()
	{
		ActiveCan = true;
		GameManager.self.sceneManager.AddSimilarCansToTheList(this);
	}

	void DeactivateSelfCan()
	{
		ActiveCan = false;
		
	}

	public void DisableCan()
	{
		transform.gameObject.SetActive(false);
	}
	

	void SetNewTag()
	{
		selfCanIndex = GetRandomNewIndex();

		selfTag = GetNewTagForThisCan(selfCanIndex);

		visualGameObject.transform.gameObject.tag = selfTag;
	}

	void SetNewColor()
	{
		visualRender.material.color = GetRandomColor();
	}



	private string GetNewTagForThisCan(int newIndex)
	{
		string newString = "";

		switch (newIndex)
		{
			case 0:
				newString = GameManager.self.sceneManager.CanTags[newIndex];
				break;
			case 1:
				newString = GameManager.self.sceneManager.CanTags[newIndex];
				break;
			case 2:
				newString = GameManager.self.sceneManager.CanTags[newIndex];
				break;
		}

		return newString;
	}

	private Color GetRandomColor()
	{
		Color newColor = GameManager.self.colorManager.Colors[selfCanIndex];

		return newColor;
	}

	private int GetRandomNewIndex()
	{
		int randomX = Random.Range(0, 3);

		return randomX;
	}
}
