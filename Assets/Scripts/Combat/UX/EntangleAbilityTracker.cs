using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntangleAbilityTracker : MonoBehaviour
{
	[SerializeField] Slider cooldownSlider;
	[SerializeField] Slider durationSlider;
	private Meter entangleCooldown;
	private Meter entangleDuration;

	void Start()
	{
		PlayerAbilityController abilityController = FindObjectOfType<PlayerAbilityController>();
		if (!abilityController)
		{
			throw new MissingReferenceException("No PlayerAbilityController in the scene.");

		}

		if (!abilityController.available.hasEntangle)
		{
			gameObject.SetActive(false);
			return;
		}

		// Cooldown.
		entangleCooldown = abilityController.entangleCooldown;
		cooldownSlider.minValue = entangleCooldown.minValue;
		cooldownSlider.maxValue = entangleCooldown.maxValue;
		cooldownSlider.value = entangleCooldown.currentValue;

		// Duration.
		entangleDuration = abilityController.entangleDuration;
		durationSlider.minValue = entangleDuration.minValue;
		durationSlider.maxValue = entangleDuration.maxValue;
		durationSlider.value = entangleDuration.currentValue;
	}

	void Update()
	{
		cooldownSlider.value = entangleCooldown.currentValue;
		durationSlider.value = entangleDuration.currentValue;
	}
}
