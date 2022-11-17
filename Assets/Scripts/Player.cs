using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float maxHP;
    [SerializeField] float currentHP;
    [SerializeField] float flirtPoints;
    [SerializeField] string playerName;

    public float GetMaxHP()
    {
        return maxHP;
    }

    public float GetCurrentHP()
    {
        return currentHP;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }

    public float GetPoints()
    {
        return flirtPoints;
    }


}
