using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
	public float speed { get; private set; }
	private int currentIndex;
	private List<Transform> path;
	public Pathfinding(float speed, List<Transform> path)
	{
		this.speed = speed;
		this.path = path;
		this.currentIndex = 0;
	}


	public Vector3 NextPosition(Transform transform)
	{
		if (currentIndex <= path.Count && transform.position == path[currentIndex].position)
		{
			++currentIndex;
		}

		Vector3 intermediatePosition = Vector3.MoveTowards(transform.position, path[currentIndex].position, speed * Time.fixedDeltaTime * Settings.combatSpeedMultiplier);
		return intermediatePosition;
	}
}
