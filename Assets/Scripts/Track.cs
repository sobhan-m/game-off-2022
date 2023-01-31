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

    public Transform CurrentPosition()
    {
        return positions[currentIndex];
    }

}
