using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{


	private void OnCollisionEnter(Collision collision)
	{
		DisableBullet();
	}


	void DisableBullet()
	{
		ShotPool.self.ReturnToPool(this);
	}

    
}
