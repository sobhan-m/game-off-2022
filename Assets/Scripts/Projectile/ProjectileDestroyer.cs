using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		DestroyEnemyProjectiles(collision);
		DestroyPlayerProjectiles(collision);
	}

	private void DestroyEnemyProjectiles(Collider2D collision)
	{
		Projectile projectile = collision.gameObject.GetComponent<Projectile>();

		if (projectile)
		{
			Destroy(projectile.gameObject);
			return;
		}
	}

	private void DestroyPlayerProjectiles(Collider2D collision)
	{
		PlayerProjectile projectile = collision.gameObject.GetComponent<PlayerProjectile>();

		if (projectile)
		{
			Destroy(projectile.gameObject);
			return;
		}
	}
}
