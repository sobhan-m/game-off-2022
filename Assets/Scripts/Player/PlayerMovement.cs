using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	private InputAction movement;
	private PlayerInputActions playerInputActions;

	private Player player;
	private Rigidbody2D rb;
	private Track track;

	void Awake()
	{
		playerInputActions = new PlayerInputActions();
		player = GetComponent<Player>();
		rb = GetComponent<Rigidbody2D>();
		movement = playerInputActions.Player.Movement;
		movement.performed += ctx => Move();
	}

	private void Start()
	{
		track = player.playerTrack;
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
		Transform newTransform;
		if (movementCommand == 0)
		{
			newTransform = track.CurrentPosition();
		}
		else if (Mathf.Sign(movementCommand) > 0)
		{
			newTransform = track.MoveNext();
		}
		else
		{
			newTransform = track.MovePrevious();
		}
		player.transform.position = newTransform.position;
	}
}
