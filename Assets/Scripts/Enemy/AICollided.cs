using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will:
/// <para>Check for collisions to collider on the gameobject.</para>
/// <para>If something with the tag of 'Damager' collides, AI is hit.</para>
/// <para>If the collider has a rigidbody the velocity will be passed into the hit.</para>
/// </summary>
public class AICollided : MonoBehaviour
{
    public AIManager aiManager;                 //Referance to the AIManager script.
    public bool destroyCollindingEntityOnHit;   //Wether or not to destroy what has hit this collider.
    public string ignoreCollisionsWithThisTag;  //If object with this tag enters this collider it will be ignored.
    [Range(1,100)]
    public int damage=1;                        //The damage taken when something of tag damager hits this collider.


    private void Awake()
    {
        aiManager = aiManager == null ? aiManager.GetComponent<AIManager>() : aiManager;
    }







    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ignoreCollisionsWithThisTag)
        {
            //Debug.Log("Igoning collisions on: " + other.gameObject.name);
            Physics.IgnoreCollision(other, GetComponent<Collider>());
        }




        if (other.gameObject.tag == "Damager")
        {
            Vector3 velocity = other.attachedRigidbody == null ? Vector3.zero : other.attachedRigidbody.velocity;

            aiManager.IsHit(damage, velocity);

            if (destroyCollindingEntityOnHit) Destroy(other.gameObject);
            //Debug.Log("Hit by damager trigger ."+velocity);

        }
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ignoreCollisionsWithThisTag)
        {
            //Debug.Log("Igoning collisions on: " + collision.gameObject.name);
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }



        if (collision.gameObject.tag == "Damager")
        {
            Vector3 velocity = collision.relativeVelocity == null ? Vector3.zero : collision.relativeVelocity;

            aiManager.IsHit(damage, velocity);

            if (destroyCollindingEntityOnHit) Destroy(collision.gameObject);

            //Debug.Log("Hit by damager collider ."+velocity);

        }
        
    }


    private void HitByRay()
    {
        Debug.Log("Hit by ray!");
    }
    
}
