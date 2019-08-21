using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;
/// <summary>
/// Using 3 rays and an overlapping sphere each frame is very performant. 
/// Look into using a vision cone for enemy sight.
/// </summary>
[CreateAssetMenu (menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller){

        bool targetVisible = CheckViewRadiusForPlayer(controller);
        return targetVisible;
    }

    private bool CheckViewRadiusForPlayer(StateController controller)
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(controller.eyes.position, controller.aiStats.lookRange);

        //Check each collider in the view radius.
        foreach (Collider coll in targetsInViewRadius)
        {
            if (coll.CompareTag("Player"))
            {
                Transform target = coll.transform;
                //Get direction from enemy eyes to target.
                Vector3 dirToTarget = (target.position - controller.eyes.position).normalized;

                //If target is in view angle.
                if (Vector3.Angle(controller.eyes.forward, dirToTarget) < controller.aiStats.lookAngle / 2)
                {
                    controller.chaseTarget = target;
                    return true;
                    /*
                    float distToTarget = Vector3.Distance(controller.eyes.position, target.position);
                    RaycastHit hit;

                    //If a sphercast hits the player without intersecting with something.
                    if (Physics.SphereCast(controller.eyes.position, 3, dirToTarget,out hit, distToTarget) && hit.collider.CompareTag("Player"))
                    {
                        //Debug.Log(hit.collider.gameObject.name);
                        //Debug.DrawRay(controller.eyes.position, dirToTarget, Color.red);

                        controller.chaseTarget = target;
                        return true;

                    }
                    else return false;
                    */

                }
            }
        }
        return false;
    }




    //Redundednt Code.
    private bool RaycastLook(StateController controller){
        RaycastHit hit;


        Vector3 pos1 = controller.eyes.position;



        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.aiStats.lookRange, Color.green);
  

        //If the player enters the sphere, gets too close to the enemy.
        Collider[] hitColliders = Physics.OverlapSphere(controller.eyes.position, controller.aiStats.lookAngle);
        foreach (Collider coll in hitColliders){
            if (coll.CompareTag("Player")){
                controller.chaseTarget = coll.transform;
                return true;
            }
        }

        //If object tagged player collides with the spherecast the chasetarged is set to said object and return true as something has been found.
        if (Physics.SphereCast(controller.eyes.position, controller.aiStats.lookAngle, controller.eyes.forward, out hit, controller.aiStats.lookRange)&& hit.collider.CompareTag("Player")){
            controller.chaseTarget = hit.transform;
            return true;
        }




        else return false;
    }
    

}
