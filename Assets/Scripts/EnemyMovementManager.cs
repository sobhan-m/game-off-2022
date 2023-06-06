using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{
	[SerializeField] List<Transform> initialTrack;
	[SerializeField] float delaySeconds;
	public Track track { get; private set; }
	private PlayerMovementController player;
	private Meter waitMeter;

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

	private void Update()
	{
		waitMeter.FillMeter(Time.deltaTime);
		if (!waitMeter.IsFull())
		{
			return;
		}

		if (!HasProjectile(track.CurrentPosition()))
		{
			transform.position = FollowPlayer().position;
			waitMeter.EmptyMeter();
			return;
		}

		AvoidProjectile();
	}

	private Transform FollowPlayer()
	{
		if (track.currentIndex < player.track.currentIndex)
		{
			return track.MoveNext();
		}
		else if (track.currentIndex > player.track.currentIndex)
		{
			return track.MovePrevious();
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
		else if (Random.Range(0, 2) == 0)
		{
			transform.position = track.MoveNext().position;
		}
		else
		{
			transform.position = track.MovePrevious().position;
		}
	}

}
