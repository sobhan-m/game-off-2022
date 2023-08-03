using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
	[SerializeField] int remainingHits;
	[SerializeField] GameObject shatterVFX;
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.TryGetComponent<EnemyMissile>(out EnemyMissile enemy))
		{
			return;
		}

		Destroy(enemy.gameObject);

		if (--remainingHits <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		PlayerAbilityController player = FindObjectOfType<PlayerAbilityController>();
		if (player == null)
		{
			return;
		}

		player.EndShield();
		Instantiate(shatterVFX, transform.position, shatterVFX.transform.rotation);
		Destroy(gameObject);
	}
}
