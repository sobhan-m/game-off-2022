using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHP;
    [SerializeField] float currentHP;
    [SerializeField] float damage;

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }

    public void Die()
    {

    }

}
