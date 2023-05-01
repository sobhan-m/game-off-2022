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

	private float previousAction;

	void Awake()
	{
		playerInputActions = new PlayerInputActions();
		player = GetComponent<Player>();
		rb = GetComponent<Rigidbody2D>();
	}

    private void Start()
    {
		track = player.playerTrack;
		previousAction = 0;
	}

    void OnEnable()
    {
		movement = playerInputActions.Player.Movement;
		movement.Enable();
	}

    void OnDisable()
	{
		movement.Disable();
	}

	void FixedUpdate()
	{
		Move();
	}

	private void Move()
    {
		float movementCommand = movement.ReadValue<float>();
		// Debug.Log("PlayerController.Move(): " + movementCommand);

		// Ensuring no repeating moves.
		if (movementCommand == previousAction)
        {
			return;
        }
		else
        {
			previousAction = movementCommand;
        }

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
