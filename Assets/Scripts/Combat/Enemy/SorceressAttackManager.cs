using System;
using System.Collections;
using UnityEngine;

public class SorceressAttackManager : MonoBehaviour
{

    [SerializeField] float secondsBetweenAttacks;
    [SerializeField] float secondsBetweenAttackGaps;
    [SerializeField] Transform[] attackingPositions;
    [SerializeField] GameObject missile;
    [SerializeField] SorceressAttackWeights weights;
    private Track playerTrack;
    private Meter attackWait;
    private bool isAttacking;

    void Start()
    {
        playerTrack = FindObjectOfType<PlayerMovementController>().track;
        attackWait = new Meter(0, secondsBetweenAttacks);
        isAttacking = false;
    }

    void Update()
    {
        BuildUpAttack();
    }

    private void BuildUpAttack()
    {
        if (isAttacking)
        {
            return;
        }

        attackWait.FillMeter(Time.deltaTime);
        if (attackWait.IsFull())
        {
            PerformRandomAttack();
            attackWait.EmptyMeter();
        }
    }

    private void BasicAttack(int laneIndex)
    {
        Transform attackPosition = attackingPositions[laneIndex];
        GameObject.Instantiate(missile, attackPosition);
    }

    private void AttackAllLanes()
    {
        for (int i = 0; i < 5; ++i)
        {
            BasicAttack(i);
        }
    }

    private void AttackAllButOne()
    {
        int excludedIndex = UnityEngine.Random.Range(0, 5);
        for (int i = 0; i < 5; ++i)
        {
            if (i == excludedIndex)
            {
                continue;
            }

            BasicAttack(i);
        }
    }

    private void AttackThree()
    {
        int playerPosition = playerTrack.currentIndex;
        bool isLeftmost = playerTrack.IsLeftMost();
        bool isRightmost = playerTrack.IsRightMost();

        BasicAttack(playerPosition);
        if (!isLeftmost)
        {
            BasicAttack(playerPosition - 1);
        }
        if (!isRightmost)
        {
            BasicAttack(playerPosition + 1);
        }
    }

    private IEnumerator AttackOddThenEven()
    {
        isAttacking = true;
        AttackOdds();
        yield return new WaitForSeconds(secondsBetweenAttackGaps);
        AttackEvens();
        isAttacking = false;
    }

    private IEnumerator AttackEvensThenOdds()
    {
        isAttacking = true;
        AttackEvens();
        yield return new WaitForSeconds(secondsBetweenAttackGaps);
        AttackOdds();
        isAttacking = false;
    }

    private IEnumerator AttackFollowingPlayer()
    {
        isAttacking = true;
        BasicAttack(playerTrack.currentIndex);
        yield return new WaitForSeconds(secondsBetweenAttackGaps);
        BasicAttack(playerTrack.currentIndex);
        yield return new WaitForSeconds(secondsBetweenAttackGaps);
        BasicAttack(playerTrack.currentIndex);
        isAttacking = false;
    }

    private void AttackOdds()
    {
        for (int i = 0; i < 5; i += 2)
        {
            BasicAttack(i);
        }
    }

    private void AttackEvens()
    {
        for (int i = 1; i < 5; i += 2)
        {
            BasicAttack(i);
        }
    }

    private void PerformRandomAttack()
    {
        int result = UnityEngine.Random.Range(0, 100);
        if (IsWithinRange(result, weights.attackAllButOneDiceRange))
        {
            AttackAllButOne();
        }
        else if (IsWithinRange(result, weights.attackThreeRange))
        {
            AttackThree();
        }
        else if (IsWithinRange(result, weights.attackOddThenEvenRange))
        {
            StartCoroutine(AttackOddThenEven());
        }
        else if (IsWithinRange(result, weights.attackEvenThenOddRange))
        {
            StartCoroutine(AttackEvensThenOdds());
        }
        else if (IsWithinRange(result, weights.attackFollowingPlayerRange))
        {
            StartCoroutine(AttackFollowingPlayer());
        }
        else
        {
            throw new ArgumentException($"No attack range supports a result of {result}");
        }

    }

    private bool IsWithinRange(int result, int[] range)
    {
        return result >= range[0] && result < range[1];
    }
}
