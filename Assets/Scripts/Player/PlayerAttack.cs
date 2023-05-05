using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private InputAction attackAction;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        attackAction = playerInputActions.Player.Attack;
        attackAction.Enable();
        attackAction.performed += Attack;
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        Debug.Log("PlayerAttack.Attack(): Attack!");
    }

    void OnDisable()
    {
        attackAction.Disable();
    }
}
