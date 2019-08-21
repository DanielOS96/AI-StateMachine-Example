using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;

    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    } 
    #endregion

    [Header("PlayerManager Variables")]
    public float playerStartHealth = 10;
    public bool godMode;
    public GameObject player;
    public HealthBarVisualisation heathBarVisualizer;

    [Header("Sound Variables")]
    public AudioSource audioSource;
    public AudioClip deathNoise;
    public AudioClip deathTune;
    public AudioClip hitNoise;

    [Header("Unity Events")]
    public UnityEvent onPlayerHit;
    public UnityEvent onPlayerDeath;


    public float CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }


    private void Start()
    {
        CurrentHealth = playerStartHealth;
        player = gameObject;
    }

    public void ChangeHealth(float ammount)
    {
        CurrentHealth += ammount;
        if (heathBarVisualizer != null) heathBarVisualizer.UpdateHealthBar(CurrentHealth, playerStartHealth);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//Reload Current scene
    }






    public void PlayerHit(float damage)
    {
        if (IsDead) return;

        if(!godMode)ChangeHealth(-damage);

        if (CurrentHealth <= 0)
        {
            PlayerDeath();
            return;
        }

        audioSource.PlayOneShot(hitNoise);
        onPlayerHit.Invoke();
        Debug.Log("Player is hit. Damage: "+damage);
    }



    public void PlayerDeath()
    {
        if (IsDead) return;
        IsDead = true;

        Debug.Log("Player is dead");

        onPlayerDeath.Invoke();

        StartCoroutine(DeathDelay());
    }
    private IEnumerator DeathDelay()
    {
        //fade screen

        //Disable movement.

        //Disable menu.

        audioSource.PlayOneShot(deathNoise);
        yield return new WaitForSeconds(deathNoise.length);

        audioSource.PlayOneShot(deathTune);
        yield return new WaitForSeconds(deathTune.length+0.5f);

        ReloadScene();
    }


    
}
