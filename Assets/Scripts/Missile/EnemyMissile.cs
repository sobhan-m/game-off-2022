using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour, IMissile, IDamageable
{
	[SerializeField] float damage;

	private Player player;
	private Pathfinding pathfinding;
	private Rigidbody2D rb;


	private void Awake()
	{
		player = FindObjectOfType<Player>();

		if (!player)
		{
			throw new MissingReferenceException("No Player Found In Scene");
		}

		rb = GetComponent<Rigidbody2D>();

		if (!rb)
		{
			throw new MissingComponentException("No rigidbody attached to this Missile.");
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (player.gameObject == collision.gameObject)
		{
			player.playerHealth.Damage(damage);
			Destroy(gameObject);
		}
	}

	public void Move()
	{
		if (pathfinding == null)
		{
			return;
		}

		rb.MovePosition(pathfinding.NextPosition(transform));
	}

	public void SetPathfinding(Pathfinding pathfinding)
	{
		this.pathfinding = pathfinding;
	}

	private void FixedUpdate()
	{
		Move();
	}

	public Health RetrieveHealth()
	{
		return new Health(1);
	}

	public void Die()
	{
		Destroy(this.gameObject);
	}
}
