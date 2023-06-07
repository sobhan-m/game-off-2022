using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track
{
	private List<Transform> positions;
	public int currentIndex { get; private set; }

	public Track(List<Transform> positions, int startingIndex = 0)
	{
		this.positions = positions;
		currentIndex = startingIndex;
	}

	public Transform MovePrevious()
	{
		currentIndex = Mathf.Clamp(currentIndex - 1, 0, positions.Count - 1);
		return positions[currentIndex];
	}

	public Transform MoveNext()
	{
		currentIndex = Mathf.Clamp(currentIndex + 1, 0, positions.Count - 1);
		return positions[currentIndex];
	}

	public Transform GetPrevious()
	{
		int index = Mathf.Clamp(currentIndex - 1, 0, positions.Count - 1);
		return positions[index];
	}

	public Transform GetNext()
	{
		int index = Mathf.Clamp(currentIndex + 1, 0, positions.Count - 1);
		return positions[index];
	}

	public Transform CurrentPosition()
	{
		return positions[currentIndex];
	}

	public bool IsLeftMost()
	{
		return currentIndex == 0;
	}

	public bool IsRightMost()
	{
		return currentIndex == positions.Count - 1;
	}

	public void MoveToPosition(Vector3 position)
	{
		for (int i = 0; i < positions.Count; ++i)
		{
			if (positions[i].position == position)
			{
				currentIndex = i;
				return;
			}
		}

		throw new System.ArgumentException("The given position was not on the track.");
	}

}
