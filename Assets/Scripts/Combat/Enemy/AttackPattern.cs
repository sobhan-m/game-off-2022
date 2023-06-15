using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackPattern : ScriptableObject
{
	public GameObject missilePrefab;
	public int numberOfAttacks;
	public float secondsBetweenAttacks;
}
