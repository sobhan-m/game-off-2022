using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private PlayerInputActions playerInputActions;
	private Rigidbody2D rb;
	private InputAction movement;

	[SerializeField] float maxSpeed = 3f;
	[SerializeField] float accelerationConst = 5f;
	[SerializeField] float sideChangingConstant = 2f;

	void Awake()
	{
		playerInputActions = new PlayerInputActions();
		rb = GetComponent<Rigidbody2D>();
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
		Vector2 currentVelocity = rb.velocity;
		Vector2 movementCommand = movement.ReadValue<Vector2>();
		/*Vector2 changeInVelocity = movementCommand * accelerationConst * Time.fixedDeltaTime;

		float newX = Mathf.Clamp(currentVelocity.x + changeInVelocity.x, -1 * maxSpeed, maxSpeed);
		float newY = Mathf.Clamp(currentVelocity.y + changeInVelocity.y, -1 * maxSpeed, maxSpeed);*/

		// Handling X.
		float newX;
		if (movementCommand.x != 0)
        {
			float factor = 1f;
			if (-1 * Mathf.Sign(movementCommand.x) == Mathf.Sign(currentVelocity.x))
            {
				factor = sideChangingConstant;
            }


			float changeInVelocity = movementCommand.x * accelerationConst * Time.fixedDeltaTime * factor; // a * t
			newX = Mathf.Clamp(currentVelocity.x + changeInVelocity, -1 * maxSpeed, maxSpeed); // vi + a * t
		}
		else
        {
			float changeInVelocity = accelerationConst * Time.fixedDeltaTime;
			newX = Mathf.Sign(currentVelocity.x) * Mathf.Clamp(Mathf.Abs(currentVelocity.x) - Mathf.Abs(changeInVelocity), 0, maxSpeed); // Deceleration.
		}

		// Handling Y.
		float newY;
		if (movementCommand.y != 0)
		{
			// Deceleration happens faster when switching sides.
			float factor = 1f;
			if (-1 * Mathf.Sign(movementCommand.x) == Mathf.Sign(currentVelocity.x))
			{
				factor = sideChangingConstant;
			}

			float changeInVelocity = movementCommand.y * accelerationConst * Time.fixedDeltaTime * factor;
			newY = Mathf.Clamp(currentVelocity.y + changeInVelocity, -1 * maxSpeed, maxSpeed);
		}
		else
		{
			float changeInVelocity = accelerationConst * Time.fixedDeltaTime;
			newY = Mathf.Sign(currentVelocity.y) * Mathf.Clamp(Mathf.Abs(currentVelocity.y) - Mathf.Abs(changeInVelocity), 0, maxSpeed);
		}

        // Check when diagonal.

        rb.velocity = new Vector2(newX, newY);
        /*rb.AddForce(new Vector2(newX, newY), ForceMode2D.Impulse);*/

        Debug.Log("PlayerController.Move(): " + rb.velocity);
	}
}
