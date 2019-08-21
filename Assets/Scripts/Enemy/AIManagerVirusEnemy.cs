using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * Bugs: 
 * AllignToSlope() can casue enemies y rotation to get stuck so they are facing the wrong direction.
 * death during jump.
 * stuff in hit is still beaing called when dead.
 */
public class AIManagerVirusEnemy : AIManager
{

    [Space(20)][Header("--Virus Enemy Variables--")][Space(35)]
    
    public float deathDelayTime = 1;           //The time until the death delay method is called.
    public UnityEvent onDeathDelay;            //Event called on the death delay.

    [Header("Sound Variables")]
    public AudioClip enemyWalkSound;           //Sounds used for enemy walking.
    public AudioClip enemyRunSound;            //Sounds used for enemy running.
    public List<AudioClip> enemyAtkSounds;     //Sounds used for enemy attacking.
    public List<AudioClip> enemyJumpAtkSounds; //Sounds used for enemy jump attack.
    public List<AudioClip> enemyHitSounds;     //Sounds used for enemy hit.
    public List<AudioClip> enemyDeathSounds;   //Sounds used for enemy death.
    


    private Vector3 lastPosition = Vector3.zero;        //Used in calculating enemy speed.
    private Vector3 movementDirection = Vector3.zero;   //The current movement direction.
    private float speedPerSec;                          //The speed per second the enemy is moving.

    private Vector3 posCur;      //The position aligned to the surface normal.
    private Quaternion rotCur;   //The rotation alligned to the surface normal.



    public  void Start() {
        controller.SetupAI(true,waypointList);
    }

    private void Update(){
        //---------Debug----------------
        if (Input.GetKeyDown("o"))
            IsHit(1, Vector3.up);
        if (Input.GetKeyDown("p"))
            IsHit(100, Vector3.zero);
        //-----------------------------

        if (!IsDead){

            //Update speed & move dir.
            CalculateSpeedAndMoveDirection();

            //Set the speed in the animator (between 0.0 - 1.0).
            aiAnimator.SetFloat("Speed",Mathf.Round(speedPerSec*100)/100);

            //Allign to slope. Using NavmeshAgent child transform to not interfear with NavMeshAgent.
            //AllignToSlope(aiAnimator.transform);

            //Mix between the walk and run audio.
            HandleWalkRunSoundFX();
        }

    }








    public override bool Attack(float force, string type){
        if (!base.Attack(force, type))return false;


        switch (type)
        {
            case "Jump":
                aiAnimator.SetTrigger("JumpAttack");
                PlayAudioFX(enemyJumpAtkSounds[Random.Range(0, enemyJumpAtkSounds.Count - 1)]);
                StartCoroutine(PhysicsJumpAttack());

                break;
            case "Normal":
                aiAnimator.SetInteger("RandomAttack", Random.Range(1, 3));
                aiAnimator.SetTrigger("Attack");
                PlayAudioFX(enemyAtkSounds[Random.Range(0, enemyAtkSounds.Count - 1)]);

                break;
            default:
                Debug.LogWarning("Not a valid attack type");
                break;
        }

        return true;
    }

    #region Hit Methods
    public override bool IsHit(int damage, Vector3 hitDirection){
        if (!base.IsHit(damage, hitDirection)) return false;

        aiAnimator.StopPlayback();
        aiAnimator.SetTrigger("Hit");
        
        PlayAudioFX(enemyHitSounds[Random.Range(0, enemyHitSounds.Count - 1)]);

        aiRigidBody.freezeRotation = true;
        aiRigidBody.isKinematic = false;

        aiRigidBody.AddForce(hitDirection/2, ForceMode.VelocityChange);


        IEnumerator hitDelay = HitDelay();
        StopCoroutine(hitDelay);
        StartCoroutine(hitDelay);

        return true;
    }
    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(1);

        
        //Wait until grounded.
        while (!CheckGroundedOnNavmesh())
        {
            yield return null;
        }

        aiRigidBody.isKinematic = true;
        aiRigidBody.freezeRotation = false;


    }
    #endregion


    #region Death Methods
    public override bool Death(){
        if (!base.Death())return false;

        controller.SetupAI(false);

        aiAnimator.SetTrigger("Death");
        PlayAudioFX(enemyDeathSounds[Random.Range(0, enemyAtkSounds.Count - 1)]);

        aiRigidBody.freezeRotation = true;
        aiRigidBody.isKinematic = false;
        aiRigidBody.AddForce(-movementDirection * 5, ForceMode.VelocityChange);


        StartCoroutine(DeathDelay());

        return true;
    }
    private IEnumerator DeathDelay(){
        yield return new WaitForSeconds(deathDelayTime);
        onDeathDelay.Invoke();
                
        Destroy(gameObject);
    }
    #endregion



    //Apply a physics thrust in the jump direction twords player.
    private IEnumerator PhysicsJumpAttack()
    {
        //Get the position to aim jump to.
        Transform playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        //Aim for slightly above players head.
        Vector3 playerPosModified = new Vector3(playerPos.position.x, playerPos.position.y + 0.5f, playerPos.position.z);
        //Get the direction between the enemy and the player.
        Vector3 playerDir = (playerPosModified -transform.position).normalized;

        //Come to a stop before jump.
        aiRigidBody.velocity = Vector3.zero;

        //Small pause before jump begins.
        yield return new WaitForSeconds(0.5f);

        //Do the jump.
        aiRigidBody.AddForce(playerDir * 20, ForceMode.VelocityChange);


    }
    //Calculate the speed & move direction of enemy.
    private void CalculateSpeedAndMoveDirection()
    {
        speedPerSec = Vector3.Distance(lastPosition, transform.position) / Time.deltaTime;

        movementDirection = (transform.position - lastPosition).normalized;

        lastPosition = transform.position;
    }
    //Mix between walk and run audio.
    private void HandleWalkRunSoundFX()
    {
        //If audiosource playing something other than walk or run sound wait for it to finish.
        if (aiAudioSource.clip != enemyWalkSound && aiAudioSource.clip != enemyRunSound)
        {
            //Stop it looping.
            aiAudioSource.loop = false;
            //When its not playing return.
            if (aiAudioSource.isPlaying)
            {
                return;
            }
        }

        //Dont play walk and run sound if enemy is not grounded or not moving.
        if (!CheckGroundedOnNavmesh() || speedPerSec<0.1)
        {
            if (aiAudioSource.clip == enemyWalkSound || aiAudioSource.clip == enemyRunSound)
            {
                aiAudioSource.Stop();
                aiAudioSource.loop=false;
                //Debug.Log("Stop Audio");
            }
        }
        else if (speedPerSec > 3.8)
        {
            PlayAudioFX(enemyRunSound, true, 0.2f);
            //Debug.Log("Audio Run");
        }
        else if (speedPerSec <= 3.8)
        {
            PlayAudioFX(enemyWalkSound, true, 0.2f);
            //Debug.Log("Audio Walk");
        }

    }
    //Play an audio clip.
    private void PlayAudioFX(AudioClip clipToPlay, bool loop = false, float volume =1)
    {
        if (aiAudioSource == null)
        {
            Debug.Log("Audio Source is missing!");
        }

        else if (clipToPlay != null)
        {
            //If audiosource already playing given clip then return.
            if (aiAudioSource.clip == clipToPlay && aiAudioSource.isPlaying) return;

            aiAudioSource.clip = clipToPlay;
            aiAudioSource.loop = loop;
            aiAudioSource.volume = volume;
            aiAudioSource.Play();
        }
    }
    //Align enemy to the surface normals it is on. (Sometimes rotation can get screwd up and is not facing the player)

    

}
