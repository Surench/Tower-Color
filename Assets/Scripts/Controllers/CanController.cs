using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanController : MonoBehaviour
{
	[SerializeField] Rigidbody rb;
	[SerializeField] Renderer visualRender;

	[SerializeField] GameObject visualGameObject;
	[SerializeField] GameObject grayVisual;
	[SerializeField] GameObject particlEffect;

	[SerializeField] Transform rightPoint;
	[SerializeField] Transform leftPoint;

	[SerializeField] ParticleSystem ps;
	[SerializeField] Gradient grad;

	public bool ActiveCan;// it's for to finde similar Can if founded so it's Active
	public bool isBlocked; // if true it means cant hit block

	private int selfCanIndex;
	private string selfTag;
	private readonly string floorTag = "Floor";
	private readonly string unTagged = "Untagged";
	
	private void Update()
	{
		//DebugDrawRay();
	}


	public void InitCan()
	{
		BlockCan();
		DeactivateSelfCan(); // gravity off etc..
		SetNewTag();
		SetNewColor();
		SetParticle();
	}


	public void BallHits() // Ball hits the Can
	{
		ActivateSelfCan();
		SearchSimilarCans();
		AddFeverScore();
	}

	void AddFeverScore()
	{
		GameManager.instance.ScoreManager.AddFeverScore();
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


	private void OnTriggerExit(Collider other)
	{
		if (other.tag.Equals(floorTag) && !isBlocked)
		{
			CanFalledDown();
		}
	}


	void SearchDone()
	{
		GameManager.instance.SceneManager.SearchingOfSimilarCansDone();
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
		
	
	public void BlockCan() // make them Gray and block so bullet cant hit
	{
		rb.useGravity = false;
		rb.isKinematic = true;
		isBlocked = true;		
	}

	public void UnBlockCan()
	{
		rb.useGravity = true;
		rb.isKinematic = false;
		isBlocked = false;
		grayVisual.SetActive(false);
	}

	public void ChangeCanColorToGray() // et means the can is blocked
	{
		grayVisual.SetActive(true);
	}


	public void ActivateSelfCan()
	{
		ActiveCan = true;
		GameManager.instance.SceneManager.AddSimilarCansToTheList(this);
	}

	void CanFalledDown()
	{
		isBlocked = true;
		GameManager.instance.ScoreManager.AddScore(1);
	}

	void DeactivateSelfCan()
	{		
		ActiveCan = false;
		particlEffect.SetActive(false);
	}

	public void DisableCan() // when Can Been shooted or been similar can
	{
		DisableCanRoutine();
	}

	void DisableCanRoutine()
	{
		rb.isKinematic = true;
		visualGameObject.SetActive(false); //Disable Collider
		particlEffect.SetActive(true); // Activate Particle
	}
	

	void SetNewTag()
	{
		selfCanIndex = GetRandomNewIndex();

		selfTag = GameManager.instance.LevelManager.GetNewTag(selfCanIndex);

		visualGameObject.transform.gameObject.tag = selfTag;
		transform.gameObject.tag = selfTag;
	}
	   

	void SetNewColor()
	{		
		visualRender.material.color = GameManager.instance.ColorManager.GetNewBallColor(selfCanIndex);
	}	

	void SetParticle()
	{		
		var col = ps.colorOverLifetime;

		grad.SetKeys(new GradientColorKey[] { new GradientColorKey(GameManager.instance.ColorManager.GetNewBallColor(selfCanIndex), 0.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f) });
		col.color = grad;
	}

	private int GetRandomNewIndex()
	{
		int randomX = Random.Range(0, 3);

		return randomX;
	}
}
