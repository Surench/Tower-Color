using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	
	private void OnCollisionEnter(Collision collision)
	{
		DisableBullet();

		CanController hitCan = collision.gameObject.GetComponent<CanController>();

		if (hitCan !=null) hitCan.BallHit();			
	}


	void DisableBullet()
	{
		ShotPool.self.ReturnToPool(this);
	}
    
}
