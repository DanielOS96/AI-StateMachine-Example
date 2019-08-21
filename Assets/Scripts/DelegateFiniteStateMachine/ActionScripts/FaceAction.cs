using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Face")]
public class FaceAction : Action
{
    //Called when current state is first enterd.
    public override void FirstCall(StateController controller){}
    //Called when the current state is exited.
    public override void LastCall(StateController controller){}
    //Called each frame the state is active.
    public override void Act(StateController controller)
    {
        Face(controller);
    }


    private void Face(StateController controller){

        if (controller.chaseTarget==null)
            return;
        
        

        Vector3 direction = (controller.chaseTarget.position - controller.transform.position).normalized;			
		Quaternion lookRotation = Quaternion.LookRotation (new Vector3 (direction.x, 0f, direction.z));	
		controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, lookRotation, Time.deltaTime*controller.aiStats.rotationSpeed);	
        
    }



}
