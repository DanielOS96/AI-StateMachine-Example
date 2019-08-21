using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRangedWeapon : RangedWeapon
{

    private bool autoFireOn;    //Toggle automatic fire.


    private void Update()
    {
        //---------Debug--------------------------------
        if (Input.GetKeyDown("b")) ToggleBulletStream();
        if (Input.GetKeyUp("b")) ToggleBulletStream();
        if (Input.GetKeyUp("r")) Reload();
        //----------------------------------------------
    }



   

    public void ToggleBulletStream()
    {
        autoFireOn = !autoFireOn;

        IEnumerator autoFire = AutomaticShoot();
        StopCoroutine(autoFire);

        if (autoFireOn)
        {
            StartCoroutine(autoFire);

        }
    }



    private IEnumerator AutomaticShoot()
    {
        while (autoFireOn)
        {
            if (shotsRemaining <= 0)
            {
                OutOfAmmo();
                yield break;
            }
            else
            {
                Shoot();
            }

            yield return new WaitForSeconds(weaponStats.fireRate);
        }
    }


}
