using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutomaticRangedWeapon : RangedWeapon
{
    public int shotsInBurst = 10;       //Number of shots in a burst.

    private int remainingShotsInBurst;  //Number of shots left in burst.
    private bool semiAutoFireOn;        //Toggle semi automatic fire.

    internal override void Start()
    {
        base.Start();

        if (shotsInBurst > weaponStats.ammoInMag)
        {
            Debug.LogAssertion(gameObject.name+" ammo lower than shots in burst.");
        }
    }
    

    private void Update()
    {
        //---------Debug--------------------------------
        if (Input.GetMouseButtonDown(0)) ToggleBulletStream();
        if (Input.GetMouseButtonUp(0)) ToggleBulletStream();
        if (Input.GetKeyUp("r")) Reload();
        //----------------------------------------------
    }


    //Toggle the bullet stream on off.
    public void ToggleBulletStream()
    {
        //Toggle fire.
        semiAutoFireOn = !semiAutoFireOn;

        //Reset the ammount of shot in burst.
        remainingShotsInBurst = shotsInBurst;

        //Cancel shooting if its already running.
        CancelInvoke("ShootandCount");

        if (semiAutoFireOn)
        {
            //invoke shooting repeating at fire rate.
            InvokeRepeating("ShootandCount", 0, weaponStats.fireRate);

        }
        
    }

    //Call shoot method while counting shots in burst.
    private void ShootandCount()
    {
        if (remainingShotsInBurst > 0)
        {
            Shoot();
            remainingShotsInBurst--;
        }
        else
        {
            CancelInvoke("ShootandCount");
        }
    }
}
