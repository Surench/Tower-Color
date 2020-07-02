using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
	[SerializeField] Rigidbody rb;
	[SerializeField] GameObject bullet;

	public LayerMask layer;
	public GameObject cursor;

	public Vector3 newPos;
	private void Update()
	{
		InitProjectile();
	}

	void InitProjectile()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		Debug.DrawRay(ray.origin, ray.direction * 30, Color.red);
		if (Physics.Raycast(ray, out hit,100 ))
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 Vo = CalcualateVelocity(hit.point, transform.position , 0.4f);

				GameObject ob = Instantiate(bullet, transform.position, Quaternion.identity);
				Rigidbody rb = ob.GetComponent<Rigidbody>();
				rb.velocity = Vo;
			}	
		
		}
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
