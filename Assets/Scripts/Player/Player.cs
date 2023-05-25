using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Health")]
	[SerializeField] float maxHealth;
	public Health playerHealth { get; private set; }

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

	private void Die()
	{
		PlayerMovementController playerMovement = gameObject.GetComponent<PlayerMovementController>();
		playerMovement.enabled = false;

		SceneChangeManager sceneController = FindObjectOfType<SceneChangeManager>();
		sceneController.LoadGameOver();
	}

}
