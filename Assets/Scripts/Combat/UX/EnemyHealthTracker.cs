using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthTracker : MonoBehaviour
{
	[SerializeField] Slider healthSlider;
	private EnemyHealthManager enemy;
	private Health health;
	private Camera mainCamera;
	private RectTransform rectTransform;

	private void Awake()
	{
		rectTransform = gameObject.GetComponent<RectTransform>();
		if (!rectTransform)
		{
			throw new MissingComponentException("No RectTransform on this object.");
		}
	}

	private void Start()
	{
		enemy = FindObjectOfType<EnemyHealthManager>();
		if (!enemy)
		{
			throw new MissingReferenceException("No enemies in this scene.");
		}
		health = enemy.health;

		healthSlider.minValue = 0;
		healthSlider.maxValue = health.maxHealth;
		healthSlider.value = health.currentHealth;

		mainCamera = Camera.main;

		UpdatePosition();
	}

	private void Update()
	{
		if (!enemy)
		{
			Destroy(gameObject);
			return;
		}
		healthSlider.value = health.currentHealth;
		UpdatePosition();
	}

	private void UpdatePosition()
	{
		Vector3 enemyPosition = mainCamera.WorldToScreenPoint(enemy.gameObject.transform.position);
		Vector3 oldPosition = rectTransform.position;
		rectTransform.position = new Vector3(enemyPosition.x, oldPosition.y, oldPosition.z);
	}
}
