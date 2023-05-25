using UnityEngine;
using UnityEngine.UI;

public class AttackBuildupTracker : MonoBehaviour
{
	private Slider buildUpSlider;
	private Meter attackMeter;
	private void Awake()
	{
		buildUpSlider = GetComponent<Slider>();
		if (!buildUpSlider)
		{
			throw new MissingReferenceException("No Slider component exists on this object.");
		}
	}

	private void Start()
	{
		PlayerAttack playerAttack = FindObjectOfType<PlayerAttack>();
		if (!playerAttack)
		{
			throw new MissingReferenceException("No PlayerAttack script exists in this scene.");
		}
		attackMeter = playerAttack.attackMeter;


		buildUpSlider.minValue = attackMeter.minValue;
		buildUpSlider.maxValue = attackMeter.maxValue;
		buildUpSlider.value = attackMeter.currentValue;
	}

	private void Update()
	{
		buildUpSlider.value = attackMeter.currentValue;
	}
}
