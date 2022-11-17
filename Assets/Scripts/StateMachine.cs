using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE { START, PLAYER_TURN, ENEMY_TURN, WIN, LOSE}

public class StateMachine : MonoBehaviour
{
    private STATE currentState;


    private IEnumerator Transition(float pause=0f)
    {
        yield return new WaitForSeconds(pause);

        switch (currentState)
        {
            case STATE.START:
                StartState();
                break;
            case STATE.PLAYER_TURN:
                PlayerState();
                break;
            case STATE.ENEMY_TURN:
                EnemyState();
                break;
            case STATE.LOSE:
                LoseState();
                break;
            case STATE.WIN:
                WinState();
                break;
            default:
                break;
        }

    }

    private void WinState()
    {
        throw new NotImplementedException();
    }

    private void LoseState()
    {
        throw new NotImplementedException();
    }

    private void EnemyState()
    {
        throw new NotImplementedException();
    }

    private void StartState()
    {
        throw new NotImplementedException();
    }

    private void PlayerState()
    {
        
    }
}
