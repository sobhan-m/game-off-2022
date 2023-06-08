using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{
	[SerializeField] List<Transform> initialTrack;
	[SerializeField] float delaySeconds;
	[Range(0f, 1f)][SerializeField] float fearFactor = 0.4f;
	public Track track { get; private set; }
	private PlayerMovementController player;
	private Meter waitMeter;
	private Health health;
	private Meter attackProgress;

	private void Awake()
	{
		track = new Track(initialTrack, 2);
		transform.position = track.CurrentPosition().position;

		waitMeter = new Meter(0, delaySeconds);

		player = FindObjectOfType<PlayerMovementController>();
		if (!player)
		{
			throw new MissingReferenceException("No player exists in this scene.");
		}
	}

	private void Start()
	{
		if (gameObject.TryGetComponent<EnemyHealthManager>(out EnemyHealthManager healthManager))
		{
			health = healthManager.health;
		}

		if (gameObject.TryGetComponent<EnemyAttackManager>(out EnemyAttackManager attackManager))
		{
			attackProgress = attackManager.patternProgress;
		}

		if (health == null || attackProgress == null)
		{
			throw new MissingReferenceException("Make sure there is a health and attack manager attached to this object.");
		}
	}

	private void Update()
	{
		waitMeter.FillMeter(Time.deltaTime);
		if (!waitMeter.IsFull())
		{
			return;
		}

		if (!HasProjectile(track.CurrentPosition()))
		{
			transform.position = ChooseNextStep().position;
			waitMeter.EmptyMeter();
			return;
		}

		AvoidProjectile();
		waitMeter.EmptyMeter();
	}

	private Transform ChooseNextStep()
	{
		Transform nextStep = FollowPlayer();
		bool hasProjectile = HasProjectile(nextStep);
		if (!hasProjectile)
		{
			track.MoveToPosition(nextStep.position);
			return nextStep;
		}
		else if (Random.Range(0f, 1f) >= AnalyzeRisk(hasProjectile))
		{
			track.MoveToPosition(nextStep.position);
			return nextStep;
		}
		else
		{
			return transform;
		}
	}

	private Transform FollowPlayer()
	{
		if (track.currentIndex < player.track.currentIndex)
		{
			return track.GetNext();
		}
		else if (track.currentIndex > player.track.currentIndex)
		{
			return track.GetPrevious();
		}
		else
		{
			return track.CurrentPosition();
		}
	}

	private bool HasProjectile(Transform lane)
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll(lane.position, Vector2.down);

		foreach (RaycastHit2D hit in hits)
		{
			if (hit.collider == null)
			{
				continue;
			}

			if (hit.collider.gameObject.TryGetComponent<PlayerMissile>(out PlayerMissile missile))
			{
				return true;
			}
		}

		return false;
	}

	private void AvoidProjectile()
	{
		if (track.IsLeftMost())
		{
			transform.position = track.MoveNext().position;
		}
		else if (track.IsRightMost())
		{
			transform.position = track.MovePrevious().position;
		}
		else if (Random.Range(0f, 1f) >= 0.5f)
		{
			transform.position = track.MoveNext().position;
		}
		else
		{
			transform.position = track.MovePrevious().position;
		}
	}

	// 1 means high risk.
	// 0 means low risk.
	private float AnalyzeRisk(bool hasProjectile)
	{
		float riskFactor = 0f;

		if (attackProgress.IsFull())
		{
			riskFactor += fearFactor;
		}
		if (health.currentHealth / health.maxHealth < 0.25f)
		{
			riskFactor += fearFactor;
		}
		if (hasProjectile)
		{
			riskFactor += fearFactor;
		}

		return riskFactor / 3;
	}

}
