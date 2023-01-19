using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float maxHealth;

    public Health playerHealth { get; private set; }

    private void Start()
    {
        playerHealth = new Health(maxHealth);
    }

}
