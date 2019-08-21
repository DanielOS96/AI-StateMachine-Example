using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public bool allowDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (!allowDamage) return;

        if (other.gameObject.tag == "Player")
        {

            Debug.Log("Player is hit by collider");

        }
    }
}
