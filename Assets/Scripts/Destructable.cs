using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroyedVersion;


    public void Destroy(float lifeTime=3) {
        Destroy(Instantiate(destroyedVersion, transform.position, transform.rotation),lifeTime);
        
        Destroy(gameObject);
    }


    

    
}
