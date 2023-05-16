using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDestroyer : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		DestroyMissiles(collision);
	}

	private void DestroyMissiles(Collider2D collision)
	{
		IMissile missile = collision.gameObject.GetComponent<IMissile>();

		if (missile != null)
		{
			Destroy(collision.gameObject);
		}
	}
}
