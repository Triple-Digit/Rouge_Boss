using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    [SerializeField] int maxHealth, currentHealth;
    [SerializeField] bool invincible;
    [SerializeField] float invincibilityDuration;

    private void Awake()
    {
        instance = this;
        currentHealth = maxHealth; 
    }

    public void TakeDamage()
    {
        if (!invincible)
        {
            --currentHealth;
            invincible = true;
            if(currentHealth <= 0)
            {
                Dead();
            }
            else
            {
                StartCoroutine(TurnInvincibilityOff(invincibilityDuration));
            }            
        }
        else
        {
            return;
        }            
    }

    IEnumerator TurnInvincibilityOff(float time)
    {
        yield return new WaitForSeconds(time);
        invincible = false;
    }

    public void Heal()
    {
        if(currentHealth < maxHealth)
        {
            ++currentHealth;
        }
    }

    public void Dead()
    {

    }

    

}
