using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;


[CreateAssetMenu(menuName = "PluggableAI/Decisions/ExitJumpAttack")]
public class ExitJumpAttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {

        bool landed = CanExitJumpAttack(controller);
        return landed;
    }


    private bool CanExitJumpAttack(StateController controller)
    {

        //Set rigedbody and NavMeshAgent properties.
        //controller.aiManager.aiRigidBody.drag = 0;
        controller.aiManager.aiRigidBody.isKinematic = false;
        controller.aiManager.aiRigidBody.freezeRotation = true;
        controller.navMeshAgent.enabled = false;


        //If after initial velocity added the AI is falling.
        if (controller.CheckIfCountDownElapsed(0.5f) && controller.aiManager.aiRigidBody.velocity.y <= 0)
        {
            //Debug.Log("AI: " + controller.gameObject.name + " falling");
            controller.aiManager.aiRigidBody.velocity += Vector3.up * Physics.gravity.y * (10.5f - 1) * Time.deltaTime;
        }




        //If timer has elapsed and the AI is on the ground.
        if (controller.CheckIfCountDownElapsed(2.5f) && controller.aiManager.CheckGroundedOnNavmesh())
        {
            //Reset rigedbody and NavMeshAgent properties.
            //controller.aiManager.aiRigidBody.drag = 0;
            controller.aiManager.aiRigidBody.isKinematic = true;
            controller.aiManager.aiRigidBody.freezeRotation = false;
            controller.navMeshAgent.enabled = true;


            return true;

        }

        //Should the AI get stuck more than 'x' seconds in this state kill them.
        //Stuck can happen when AIs jump lands on non navmesh area.
        if (controller.CheckIfCountDownElapsed(10f))
        {
            Debug.Log("AI: " + controller.gameObject.name + " in jump state too long. Calling AI Death");
            //controller.aiManager.Death();
        }


        return false;

    }

  
}
