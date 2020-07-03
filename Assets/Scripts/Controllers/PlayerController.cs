using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float shotSpeed;

	public LayerMask layer;
	public GameObject cursor;

	private Queue<GameObject> availableObjects = new Queue<GameObject>();
	private Ray ray;
	private RaycastHit hit;


	
	private void Update()
	{
		InitProjectile();
	}
		


	void InitProjectile()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		Debug.DrawRay(ray.origin, ray.direction * 30, Color.red);

		if (Physics.Raycast(ray, out hit,15 ))
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 Vo = CalcualateVelocity(hit.point, transform.position , shotSpeed);

				var newBullet = ShotPool.self.Get();
				
				ShootBullet(newBullet.gameObject, Vo);				
			}

		}
	}


	void ShootBullet(GameObject bullet, Vector3 velocity)
	{
		Rigidbody rb = bullet.GetComponent<Rigidbody>();
		bullet.transform.position = transform.position;
		bullet.transform.rotation = Quaternion.identity;
		bullet.gameObject.SetActive(true);
		rb.velocity = velocity;
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
}
