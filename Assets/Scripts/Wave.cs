using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Wave Configuration")]
public class Wave : ScriptableObject
{
    public float secondsBetweenProjectiles;
    public List<Transform> paths;
}
