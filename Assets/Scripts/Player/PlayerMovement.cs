using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] int initialPositionIndex = 2;
	[SerializeField] List<Transform> initialTrackPositions;

	private InputAction movement;

	private Rigidbody2D rb;
	private Track track;

	void Awake()
	{
		track = new Track(initialTrackPositions, initialPositionIndex);

		rb = GetComponent<Rigidbody2D>();
		transform.position = track.CurrentPosition().position;

		movement = (new PlayerInputActions()).Player.Movement;
		movement.performed += ctx => Move();
	}

	void OnEnable()
	{
		movement.Enable();
	}

	void OnDisable()
	{
		movement.Disable();
	}

	private void Move()
	{
		if (PauseController.IsPaused())
		{
			return;
		}

		float movementCommand = movement.ReadValue<float>();

		// Moving player.
		if (movementCommand == 0)
		{
			transform.position = track.CurrentPosition().position;
		}
		else if (Mathf.Sign(movementCommand) > 0)
		{
			transform.position = track.MoveNext().position;
		}
		else
		{
			transform.position = track.MovePrevious().position;
		}
	}
}
