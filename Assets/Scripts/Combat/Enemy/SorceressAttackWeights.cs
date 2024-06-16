using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SorceressAttackWeights", menuName = "SorceressAttackWeights", order = 0)]
public class SorceressAttackWeights : ScriptableObject
{
	public int[] attackAllButOneDiceRange;
	public int[] attackThreeRange;
	public int[] attackOddThenEvenRange;
	public int[] attackEvenThenOddRange;
	public int[] attackFollowingPlayerRange;
}