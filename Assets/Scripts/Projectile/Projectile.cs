using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damagePerSecond;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();

        if (!player)
        {
            throw new MissingReferenceException("No Player Found In Scene");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (player.gameObject == collision.gameObject)
        {
            player.playerHealth.Damage(damagePerSecond * Time.deltaTime);
        }
    }
}
