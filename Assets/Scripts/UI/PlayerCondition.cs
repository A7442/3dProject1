using System;
using System.Collections;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }
    
    public event Action onTakeDamage;

    private void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);
        
        if(health.curValue < 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public bool ConsumptionStamina(float amount)
    {
        if (stamina.curValue < amount)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }

    public void Die()
    {
        
    }
    
}
