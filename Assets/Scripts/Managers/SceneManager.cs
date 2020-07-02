using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	public List<string> CanTags;
	public List<CanController> canControllersForDisable;


	public void AddSimilarCansToTheList(CanController newSimilarCan)
	{
		canControllersForDisable.Add(newSimilarCan);
	}
}
