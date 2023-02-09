using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] float speed;

    public List<Transform> path;

    private int currentIndex;
    private Rigidbody2D rb;

    private void Awake()
    {
        currentIndex = 0;
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.Log("Pathfinding.Awake(): No RigidBody()");
        }
    }

    private void Start()
    {
        if (path != null)
        {
            transform.position = path[currentIndex].position;
        }
    }

    private void Move()
    {
        if (currentIndex <= path.Count && transform.position == path[currentIndex].position)
        {
            ++currentIndex;
        }

        Vector3 intermediatePosition = Vector3.MoveTowards(transform.position, path[currentIndex].position, speed * Time.deltaTime);
        rb.position = intermediatePosition;
    }

    private void Update()
    {
        Move();
    }

}
