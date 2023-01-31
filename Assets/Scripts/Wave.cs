using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave")]
public class Wave : ScriptableObject
{
    public int numberOfProjectiles;
    public float secondsBetweenSpawns;

    public List<Transform> startPoints;
    public List<Transform> endPoints;
}
