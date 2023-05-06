using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
	[SerializeField] float speed;

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
		rb.velocity = new Vector2(0, speed);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Enemy")
		{
			return;
		}

		Projectile enemyProjectile = other.GetComponent<Projectile>();
		if (enemyProjectile)
		{
			Destroy(enemyProjectile.gameObject);
		}
	}
}
