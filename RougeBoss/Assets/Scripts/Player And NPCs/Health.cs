using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer = false;
    [SerializeField] float maxBossHealth, currentBossHealth;
    [SerializeField] int maxPlayerHealth, currentPlayerHealth;
    [SerializeField] bool invincible;
    [SerializeField] float invincibilityDuration;
    [SerializeField] int dropFactor=1;


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

    public void TakeDamage(float damageAmount)
    {
        if(!isPlayer)
        {
            currentBossHealth = currentBossHealth - damageAmount;
            UIManager.instance.SetBossHealth(currentBossHealth);
            if(currentBossHealth < 25)
            {
                GetComponent<BossController>().halfHealth = true;
            }
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
        if(isPlayer)
        {
            GameManager.instance.LevelFail();
        }
        else
        {
            GameManager.instance.LevelComplete();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && isPlayer)
        {
            TakeDamage(1f);
        }
    }
}
