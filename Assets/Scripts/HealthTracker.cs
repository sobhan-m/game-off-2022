using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{
	[SerializeField] Slider healthSlider;

	private Health playerHealth;

	private void Start()
	{
		playerHealth = FindObjectOfType<Player>().playerHealth;

		healthSlider.minValue = 0;
		healthSlider.maxValue = playerHealth.maxHealth;
		healthSlider.value = playerHealth.currentHealth;
	}

	private void Update()
	{
		healthSlider.value = playerHealth.currentHealth;
	}
}
