using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance = null;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        bosshealthBar.maxValue = bosshealth;
    }
    #endregion

    #region Player and Boss Health Stats
    [SerializeField] private int playerHealth;
    [SerializeField] private float bosshealth;
    [SerializeField] private Slider bosshealthBar;
    [SerializeField] Image[] healthIcon;
    [SerializeField] Sprite fullHeart, emptyHeart;

    
    
    public void SetBossHealth(float healthValue)
    {
        bosshealthBar.value = healthValue;
        bosshealth = healthValue;
    }

    public void SetPlayerHealth(int healthValue)
    {
        playerHealth = healthValue;

        for (int i = 0; i < healthIcon.Length; i++)
        {
            if(i < playerHealth)
            {
                healthIcon[i].sprite = fullHeart;
            }
            else
            {
                healthIcon[i].sprite = emptyHeart;
            }
        }
    }
    #endregion

}
