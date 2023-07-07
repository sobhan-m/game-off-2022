using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour, IMissile, IDamageable, IFreezable
{
	[Header("Damage")]
	[SerializeField] private float initialDamage;
	private MissileDamage damage;

	[Header("Movement")]
	[SerializeField] public float speed;
	private Rigidbody2D rb;
	private int numOfFreezes;

	[Header("Visuals")]
	[SerializeField] GameObject deathParticleEffect;
	[SerializeField] GameObject hitParticleEffect;



	private void Awake()
	{
		if (!TryGetComponent<Rigidbody2D>(out rb))
		{
			throw new MissingComponentException("No rigidbody attached to this Missile.");
		}

		damage = new EnemyDamage(initialDamage);
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag != "Player")
		{
			return;
		}

		if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
		{
			damage.ApplyDamage(damageable);
		}

		if (collision.TryGetComponent<PlayerHealthManager>(out PlayerHealthManager player))
		{
			Instantiate(hitParticleEffect, transform.position, hitParticleEffect.transform.rotation);
			Destroy(gameObject);
		}
	}

	public void Move()
	{
		Vector2 displacement = new Vector2(0, -speed * Time.fixedDeltaTime);
		rb.MovePosition(rb.position + displacement);
	}

	public Health RetrieveHealth()
	{
		return new Health(1);
	}

	public void Die()
	{
		Instantiate(deathParticleEffect, transform.position, deathParticleEffect.transform.rotation);
		Destroy(this.gameObject);
	}

	public void Freeze()
	{
		numOfFreezes++;
		if (numOfFreezes > 0)
		{
			this.enabled = false;
		}
	}

	public void Unfreeze()
	{
		numOfFreezes = Mathf.Max(numOfFreezes - 1, 0);
		if (numOfFreezes <= 0)
		{
			this.enabled = true;
		}
	}
}
