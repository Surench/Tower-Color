using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkuAkuController : MonoBehaviour
{
	[SerializeField] GameObject Glow;
	[SerializeField] GameObject Pokimon;
	[SerializeField] GameObject AkuAku;

	private bool isBossLvl;


	public void InitAkuAku()
	{
		CheckIsBossLevel();
	}

	void CheckIsBossLevel()
	{
		isBossLvl = LevelManager.levelConfigs.isBossLvl;

		if (isBossLvl)
		{
			Pokimon.SetActive(true);
			Glow.SetActive(false);
			ActivateAkuAku();
		}
		else
		{
			Glow.SetActive(true);
			Pokimon.SetActive(false);		
			AkuAku.SetActive(false);
		}	
	}

	public void UpdateAkuAkuPosToDown()
	{
		if (isBossLvl)
		{
			int newYpos;

			if (GameManager.instance.ScoreManager.currentLevelStage == ScoreManager.GameLvelStage.HalfWay)
				newYpos = 13;
			else newYpos = 9;

			StartCoroutine(UpdateAkuAkuPos(new Vector3(0, newYpos, 0)));
		}
	}

	void ActivateAkuAku()
	{
		AkuAku.SetActive(true);
		StartCoroutine(UpdateAkuAkuPos(new Vector3(0, 18, 0))); 
	}

	IEnumerator UpdateAkuAkuPos(Vector3 endPos)
	{
		float startTime = Time.time;
		float duration = 3f;
		float t = 0;

		Vector3 startPos = AkuAku.transform.position;

		while (t < 1)
		{
			t = (Time.time - startTime) / duration;

			AkuAku.transform.position = Vector3.Lerp(startPos, endPos, t);

			yield return new WaitForEndOfFrame();
		}
	}


}
