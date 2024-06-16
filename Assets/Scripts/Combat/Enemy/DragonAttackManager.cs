using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttackManager : MonoBehaviour
{
    [SerializeField] float secondsBetweenAttacks;
    [SerializeField] float secondsBetweenAttackGaps;
    [SerializeField] Transform[] attackingPositions;
    [SerializeField] GameObject lightning;
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
            StartCoroutine(AttackInnerToOuter());
            attackWait.EmptyMeter();
        }
    }

    private void BasicAttack(int laneIndex)
    {
        Transform attackPosition = attackingPositions[laneIndex];
        GameObject.Instantiate(lightning, attackPosition);
    }

    private void AttackPlayerPosition()
    {
        BasicAttack(playerTrack.currentIndex);
    }

    private IEnumerator AttackOuterToInner()
    {
        isAttacking = true;
        BasicAttack(0);
        BasicAttack(4);
        yield return new WaitForSeconds(secondsBetweenAttackGaps);
        BasicAttack(1);
        BasicAttack(3);
        yield return new WaitForSeconds(secondsBetweenAttackGaps);
        BasicAttack(2);
        isAttacking = false;
    }

    private IEnumerator AttackInnerToOuter()
    {
        isAttacking = true;
        BasicAttack(2);
        yield return new WaitForSeconds(secondsBetweenAttackGaps);
        BasicAttack(1);
        BasicAttack(3);
        yield return new WaitForSeconds(secondsBetweenAttackGaps);
        BasicAttack(0);
        BasicAttack(4);
        isAttacking = false;
    }

    private IEnumerator AttackAllExceptRightmost()
    {
        isAttacking = true;
        for (int i = 0; i < 4; ++i)
        {
            BasicAttack(i);
            if (i != 3)
            {
                yield return new WaitForSeconds(secondsBetweenAttackGaps);
            }
        }
        isAttacking = false;
    }

    private IEnumerator AttackAllExceptLeftmost()
    {
        isAttacking = true;
        for (int i = 4; i > 0; --i)
        {
            BasicAttack(i);
            if (i != 1)
            {
                yield return new WaitForSeconds(secondsBetweenAttackGaps);
            }
        }
        isAttacking = false;
    }
}