using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
	[SerializeField] AttackPattern[] attacks;
	[SerializeField] float secondsBetweenPatterns;
	public Meter patternWait { get; private set; }
	public Meter attackWait { get; private set; }
	public Meter patternProgress { get; private set; }
	public AttackPattern currentAttack { get; private set; }

	private void Awake()
	{
		patternWait = new Meter(0, secondsBetweenPatterns);

		ResetAttackPattern();
	}

	private void Attack()
	{
		GameObject missile = Instantiate(currentAttack.missilePrefab, transform.position, Quaternion.identity);
	}

	private void Update()
	{
		if (patternProgress.IsFull()) // We're done with this attack pattern.
		{
			patternWait.FillMeter(Time.deltaTime);
		}
		else
		{
			attackWait.FillMeter(Time.deltaTime);
		}

		if (patternWait.IsFull()) // We're done waiting for the next attack pattern.
		{
			patternProgress.EmptyMeter();
			ResetAttackPattern();
			attackWait.FillMeter();
			patternWait.EmptyMeter();
		}

		if (attackWait.IsFull()) // We're done waiting for the next attack.
		{
			Attack();
			patternProgress.FillMeter(1f);
			attackWait.EmptyMeter();
		}
	}

	private void ResetAttackPattern()
	{
		currentAttack = attacks[Random.Range(0, attacks.Length)];
		attackWait = new Meter(0, currentAttack.secondsBetweenAttacks);
		patternProgress = new Meter(0, currentAttack.numberOfAttacks);
	}
}
