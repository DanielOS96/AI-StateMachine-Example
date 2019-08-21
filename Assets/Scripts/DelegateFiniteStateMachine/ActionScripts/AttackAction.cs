using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{



    //Called when current state is first enterd.
    public override void FirstCall(StateController controller)
    {
        controller.navMeshAgent.isStopped = true;
    }
    //Called when the current state is exited.
    public override void LastCall(StateController controller){}
    //Called each frame the state is active.
    public override void Act(StateController controller)
    {
        //Possible improvement would be to have enemy move back and forward.
        //in response to player movement when in attack state.
        DoAttack(controller);
        //KeepDistanceFromPlayer(controller);
    }




    private void CheckAttack(StateController controller){

        controller.navMeshAgent.isStopped = true;

        Vector3 pos1 = controller.eyes.position;
        Vector3 pos2 = new Vector3(controller.eyes.position.x, controller.eyes.position.y + 2, controller.eyes.position.z);
        Vector3 pos3 = new Vector3(controller.eyes.position.x, controller.eyes.position.y - 2, controller.eyes.position.z);

        RaycastHit hit;
        
        Debug.DrawRay(pos1, controller.eyes.forward.normalized * controller.aiStats.attackRange, Color.red);
        Debug.DrawRay(pos2, controller.eyes.forward.normalized * controller.aiStats.attackRange, Color.red);
        Debug.DrawRay(pos3, controller.eyes.forward.normalized * controller.aiStats.attackRange, Color.red);
        
        if (Physics.SphereCast(pos1, controller.aiStats.lookAngle, controller.eyes.forward, out hit, controller.aiStats.attackRange)&& hit.collider.CompareTag("Player"))
        {
            DoAttack(controller);
        }
        if (Physics.SphereCast(pos2, controller.aiStats.lookAngle, controller.eyes.forward, out hit, controller.aiStats.attackRange) && hit.collider.CompareTag("Player"))
        {
            DoAttack(controller);
        }
        if (Physics.SphereCast(pos3, controller.aiStats.lookAngle, controller.eyes.forward, out hit, controller.aiStats.attackRange) && hit.collider.CompareTag("Player"))
        {
            DoAttack(controller);
        }


        //We check the colliders here becasue if the player is too close to the enemy they can avoid the spherecast
        //This is quite preformant it may be more efficent to do a distance check from the eyes to the player
        //And attack when the player is within a certain bounds.
        Collider[] hitColliders = Physics.OverlapSphere(controller.eyes.position, controller.aiStats.lookAngle);
        foreach (Collider coll in hitColliders)
        {
            if (coll.CompareTag("Player"))
            {
                DoAttack(controller);
            }
        }
    }


    private void DoAttack(StateController controller)
    {
        if (controller.CheckIfCountDownElapsed(controller.aiStats.attackRate))
        {
            controller.aiManager.Attack(controller.aiStats.attackForce, "Normal");
            controller.stateTimeElapsed = 0;
        }
        
    }

    private void KeepDistanceFromPlayer(StateController controller)
    {
        if (controller.chaseTarget == null)
            return;

        //Make sure chase target is players position + distance from player.

        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        //float distToPlayer = Vector3.Distance(controller.transform.position, playerPos);
        Vector3 dirToTarget = (playerPos - controller.eyes.position).normalized;

        Vector3 offsetChasePosition = controller.eyes.position - dirToTarget * 2;

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = offsetChasePosition;
    }


}
