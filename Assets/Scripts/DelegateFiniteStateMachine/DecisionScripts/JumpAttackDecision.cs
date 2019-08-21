using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/JumpAttack")]
public class JumpAttackDecision : Decision
{
    [Range(1,10)]
    public float maxJumpBounds = 7;                 //The maximum meters the AI will preform jump from.
    [Range(0, 9)]
    public float minJumpBounds = 5;                 //The minimum meters the AI will preform jump from.
    [Range(0, 1)]
    public float percentChancePerHalfSecond = 0.01f;    //The percent chance per second that the AI will jump if in range.

    public override bool Decide(StateController controller){

        bool canJumpAttackEnemy = CheckDistance(controller);
        return canJumpAttackEnemy;
    }

    private bool CheckDistance(StateController controller){

        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        float distToPlayer = Vector3.Distance(controller.transform.position, playerPos);

        //If enemy witin range to jump.
        if (distToPlayer < maxJumpBounds && distToPlayer > minJumpBounds)
        {
            //Debug.Log("In jump attack range");
            //Random chance to perform jump.

            if (controller.CheckIfCountDownElapsed(0.5f))
            {
                controller.stateTimeElapsed = 0;
                if (Random.value <= percentChancePerHalfSecond) return true;
                else return false;
            }
            else return false;


        }
        else return false;



    }
}
