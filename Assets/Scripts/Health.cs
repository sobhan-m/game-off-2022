using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public float currentHealth { get; private set; }
    public float maxHealth { get; private set; }

    public Health(float maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }

    public void Damage(float damageAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth - damageAmount, 0, maxHealth);
        Debug.Log(currentHealth);
    }

    public void Heal(float healAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);
    }

}
