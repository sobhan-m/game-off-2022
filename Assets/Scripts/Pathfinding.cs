using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] List<Transform> pointsInPath;

    private int pointIndex;
    private Transform nextDestination;
    private Rigidbody2D rb;

    private void Awake()
    {
        pointIndex = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        nextDestination = pointsInPath[pointIndex];
        transform.position = nextDestination.position;
    }

    private void FixedUpdate()
    {
        if (transform.position.Equals(nextDestination.position))
        {
            ++pointIndex;
            if (pointIndex >= pointsInPath.Count)
            {
                return;
            }
            else
            {
                nextDestination = pointsInPath[pointIndex];
            }
        }

        Vector3 intermediatePosition = Vector3.MoveTowards(transform.position, nextDestination.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(intermediatePosition);
    }

    public void SetPointsInPath(List<Transform> newPath)
    {
        pointsInPath = newPath;
    }
}
