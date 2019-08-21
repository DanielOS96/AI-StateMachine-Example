using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Attack")]
public class AttackDecision : Decision
{
    public override bool Decide(StateController controller){

        bool canAttackEnemy = IsTargetInAttackRange(controller);
        return canAttackEnemy;
    }

    private bool IsTargetInAttackRange(StateController controller){

        //Find player position by tag.
        Transform playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        //Find distance from enemy eyes to player position.
        float distToTarget = Vector3.Distance(controller.eyes.position, playerPos.position);

        //If player is in attack range return true.
        if (distToTarget<= controller.aiStats.attackRange) return true;

        else return false;

    }
}
