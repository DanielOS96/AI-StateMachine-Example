﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPos;
    public bool destroyObjAfterTime;
    public float timeBeforeDestroy;
    public Vector3 positionOffset;

    GameObject spawnedObj;

    public void Spawn()
    {
        spawnPos = spawnPos = null ?? transform;

        spawnedObj = Instantiate(prefab, spawnPos.position+positionOffset, spawnPos.rotation);

        if (destroyObjAfterTime)
        {
            Destroy(spawnedObj, timeBeforeDestroy);
        }
    }
}
