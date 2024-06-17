using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DragonAttackWeights", menuName = "DragonAttackWeights", order = 0)]
public class DragonAttackWeights : ScriptableObject
{
    public int[] attackPlayerPositionRange;
	public int[] attackOuterToInnerRange;
	public int[] attackInnerToOuterRange;
	public int[] attackAllExceptRightmostRange;
	public int[] attackAllExceptLeftmostRange;
}