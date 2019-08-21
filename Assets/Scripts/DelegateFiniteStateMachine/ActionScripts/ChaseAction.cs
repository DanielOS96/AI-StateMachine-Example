using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action
{

    //Called when current state is first enterd.
    public override void FirstCall(StateController controller)
    {
        controller.navMeshAgent.isStopped = false;
    }
    //Called when the current state is exited.
    public override void LastCall(StateController controller)
    {
        //Reset move speed on state exit.
        controller.navMeshAgent.speed = controller.aiStats.moveSpeed;
    }
    //Called each frame the state is active.
    public override void Act(StateController controller)
    {
        Chase(controller);
    }



    private void Chase(StateController controller){

        //Set NevMeshAgent destination to chase target position.
        controller.navMeshAgent.destination = controller.chaseTarget.position;
        controller.navMeshAgent.speed = (controller.aiStats.moveSpeed/2)+controller.aiStats.moveSpeed;

    }

}
