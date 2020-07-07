using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	[SerializeField] Rigidbody rb;
	[SerializeField] SphereCollider selfColiider;
	[SerializeField] Renderer slefRenderer;
	[SerializeField] GameObject feverVisual;

	private Coroutine DisableBullerC;

	public bool isFeverBall; //means is Bomb ball

	private string selfTag;
	private bool falesHit;


	public void InitBullet(string newTag,Color newColor,Vector3 newvelocity)
	{
		falesHit = false;

		if (!isFeverBall) InitBasicBullet(newTag, newColor);
		else InitFeverBullet();				

		rb.velocity = newvelocity;
	}


	void InitBasicBullet(string newTag, Color newColor) 
	{
		slefRenderer.enabled = true;
		feverVisual.SetActive(false);
		selfColiider.radius = 0.4f;

		SetBulletTag(newTag);
		SetBulletColor(newColor);

		DisableBullet(1.8f); // Timer afther 1.8f gonna diable
	}

	void InitFeverBullet()
	{
		slefRenderer.enabled = false;
		feverVisual.SetActive(true);
		selfColiider.radius = 1f;

		SetBulletTag("Bomb");
	}

	void ExitFever()
	{
		isFeverBall = false;
		slefRenderer.enabled = true;
		feverVisual.SetActive(false);
		selfColiider.radius = 0.4f;
	}

	private void OnCollisionEnter(Collision collision)
	{		
		if (!falesHit)
		{	
			if (isFeverBall || collision.gameObject.tag.Equals(selfTag) ) // if the bullets and Cans Tags are the same so do stuff
			{
				if (DisableBullerC != null) StopCoroutine(DisableBullerC);
				
				DisableBullet(0f);

				CanController hitCan = collision.gameObject.GetComponent<CanController>();

				if(!hitCan.isBlocked) hitCan.BallHits();

				if (!isFeverBall) falesHit = true;
				else GameManager.instance.PlayerController.ExiteFever();

			}
			else // rebound the bullet
			{
				reboundBullet(collision.contacts[0].point);

				falesHit = true;

				GameManager.instance.ScoreManager.RestFeverSlider();
			}
		}
	}


	void reboundBullet (Vector3 hitPoint)
	{
		Vector3 distanc = transform.forward - hitPoint;
		float speed = rb.velocity.magnitude;

		rb.AddForce(distanc.normalized * -speed * 1.6f, ForceMode.Impulse);
	}
	

	void DisableBullet(float secs)
	{
		DisableBullerC = StartCoroutine(DisableBulletRoutine(secs));		
	}
	IEnumerator DisableBulletRoutine(float sec)
	{
		yield return new WaitForSeconds(sec);
		ShotPool.self.ReturnToPool(this);

		if (isFeverBall)
			ExitFever();
	}


	public void SetBulletTag(string newTag)
	{
		selfTag = newTag;
		transform.gameObject.tag = selfTag;
	}
    
	public void SetBulletColor(Color newColor)
	{
		slefRenderer.material.color = newColor;
	}
}
