using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour, IMissile
{
	[SerializeField]
	public float speed { get; private set; }

	private Rigidbody2D rb;

	private void Awake()
	{
		rb = FindObjectOfType<Rigidbody2D>();
		if (!rb)
		{
			throw new MissingReferenceException("This object does not have a RigidBody attached!");
		}
	}

	private void Start()
	{
		Move();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Enemy")
		{
			return;
		}

		EnemyMissile enemyMissile = other.GetComponent<EnemyMissile>();
		if (enemyMissile)
		{
			Destroy(enemyMissile.gameObject);
		}
	}

	public void Move()
	{
		rb.velocity = new Vector2(0, speed);
	}
}
