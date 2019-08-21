using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarVisualisation : MonoBehaviour
{
    public Image healthBarImage;



    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float newHealthValue = (currentHealth / maxHealth)*1f;
        //Debug.Log(newHealthValue);


        healthBarImage.fillAmount = newHealthValue;
    }


}
