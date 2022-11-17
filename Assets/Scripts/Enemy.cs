using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxPoints;
    [SerializeField] float currentPoints;
    [SerializeField] float damage;
    [SerializeField] string enemyName;

    public float GetMaxPoints()
    {
        return maxPoints;
    }
    
    public float GetCurrentPoints()
    {
        return currentPoints;
    }

    public float GetDamage()
    {
        return damage;
    }

    public string GetEnemyName()
    {
        return enemyName;
    }

    public void TakePoints(float points)
    {
        currentPoints += damage;
    }

    public bool IsDead()
    {
        /*return currentPoints <= 0;*/
        return true;
    }

    public void Die()
    {

    }

    public void DealDamage()
    {

    }

}
