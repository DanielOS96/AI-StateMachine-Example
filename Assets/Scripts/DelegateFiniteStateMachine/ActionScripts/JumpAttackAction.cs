using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/JumpAttack")]
public class JumpAttackAction : Action
{
    //Called when current state is first enterd.
    public override void FirstCall(StateController controller)
    {
        //Handle the jump in the enemy manager rather than in this non-instanced object.
        controller.aiManager.Attack(controller.aiStats.attackForce, "Jump");
    }
    //Called when the current state is exited.
    public override void LastCall(StateController controller){}
    //Called each frame the state is active.
    public override void Act(StateController controller){}








}
