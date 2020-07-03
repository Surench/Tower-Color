using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	public List<string> CanTags;
	public List<CanController> canControllersForDisable;

	public int searchCount;

	public void AddSimilarCansToTheList(CanController newSimilarCan)
	{
		canControllersForDisable.Add(newSimilarCan);
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
