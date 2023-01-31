using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [Header("Path")]
    [SerializeField] float speed;

    public List<Transform> pointsInPath;

    private Transform nextDestination;
    private Track path;
    private Rigidbody2D rb;

    [Header("Damage")]
    [SerializeField] float damagePerSecond;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        path = new Track(pointsInPath);
    }

    private void Start()
    {
        transform.position = path.CurrentPosition().position;
        nextDestination = path.MoveNext();
    }

    private void FixedUpdate()
    {
        if (transform.position.Equals(nextDestination.position))
        {
            nextDestination = path.MoveNext();
        }

        Vector3 intermediatePosition = Vector3.MoveTowards(transform.position, nextDestination.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(intermediatePosition);
    }

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
