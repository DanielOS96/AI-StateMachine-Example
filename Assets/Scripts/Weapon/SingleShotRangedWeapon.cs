using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotRangedWeapon : RangedWeapon
{

    private void Update()
    {
        //---------Debug------------------------
        if (Input.GetKeyDown("b")) CallShot();
        if (Input.GetKeyUp("r")) Reload();
        //--------------------------------------
    }


    public void CallShot()
    {
        Shoot();
    }
}