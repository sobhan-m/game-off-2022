using UnityEngine;
using UnityEngine.UI;

public class AttackBuildupTracker : MonoBehaviour
{
	private Slider buildUpSlider;
	private PlayerAttack playerAttack;
	private void Awake()
	{
		playerAttack = FindObjectOfType<PlayerAttack>();
		if (!playerAttack)
		{
			throw new MissingReferenceException("No PlayerAttack script exists in this scene.");
		}


		buildUpSlider = GetComponent<Slider>();
		if (!buildUpSlider)
		{
			throw new MissingReferenceException("No Slider component exists on this object.");
		}
	}

	private void Start()
	{
		buildUpSlider.minValue = playerAttack.attackMeter.minValue;
		buildUpSlider.maxValue = playerAttack.attackMeter.maxValue;
		buildUpSlider.value = playerAttack.attackMeter.currentValue;
	}

	private void Update()
	{
		buildUpSlider.value = playerAttack.attackMeter.currentValue;
	}
}
