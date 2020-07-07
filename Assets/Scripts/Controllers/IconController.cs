using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{

	[HeaderAttribute("Animation Curve")]
	public AnimationCurve animationCurve;

	[SerializeField] Image selfImg;
	[SerializeField] GameObject feverImg;

	private Vector3 initialScale;
	private Vector3 finalScale;
	private float graphValue;

	private void Awake()
	{
		initialScale = transform.localScale;
		finalScale = new Vector3(0.8f, 0.8f, 0.8f);
	}


	public void SetNewIconColor(Color newColor)
	{
		newColor.a = 1;
		selfImg.color = newColor;		
	}
	
	public void EvaluateIcon()
	{
		StartCoroutine(UpdateImg());
	}

	IEnumerator UpdateImg()
	{
		float startTime = Time.time;
		float duration = 0.5f;
		float t = 0;

		while (t<1)
		{
			t = (Time.time - startTime) / duration;

			graphValue = animationCurve.Evaluate(t);
			transform.localScale = finalScale * graphValue;			

			yield return new WaitForEndOfFrame();
		}
	}

	public void EnteredFever()
	{
		ActivateDeactivateFeverImg(true); // 
	}

	public void FeverExite()
	{
		ActivateDeactivateFeverImg(false); // 
	}

	void ActivateDeactivateFeverImg(bool isActive)
	{
		feverImg.SetActive(isActive);
	}

}
