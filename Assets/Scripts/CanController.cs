using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanController : MonoBehaviour
{
	[SerializeField] Renderer selfRender;
	[SerializeField] SphereCollider selfSphereCollider;

	List<CanController> canControllers;

	private int selfCanIndex;
	private string selfTag;

	private void Start()
	{
		InitCan();
	}


	void InitCan()
	{
		selfCanIndex = GetRandomNewIndex();
		selfTag = GetNewTagForCanself(selfCanIndex);

		SetNewTag(selfTag);
		SetNewColor();
	}
	


	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals(selfTag))
		{
			CanController neighbour= other.gameObject.GetComponent<CanController>();

			canControllers.Add(neighbour);

		}
		else
		{
			DeactivateSelfCan();
			Debug.Log("Deactivate Self");
		}
	}

	public void ActivateNeighbourCan(CanController neighbourCan)
	{
		neighbourCan.ActivateSelfCan();
		DeactivateSelfCan();
		Debug.Log("Hola");
	}

	public void ActivateSelfCan()
	{
		selfSphereCollider.enabled = true;
	}

	void DeactivateSelfCan()
	{		
		transform.gameObject.SetActive(false);
	}


	void SetNewTag(string newTag)
	{
		transform.gameObject.tag = newTag;
	}

	void SetNewColor()
	{
		selfRender.material.color = GetRandomColor();
	}

	private string GetNewTagForCanself(int newIndex)
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
