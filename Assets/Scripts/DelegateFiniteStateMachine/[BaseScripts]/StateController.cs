using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This script will:
/// <para>-Handle all functionality of the State Machine.</para>
/// </summary>
[RequireComponent(typeof(NavMeshAgent), typeof(AIManager))]
public class StateController : MonoBehaviour {

    public State currentState;      //Current State the AI is in.
    public AIStats aiStats;         //An object containint stats about the AI.
    public Transform eyes;          //The position the AI will use as its eyes.
    public State remainState;       //A referance to the 'remainState' state.
    public bool showGizmos;         //Wether or not to show the gizmos.


    [HideInInspector] public NavMeshAgent navMeshAgent;     //Referance to the NavMesh agent.
    [HideInInspector] public AIManager aiManager;           //Referance to the AI Manager script.
    [HideInInspector] public List<Transform> wayPointList;  //List of all the waypoints for the AI to patrol.
    [HideInInspector] public int nextWayPoint;              //The next waypoint to move to.
    [HideInInspector] public Transform chaseTarget;         //Target to next move to.
    [HideInInspector] public float stateTimeElapsed;        //Time the state has been active for.

    private bool aiActive;          //Toggled on when AI is initialized.

    private void Awake () 
    {
        //-----------SetUp Referances----------------
        navMeshAgent = gameObject.GetComponent<NavMeshAgent> ();
        aiManager = gameObject.GetComponent<AIManager> ();
        //-------------------------------------------
    }

    /// <summary>
    /// This method will initialize the AI state machine behaviour.
    /// </summary>
    /// <param name="aiActivationFromManager">Toggle AI movement</param>
    /// <param name="wayPointsFromManager">Waypoint list, accepts null</param>
    public void SetupAI(bool aiActivationFromManager, List<Transform> wayPointsFromManager = null)
    {
        wayPointList = wayPointsFromManager;
        aiActive = aiActivationFromManager;
        navMeshAgent.speed = aiStats.moveSpeed;
        TransitionToState(currentState);
        if (aiActive) 
        {
            navMeshAgent.enabled = true;

        } else 
        {
            navMeshAgent.enabled = false;
        }

        
    }

    
    public void TransitionToState(State nextState){
        if (nextState !=remainState){

            stateTimeElapsed = 0;

            currentState.OnStateExit(this);

            currentState = nextState;

            currentState.OnStateEnter(this);
            
        }
    }

    public bool CheckIfCountDownElapsed(float duration){
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed>= duration);
    }



    private void FixedUpdate(){
        if (!aiActive) return;

        currentState.UpdateState(this);
    }

    //Display state information in an editor gizmo.
    private void OnDrawGizmos(){
        if (currentState!=null && eyes!=null && showGizmos){
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, aiStats.lookRange);

            Vector3 viewAngleA = DirFromAngle(-aiStats.lookAngle / 2, false);
            Vector3 viewAngleB = DirFromAngle(aiStats.lookAngle / 2, false);

            Gizmos.DrawLine(transform.position, transform.position + viewAngleA * aiStats.lookRange);
            Gizmos.DrawLine(transform.position, transform.position + viewAngleB * aiStats.lookRange);

        }
    }
    private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        //if angle is not global covert it.
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}