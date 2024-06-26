using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
	[SerializeField] int initialPositionIndex = 2;
	[SerializeField] List<Transform> initialTrackPositions;

	private InputAction movement;

	private Rigidbody2D rb;
	private Animator animator;
	public Track track { get; private set; }

	void Awake()
	{
		track = new Track(initialTrackPositions, initialPositionIndex);

		rb = GetComponent<Rigidbody2D>();
		transform.position = track.CurrentPosition().position;

		movement = (new PlayerInputActions()).Combat.Movement;
		movement.performed += ctx => Move();

		animator = GetComponent<Animator>();
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
			animator.SetTrigger("Right");
			transform.position = track.MoveNext().position;
		}
		else
		{
			animator.SetTrigger("Left");
			transform.position = track.MovePrevious().position;
		}
	}
}
