using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageAbilityTracker : MonoBehaviour
{
	[SerializeField] Slider cooldownSlider;
	[SerializeField] Slider durationSlider;
	private Meter rageCooldown;
	private Meter rageDuration;

	void Start()
	{
		PlayerAbilityController abilityController = FindObjectOfType<PlayerAbilityController>();
		if (!abilityController)
		{
			throw new MissingReferenceException("No PlayerAbilityController in the scene.");

		}

		if (!abilityController.available.hasRage)
		{
			gameObject.SetActive(false);
			return;
		}

		// Cooldown.
		rageCooldown = abilityController.rageCooldown;
		cooldownSlider.minValue = rageCooldown.minValue;
		cooldownSlider.maxValue = rageCooldown.maxValue;
		cooldownSlider.value = rageCooldown.currentValue;

		// Duration.
		rageDuration = abilityController.rageDuration;
		durationSlider.minValue = rageDuration.minValue;
		durationSlider.maxValue = rageDuration.maxValue;
		durationSlider.value = rageDuration.currentValue;
	}

	void Update()
	{
		cooldownSlider.value = rageCooldown.currentValue;
		durationSlider.value = rageDuration.currentValue;
	}
}
