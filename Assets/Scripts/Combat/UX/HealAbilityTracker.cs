using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealAbilityTracker : MonoBehaviour
{
	[SerializeField] Slider cooldownSlider;
	private Meter healCooldown;

	void Start()
	{
		PlayerAbilityController abilityController = FindObjectOfType<PlayerAbilityController>();
		if (!abilityController)
		{
			throw new MissingReferenceException("No PlayerAbilityController in the scene.");

		}

		if (!abilityController.available.hasHeal)
		{
			gameObject.SetActive(false);
		}

		healCooldown = abilityController.healCooldown;
		cooldownSlider.minValue = healCooldown.minValue;
		cooldownSlider.maxValue = healCooldown.maxValue;
		cooldownSlider.value = healCooldown.currentValue;
	}

	void Update()
	{
		cooldownSlider.value = healCooldown.currentValue;
	}
}
