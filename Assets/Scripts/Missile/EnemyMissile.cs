using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour, IMissile
{
	[SerializeField] float damagePerSecond;

	private Player player;
	private Pathfinding pathfinding;
	private Rigidbody2D rb;


	private void Start()
	{
		player = FindObjectOfType<Player>();

		if (!player)
		{
			throw new MissingReferenceException("No Player Found In Scene");
		}

		rb = GetComponent<Rigidbody2D>();

		if (!player)
		{
			throw new MissingComponentException("No rigidbody attached to this Missile.");
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (player.gameObject == collision.gameObject)
		{
			player.playerHealth.Damage(damagePerSecond * Time.deltaTime);
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


}
