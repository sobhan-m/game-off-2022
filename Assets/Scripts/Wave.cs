using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Wave Configuration")]
public class Wave : ScriptableObject
{
    [SerializeField] float secondsBetweenProjectiles;
    [SerializeField] List<Transform> pathPrefab;

    public float GetSecondsBetweenProjectiles()
    {
        return secondsBetweenProjectiles;
    }

    public int NumberOfPaths()
    {
        return pathPrefab.Count;
    }

    public List<Transform> PathAtIndex(int index)
    {
        Transform path = pathPrefab[index];

        List<Transform> points = new List<Transform>();
        for (int i = 0; i < path.childCount; ++i)
        {
            points.Add(path.GetChild(i));
        }

        return points;
    }

    public List<Transform> GetAllPaths()
    {
        return pathPrefab;
    }

}
