using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/AIStats")]
public class AIStats : ScriptableObject
{
    [Header("Move Stats")]
    public float moveSpeed =1;
    public float rotationSpeed =15;
    public float lookRange =40;
    public float lookAngle =95;
    public float stoppingDistance = 3;

    [Header("Attack Stats")]
    public float attackRange =1;
    public float attackRate =1;
    public float attackForce =15;
    public float attackDamage =50;

    [Header("Heatlh Stats")]
    public int aiHealth = 10;

    [Header("Other Stats")]
    public float searchDuration =4;
    public float searchingTurnSpeed =120;


    
}
