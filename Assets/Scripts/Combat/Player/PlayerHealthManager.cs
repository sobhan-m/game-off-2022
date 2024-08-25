using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, IDamageable
{
	[Header("Health")]
	[SerializeField] float maxHealth;
	public Health playerHealth { get; private set; }
	[Header("VFX")]
	[SerializeField] GameObject deathParticles;

	private void Awake()
	{
		playerHealth = new Health(maxHealth);
	}

	private void Update()
	{
		if (playerHealth.IsDead())
		{
			Die();
		}
	}

	public void Die()
	{
		Instantiate(deathParticles, transform.position, deathParticles.transform.rotation);

		PlayerMovementController playerMovement = gameObject.GetComponent<PlayerMovementController>();
		playerMovement.enabled = false;

		SceneChangeManager sceneController = FindObjectOfType<SceneChangeManager>();
		sceneController.SaveCurrentScene();
		sceneController.LoadGameOver();
	}

	public Health RetrieveHealth()
	{
		return playerHealth;
	}

}
