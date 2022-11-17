using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum STATE { START, PLAYER_TURN, ENEMY_TURN, WIN, LOSE}

public class StateMachine : MonoBehaviour
{
    private STATE currentState;
    private ViewManager viewManager;
    private Enemy enemy;
    private Player player;

    private void Awake()
    {
        viewManager = FindObjectOfType<ViewManager>();
        enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        currentState = STATE.START;
        Transition();
    }



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
        viewManager.SetMessage("The Dragon Isn't In The Mood To Flirt. It Breathes Fire!");
        player.TakeDamage(enemy.GetDamage());
        viewManager.UpdatePlayerSlider();

        currentState = STATE.PLAYER_TURN;
        Transition(2);
    }

    private void StartState()
    {
        viewManager.SetMessage("An Attractive Dragon Approaches!");
        currentState = STATE.PLAYER_TURN;
        Transition(2);
    }

    private void PlayerState()
    {
        viewManager.SetMessage("Choose An Action.");
    }

    public void OnPlayerFlirt()
    {
        if (currentState != STATE.PLAYER_TURN)
            return;

        viewManager.SetMessage("You Successfully Flirt With The Dragon!");
        enemy.TakePoints(player.GetPoints());
        viewManager.UpdateEnemySlider();

        currentState = STATE.ENEMY_TURN;
        Transition(2);
    }

    public void OnPlayerEnd()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
