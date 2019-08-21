using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIStateMachine;

[CreateAssetMenu (menuName = "PluggableAI/State")]
public class State : ScriptableObject 
{
    
    [TextArea(0,10)]
    public string notes = "Notes about state.";

    public Action[] actions;                    //Array of actions to call when in state.
    public Transition[] transitions;            //Array of transitions to call when in state.
    public Color sceneGizmoColor = Color.grey;  //Color displayed on gizmo when in state.


    
    [Header("On State Enter Effects")]
    public GameObject effectPrefab;         //The prefab containing effects to insantiate.
    public float effectDuration;            //The time before the instantiated prefab is destroyed.
    public Transform effectSpawnPosition;   //The posistion the effect spawn at.
    [Header("On State Enter Sound")]
    public List<AudioClip> audioClips;      //List of any audio clips to play on state enter.



    #region Update/OnEnter/OnExit
    /// <summary>
    /// Called once every frame.
    /// </summary>
    /// <param name="controller">Referacne to instance of controller script.</param>
    public void UpdateState(StateController controller){

        DoActions(controller);
        CheckTransisions(controller);
    }

    //Called when the state is first enterd.
    public void OnStateEnter(StateController controller)
    {
        //Play any audio effects.
        PlayAudioFX(controller);
        //Play any effects
        PlayFX(controller);

        //Call 'FirstCall' method in each action.
        for (int count = 0; count < actions.Length; count++)
        {

            actions[count].FirstCall(controller);
        }
    }
    //Called when state is exited.
    public void OnStateExit(StateController controller)
    {
        //Call 'LastCall' method in each action.
        for (int count = 0; count < actions.Length; count++)
        {
            actions[count].LastCall(controller);
        }
    }
    #endregion



    //Act each action each frame.
    private void DoActions(StateController controller){

        //Call 'Act' method in each action in state.
        for (int count = 0; count < actions.Length; count++){

            actions[count].Act(controller);
        }
    }

    //Check each transition each frame.
    private void CheckTransisions(StateController controller){

        for (int count =0; count < transitions.Length; count++){

            //Check if decision is true or false.
            bool decisionSucceeded = transitions[count].decision.Decide(controller);

            //Transition to either true or false state.
            if (decisionSucceeded){
                controller.TransitionToState(transitions[count].trueState);
            }else{
                controller.TransitionToState(transitions[count].falseState);
            }
        }
    }






    #region Play Effects
    //Play any audio effects when the state is first enterd.
    private void PlayAudioFX(StateController controller)
    {
        if (audioClips.Count <= 0) return;

        AudioSource soundSource = controller.GetComponent<AudioSource>();
        int index = Random.Range(0, audioClips.Count - 1);

        if (soundSource != null)
        {
            soundSource.clip = audioClips[index];
            soundSource.Play();
        }
    }
    //Play any effects when the state is first enterd i.e- particle effects.
    private void PlayFX(StateController controller)
    {
        if (effectPrefab != null)
        {
            if (effectSpawnPosition != null)
            {
                Destroy(Instantiate(effectPrefab, effectSpawnPosition.position, effectSpawnPosition.rotation), effectDuration);
            }
            else
            {
                Destroy(Instantiate(effectPrefab, controller.transform.position, controller.transform.rotation), effectDuration);
            }
        }
    }
    #endregion




}