using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/RangedWeaponStats")]
public class RangedWeaponStats : ScriptableObject
{

    [Tooltip("The ammount of ammunition in one magazine clip.")]
    [Range(1,1000)]
    public int ammoInMag = 6;

    [Tooltip("Distance in meters weapons projectile can be fired.")]
    [Range(1,1000)]
    public float range = 20;

    [Tooltip("Frequency in seconds at which weapon can launch projectile.")]
    [Range(0.01f,10)]
    public float fireRate = 0.1f;
    
    [Tooltip("The damage points to be inflicted with weapon.")]
    public float damage = 1;

    [Tooltip("The force to be applied to object on collision.")]
    public float hitForce = 5;

    [Tooltip("The speed the projectile will travel at")]
    public float projectileSpeed = 10;



}
