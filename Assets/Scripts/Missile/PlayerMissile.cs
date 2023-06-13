using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour, IMissile
{
	[Header("Damage")]
	[SerializeField] private float initialDamage;
	[SerializeField] private PlayerMissileType initialDamageType;
	private MissileDamage damage;
	private MissileEffect effect;

	[Header("Movement")]
	[SerializeField] public float speed;
	private Rigidbody2D rb;

	private void Awake()
	{
		rb = FindObjectOfType<Rigidbody2D>();
		if (!rb)
		{
			throw new MissingReferenceException("This object does not have a RigidBody attached!");
		}

		damage = MissileDamage.ConstructMissileDamage(initialDamageType, initialDamage);
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

		if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
		{
			damage.ApplyDamage(damageable);
		}

		if (other.TryGetComponent<EnemyHealthManager>(out EnemyHealthManager enemy))
		{
			Destroy(gameObject);
		}
	}

	public void Move()
	{
		rb.velocity = new Vector2(0, speed);
	}
}
