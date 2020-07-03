using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour
{

	[HeaderAttribute("Animation Curve")]
	public AnimationCurve animationCurve;

	private Vector3 initialScale;
	private Vector3 finalScale;
	private float graphValue;

	private void Awake()
	{
		initialScale = transform.localScale;
		finalScale = Vector3.one;
		animationCurve.postWrapMode = WrapMode.PingPong;
	}


	public void InitImg()
	{
		StartCoroutine(UpdateImg());
	}

	IEnumerator UpdateImg()
	{
		float startTime = Time.time;
		float duration = 1.5f;
		float t = 0;

		while (t<1)
		{
			t = (Time.time - startTime) / duration;

			graphValue = animationCurve.Evaluate(t);
			transform.localScale = finalScale * graphValue;

			yield return new WaitForEndOfFrame();
		}
	}

	private void Update()
	{
		//graphValue = animationCurve.Evaluate(Time.time);
		//transform.localScale = finalScale * graphValue;
	}
}
