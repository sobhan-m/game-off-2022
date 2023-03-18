using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] List<Transform> path;

    private int currentIndex;
    private bool hasStartedPath;

    private Rigidbody2D rb;

    private void Awake()
    {
        currentIndex = 0;
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            throw new MissingReferenceException("No RigidBody2D on this GameObject.");
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
        if (hasStartedPath)
        {
            Move();
        }
    }

    public void BeginPath(List<Transform> path, float speed)
    {
        this.path = path;
        this.speed = speed;
        hasStartedPath = true;

        transform.position = path[currentIndex].position;
    }

    public void BeginPath(List<Transform> path)
    {
        this.path = path;
        hasStartedPath = true;
        transform.position = path[currentIndex].position;
    }

    public void BeginPath(float speed)
    {
        this.speed = speed;
        hasStartedPath = true;

        transform.position = path[currentIndex].position;
    }

    public void BeginPath()
    {
        hasStartedPath = true;
        transform.position = path[currentIndex].position;
    }
    

}
