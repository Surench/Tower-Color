using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] Camera mainCamera;

	private Coroutine UpdateCameraWinigPosC;
	private Coroutine WinigRotationC;
	   

	public void InitCameraController()
	{
		mainCamera.fieldOfView = 73f;
		if (WinigRotationC !=null) StopCoroutine(WinigRotationC);		
	}

	public void SetCameraWiningPos()
	{
		UpdateCameraWinigPosC = StartCoroutine(UpdateCameraWinigPos());
		WinigRotationC = StartCoroutine(UpdateCameraRotation());
	}
	
	IEnumerator UpdateCameraWinigPos()
	{
		float startTime = Time.time;
		float duration = 0.3f;
		float t = 0;

		float startView = mainCamera.fieldOfView;
		float endView = startView + 25;

		while (t < 1)
		{
			t = (Time.time - startTime) / duration;

			mainCamera.fieldOfView = Mathf.Lerp(startView, endView, t);

			yield return new WaitForEndOfFrame();
		}
	}

	IEnumerator UpdateCameraRotation()
	{
		Vector3 rotPos = new Vector3(0, 0.2f, 0);
		while (true)
		{
			transform.Rotate(rotPos);
			yield return new WaitForEndOfFrame();
		}
	}

}
