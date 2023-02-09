using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damagePerSecond;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (!player)
        {
            return;
        }

        player.playerHealth.Damage(damagePerSecond * Time.deltaTime);
    }
}
