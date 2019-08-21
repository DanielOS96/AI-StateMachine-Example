using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public RangedWeaponStats weaponStats;

    public Transform shootTransform;
    public GameObject prejectilePrefab;

    [Header("Sound Variables")]
    public AudioClip shootSound;
    public AudioClip emptySound;
    public AudioSource audioSource;

    [Header("Setting Variables")]

    public bool physicsFireMode;

    [SerializeField]
    internal int shotsRemaining;        //The ammount of shots left in the magazine.



    internal virtual void Start()
    {
        Reload();
    }




    public  void Reload()
    {
        shotsRemaining = weaponStats.ammoInMag;
        Debug.Log("'"+gameObject.name + "' reloaded.");
    }

    internal void OutOfAmmo()
    {
        Debug.Log("'" + gameObject.name + "' out of ammo.");
        audioSource.PlayOneShot(emptySound);
    }




    internal void Shoot()
    {
        if (shotsRemaining > 0)
        {
            if (physicsFireMode) ObjectShoot();
            else RayShoot();

            shotsRemaining--;
        }
        else
        {
            OutOfAmmo();
        }
       
    }

    private void ObjectShoot()
    {
        GameObject obj = Instantiate(prejectilePrefab, shootTransform.position, transform.rotation);
        obj.GetComponent<Rigidbody>().AddForceAtPosition(shootTransform.forward * weaponStats.projectileSpeed, shootTransform.forward, ForceMode.Impulse);
        Debug.DrawRay(shootTransform.position, shootTransform.forward, Color.blue);
        Destroy(obj, 2);

        audioSource.PlayOneShot(shootSound);

    }

    private void RayShoot()
    {
        audioSource.clip = shootSound;
        audioSource.Play();

        RaycastHit hit;
        if (Physics.Raycast(shootTransform.position, shootTransform.forward, out hit))
            hit.transform.SendMessage("HitByRay");
    }
}


