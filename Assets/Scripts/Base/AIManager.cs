using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

/// <summary>
/// The base AI MAnager script. 
/// <para>Inherit and overide this scripts methods to add custom functionality.</para>
/// </summary>
[RequireComponent(typeof(StateController))]
public class AIManager : MonoBehaviour
{
    [Space(10)][Header("AI Components")][Space(10)]
    public Animator aiAnimator;                     //Referance to AI animator component.
    public Rigidbody aiRigidBody;                   //Referance to AI rigidbody.
    public Collider aiCollider;                     //Referance to AI collider.
    public AudioSource aiAudioSource;               //Referance to AI audio source.
    public StateController controller;              //Referance to the instance of the controller script.  

    public List<Transform> waypointList;            //List containing the waypoints.

    [Space(10)][Header("Unity Events")][Space(10)] 
    public UnityEvent onAttack;                     //Event called on AI attack.
    public UnityEvent onHit;                        //Event called on AI hit.
    public UnityEvent onDeath;                      //Event called on AI death.

    
    internal int aiHealth;                          //The AI health value obtained from AIStats object.
    internal bool IsDead{ get; private set; }       //Can be read by other scrips only set in this script.



    internal virtual void Awake(){
        //------SetUp Referances----------------------------------------------------------
        controller = controller==null ? GetComponent<StateController>(): controller;
        aiAnimator = aiAnimator==null ? GetComponent<Animator>(): aiAnimator;
        aiRigidBody = aiRigidBody==null ? GetComponent<Rigidbody>(): aiRigidBody;
        aiCollider = aiCollider==null ? GetComponent<Collider>(): aiCollider;
        aiAudioSource = aiAudioSource==null ? GetComponent<AudioSource>(): aiAudioSource;
        //--------------------------------------------------------------------------------
        aiHealth = controller.aiStats.aiHealth;
    }


    
    
    /// <summary>
    /// Call to invoke an attack from AI.
    /// </summary>
    /// <param name="force">Force value of attack.</param>
    /// <param name="type">Type of attack.</param>
    public virtual bool Attack(float force, string type){
        if (IsDead)return false;

        onAttack.Invoke();

        return true;
    }

    /// <summary>
    /// Call to invoke a hit to AI.
    /// </summary>
    /// <param name="damage">Damage Value of Hit.</param>
    /// <param name="hitDirection">Direction of Hit.</param>
    public virtual bool IsHit(int damage, Vector3 hitDirection){
        if (IsDead) return false;

        aiHealth -=damage;

        if (aiHealth <= 0) {
            Death();
            return false;
        }


        onHit.Invoke();
        return true;
    }


    /// <summary>
    /// Call to invoke AI death.
    /// </summary>
    public virtual bool Death(){
        if (IsDead) return false;

        IsDead=true;

        onDeath.Invoke();

        return true;
    }





    /// <summary>
    /// Return true if close to surface thats is also NavMesh.
    /// </summary>
    /// <param name="checkDistance">Distance to check for ground</param>
    /// <returns></returns>
    public bool CheckGroundedOnNavmesh(float checkDistance =0.5f)
    {
        Ray ray = new Ray(transform.position, -transform.up);
        NavMeshHit hit;

        Debug.DrawRay(transform.position, -transform.up, Color.black);

        
        // Check for nearest point on navmesh to agent, within onMeshThreshold
        if (NavMesh.SamplePosition(transform.position, out hit, checkDistance, NavMesh.AllAreas))
        {
            // Check if the positions are vertically aligned
            if (Mathf.Approximately(transform.position.x, hit.position.x)&& Mathf.Approximately(transform.position.z, hit.position.z))
            {
                // Lastly, check if object is below navmesh
                return transform.position.y <= hit.position.y;
            }
           
        }
        return false;

    }

    /// <summary>
    /// Return true if close to surface.
    /// </summary>
    /// <param name="checkDistance">Distance to check for surface</param>
    /// <returns></returns>
    public bool CheckGroundedRaycast(float checkDistance = 0.5f)
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        Debug.DrawRay(transform.position, -transform.up, Color.black);

        if (Physics.Raycast(ray, out hit, checkDistance) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }












}
