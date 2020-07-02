using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] new Camera camera;

	[SerializeField] Renderer testing;
	void Update()
	{
		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);

		Debug.DrawRay(ray.origin, ray.direction * 30, Color.red);

		if (Input.GetMouseButtonDown(0))
		{
			if (Physics.Raycast(ray, out hit))
			{
				

				testing = hit.collider.GetComponent<Renderer>();
			}
		}
		
	}
}
