using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour, IMissile
{
	[Header("Damage")]
	[SerializeField] private float initialDamage;
	[SerializeField] private MissileType initialDamageType;
	[SerializeField] private int pierceCount = 3;
	private MissileDamage damage;

	[Header("Effect")]
	[SerializeField] private float durationInSeconds = 3;
	[SerializeField] private float optionalValue = 5;
	[SerializeField] private GameObject visualEffectPrefab;
	private MissileEffect effect;

	[Header("Movement")]
	[SerializeField] public float speed;
	private Rigidbody2D rb;

	[Header("Visuals")]
	[SerializeField] GameObject particles;

	private void Awake()
	{
		rb = FindObjectOfType<Rigidbody2D>();
		if (!rb)
		{
			throw new MissingReferenceException("This object does not have a RigidBody attached!");
		}

		damage = MissileDamage.ConstructMissileDamage(initialDamageType, initialDamage);
		effect = MissileEffect.CreateMissileEffect(initialDamageType, durationInSeconds, optionalValue, visualEffectPrefab);
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
			pierceCount--;
		}

		if (effect != null && other.TryGetComponent<IAffectable>(out IAffectable affectable))
		{
			affectable.StoreEffect(effect);
		}

		if (pierceCount <= 0 || other.TryGetComponent<EnemyHealthManager>(out EnemyHealthManager enemy))
		{
			Instantiate(particles, transform.position, particles.transform.rotation);
			Destroy(gameObject);
		}
	}

	public void Move()
	{
		rb.velocity = new Vector2(0, speed);
	}
}
