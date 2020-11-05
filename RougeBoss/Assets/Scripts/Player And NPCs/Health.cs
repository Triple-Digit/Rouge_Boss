using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer = false;
    [SerializeField] float maxBossHealth, currentBossHealth;
    [SerializeField] int maxPlayerHealth, currentPlayerHealth;    
    [SerializeField] float invincibilityDuration;
    [SerializeField] int dropFactor=1;
    [SerializeField] GameObject item;
    private bool bossDead;
    public bool invincible;

    private void Awake()
    {
        

        if (isPlayer)
        {
            currentPlayerHealth = maxPlayerHealth;
            GameManager.Instance.m_UIManager.SetPlayerHealth(currentPlayerHealth);
        }
        else
        {
            GameManager.Instance.m_UIManager.bosshealthBar.maxValue = maxBossHealth;
            currentBossHealth = maxBossHealth;
            GameManager.Instance.m_UIManager.SetBossHealth(maxBossHealth);
        }
                
    }

    public void TakeDamage(float damageAmount)
    {
        if(!isPlayer)
        {
            currentBossHealth = currentBossHealth - damageAmount;
            GameManager.Instance.m_UIManager.SetBossHealth(currentBossHealth);
            //if(currentBossHealth < 5)
            //{
            //    GetComponent<BossController>().halfHealth = true;
            //}
            if (currentBossHealth <= 0 && !bossDead)
            {
                bossDead = true;

                if (bossDead)
                {
                    Dead();
                }
            }  
        }
        else
        {
            if (!invincible)
            {
                --currentPlayerHealth;
                GameManager.Instance.m_UIManager.SetPlayerHealth(currentPlayerHealth);
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
            GameManager.Instance.LevelFail();
        }
        else if(bossDead)
        {
            Destroy(gameObject);
            int randomInt = Random.Range(0, 5);
            if(randomInt >= 3)
            {
                Instantiate(item,transform.position, transform.rotation);
            }
            
            GameManager.Instance.ClearLevel();         

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
