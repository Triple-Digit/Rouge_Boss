using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] float maxBossHealth, currentBossHealth;
    [SerializeField] int maxPlayerHealth, currentPlayerHealth;
    [SerializeField] bool invincible;
    [SerializeField] float invincibilityDuration;
    [SerializeField] int dropFactor;


    private void Awake()
    {
        if(isPlayer)
        {
            currentPlayerHealth = maxPlayerHealth;
            UIManager.instance.SetPlayerHealth(currentPlayerHealth);
        }
        else
        {
            currentBossHealth = maxBossHealth;
            UIManager.instance.SetBossHealth(currentBossHealth);
        }
                
    }

    public void TakeDamage(int damageAmount)
    {
        if(!isPlayer)
        {
            currentBossHealth = currentBossHealth - damageAmount;
            UIManager.instance.SetBossHealth(currentBossHealth);
            if (currentBossHealth <= 0)
            {
                Dead();
            }
        }
        else
        {
            if (!invincible)
            {
                --currentPlayerHealth;
                UIManager.instance.SetPlayerHealth(currentPlayerHealth);
                invincible = true;
                if (currentPlayerHealth <= 0)
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
    }

    
    IEnumerator TurnInvincibilityOff(float time)
    {
        yield return new WaitForSeconds(time);
        invincible = false;
    }

    public void Heal()
    {
        if (currentPlayerHealth < maxPlayerHealth)
        {
            ++currentPlayerHealth;
        }
    }

    public void Dead()
    {
        if(!isPlayer)
        {

        }
    }
}
