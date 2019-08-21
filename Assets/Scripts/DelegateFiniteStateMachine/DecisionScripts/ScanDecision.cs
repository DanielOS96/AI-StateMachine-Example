using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Scan")]
public class ScanDecision : Decision
{
    public override bool Decide(StateController controller){

        bool noEnemyInSight = Scan(controller);
        return noEnemyInSight;
    }

    private bool Scan(StateController controller){

        controller.navMeshAgent.isStopped = true;

        controller.transform.Rotate(0, controller.aiStats.searchingTurnSpeed * Time.deltaTime, 0);

        return controller.CheckIfCountDownElapsed(controller.aiStats.searchDuration);
    }
}
