using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldAbilityTracker : MonoBehaviour
{
	[SerializeField] Slider cooldownSlider;
	[SerializeField] Image shieldImage;
	private Meter shieldCooldown;
	private PlayerAbilityController player;
	private bool previousHasShield;

	void Start()
	{
		player = FindObjectOfType<PlayerAbilityController>();
		if (!player)
		{
			throw new MissingReferenceException("No PlayerAbilityController in the scene.");

		}

		if (!player.available.hasShield)
		{
			gameObject.SetActive(false);
			return;
		}

		shieldCooldown = player.shieldCooldown;
		cooldownSlider.minValue = shieldCooldown.minValue;
		cooldownSlider.maxValue = shieldCooldown.maxValue;
		cooldownSlider.value = shieldCooldown.currentValue;

		previousHasShield = player.hasShield;
		shieldImage.gameObject.SetActive(previousHasShield);
	}

	void Update()
	{
		cooldownSlider.value = shieldCooldown.currentValue;
		if (player.hasShield != previousHasShield)
		{
			ToggleShield();
		}
	}

	private void ToggleShield()
	{
		previousHasShield = player.hasShield;
		shieldImage.gameObject.SetActive(previousHasShield);
	}
}
