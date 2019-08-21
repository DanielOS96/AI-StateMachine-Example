using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/PatrolAction")]
public class PatrolAction : Action
{
    //Called when current state is first enterd.
    public override void FirstCall(StateController controller)
    {
        controller.navMeshAgent.isStopped = false;
    }
    //Called when the current state is exited.
    public override void LastCall(StateController controller){}
    //Called each frame the state is active.
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }



    private void Patrol(StateController controller)
    {
        //Return if no waypoints.
        if (controller.wayPointList == null) return;
        if (controller.wayPointList.Count <= 0) return;

        //Set destination to next waypoint.
        controller.navMeshAgent.destination = controller.wayPointList[controller.nextWayPoint].position;
        

        Debug.DrawLine(controller.transform.position, controller.navMeshAgent.destination, Color.gray);


        //If arrived at destination.
        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance + 0.5f && !controller.navMeshAgent.pathPending)
        {

            //The modulo operator '%' makes sure list is not exceeded and return to beginning.
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
        }
    }

}


  
