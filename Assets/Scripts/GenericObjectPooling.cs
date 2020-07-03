using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPooling<T> : MonoBehaviour where T : Component
{
	[SerializeField] private T prefab;

	public static GenericObjectPooling<T> self { get; private set; }
	private Queue<T> objects = new Queue<T>();

	private void Awake()
	{
		self = this;
	}

	public T Get()
	{
		if (objects.Count.Equals(0))
			AddBullets(1);

		return objects.Dequeue();
	}

	public void ReturnToPool(T newObj)
	{
		newObj.gameObject.SetActive(false);
		objects.Enqueue(newObj);
	}

	private void AddBullets(int amount)
	{
		for (int i = 0; i < amount; i++) // WHY FOR ? cus may be we know at the begining how much balls we need and can call
		{
			var newBall = Instantiate(prefab);			
			ReturnToPool(newBall);
		}
	}

}
