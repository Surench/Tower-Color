using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class PlayerController : MonoBehaviour
{
	[SerializeField] GameObject cameraContainer;
	[SerializeField] Transform shotingPos;
	[SerializeField] float shotSpeed;

	public IconController iconController;
	public static bool enteredFever;

	private Ray ray;
	private RaycastHit hit;
	
	private Color newBulletColor;
	private string newBulletTag;

	private bool allowShooting;
	private bool allowRotate;
	

	public void InitPlayerController()
	{
		SetOffBools();

		ResetPlayerControler();
		MovePlayerToStartingPos();
		
		InitNewBulletData(); // take new Color and Tag for next Bullet
		SetBulletIconColor(); // set next bullet color
	}

	public void GameFinished()
	{
		SetOffBools();
	}

	void SetOffBools()
	{
		allowShooting = false;
		allowRotate = false;
	}
	
	void ResetPlayerControler()
	{
		transform.position = Vector3.zero;
		cameraContainer.transform.position = Vector3.zero;

		transform.rotation = Quaternion.identity;
		cameraContainer.transform.rotation = Quaternion.identity;
	}

	void MovePlayerToStartingPos()
	{
		StartCoroutine(MovePlayerToStartingPosR());
		allowShooting = true;
		allowRotate = true;
	}

	IEnumerator MovePlayerToStartingPosR()
	{
		float startTime = Time.time;
		float duration = 2f;
		float t = 0;

		Vector3 startPos = transform.position;
		Vector3 endPos = LevelManager.levelConfigs.playerStartingHight;

		while (t<1)
		{
			t = (Time.time - startTime) / duration;

			transform.position = Vector3.Lerp(startPos, endPos, t);
			cameraContainer.transform.position = Vector3.Lerp(startPos, endPos, t);

			transform.Rotate(new Vector3(0, -t * 2f, 0));
			cameraContainer.transform.Rotate(new Vector3(0, -t * 2f, 0));			

			yield return new WaitForEndOfFrame();
		}
	}

	public void UpdatePlayerPosition()
	{
		StartCoroutine(UpdatePlayerNewPositionR());
	}

	IEnumerator UpdatePlayerNewPositionR()
	{
		float startTime = Time.time;
		float duration = 0.5f;
		float t = 0;

		Vector3 startPos = transform.position;
		Vector3 endPos = transform.position - new Vector3(0, 4.6f,0);

		while (t < 1)
		{
			t = (Time.time - startTime) / duration;

			transform.position = Vector3.Lerp(startPos, endPos, t);
			cameraContainer.transform.position = Vector3.Lerp(startPos, endPos, t);			

			yield return new WaitForEndOfFrame();
		}
	}

	void InitNewBulletData()
	{
		int randomX = Random.Range(0, LevelManager.levelConfigs.colorsAmount);
		newBulletTag = GameManager.instance.LevelManager.GetNewTag(randomX); // Will take tag
		newBulletColor = GameManager.instance.ColorManager.GetNewColor(randomX); // Will take color		
	}

	void SetBulletIconColor()
	{
		iconController.SetNewIconColor(newBulletColor);
	}


	void ShootBullet()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		Debug.DrawRay(ray.origin, ray.direction * 30, Color.red);

		if (Physics.Raycast(ray, out hit,15 ) && allowShooting)
		{
			var newBullet = ShotPool.self.Get();			

			Vector3 Vo = CalcualateVelocity(hit.point, shotingPos.position, shotSpeed);
			
			InitBullet(newBullet.gameObject, Vo);

			PlayerShooted();
		}
	}


	void InitBullet(GameObject bullet, Vector3 velocity)
	{
		BulletController newBulletObj = bullet.gameObject.GetComponent<BulletController>();

		if (enteredFever)
			newBulletObj.isFeverBall = true;
		

		bullet.transform.position = shotingPos.position;
		bullet.transform.rotation = shotingPos.rotation;

		bullet.gameObject.SetActive(true);

		newBulletObj.InitBullet(newBulletTag, newBulletColor, velocity);

		iconController.EvaluateIcon();
	}


	void PlayerShooted()
	{
		StartCoroutine(PlayerShootedRoutin());
	}

	IEnumerator PlayerShootedRoutin()
	{
		allowShooting = false;

		InitNewBulletData(); // take new Color and Tag for next Bullet
		SetBulletIconColor(); // set next bullet color

		GameManager.instance.ScoreManager.DecreaseAmoAmount();

		yield return new WaitForSeconds(0.7f);
		allowShooting = true;
	}
	   

	Vector3 CalcualateVelocity (Vector3 target, Vector3 origin , float time)
	{
		//define the distance x - y
		Vector3 distance = target - origin;
		Vector3 distancXZ = distance;
		distancXZ.y = 0;

		//new float represent distanc
		float Sy = distance.y;
		float Sxz = distance.magnitude;

		float Vxz = Sxz / time;
		float Vy = Sy / time + 0.5f * Mathf.Abs( Physics.gravity.y) * time;

		Vector3 result = distancXZ.normalized;
		result *= Vxz;
		result.y = Vy;

		return result;
	}

	public void EnterFerev()
	{
		enteredFever = true;
		//iconController.EnteredFever();
	}

	public void ExiteFever()
	{
		enteredFever = false;
	}

	void RotatePlayer(Vector3 newElure)
	{
		transform.Rotate(newElure);
		cameraContainer.transform.Rotate(newElure);
	}



	PointerEventData pointerData;

	
	Vector2 StartPosition;
	Vector2 CurrentPosition;
	Vector2 TotalDeltaPosition;
	Vector2 LastPosition;
	Vector2 DeltaPosition;


	public void TouchDrag(BaseEventData data)
	{
		pointerData = data as PointerEventData;

		CurrentPosition = pointerData.position;

		TotalDeltaPosition = CurrentPosition - StartPosition;

		DeltaPosition = CurrentPosition - LastPosition;

		LastPosition = pointerData.position;


		if (allowRotate)
			RotatePlayer(new Vector3(0, DeltaPosition.x/3, 0));

	}

	public void TouchDown(BaseEventData data)
	{
		pointerData = data as PointerEventData;

		StartPosition = pointerData.position;
		LastPosition = StartPosition;
		TotalDeltaPosition = Vector3.zero;
	}


	public void TouchUp()
	{
		if (TotalDeltaPosition.magnitude < 10) // Limir for shooting , if you rotated so you cant shoot
			ShootBullet();						
	}

}
