using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    #region PauzeMenu
    public static bool GameIsPaused;

    public GameObject PauzeMenuObject;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        PauzeMenuObject.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }
    public void Pause()
    {
        PauzeMenuObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Continue()
    {
        Resume();
    }
    public void QuitCurrentGame()
    {
        SceneManager.LoadScene("MainMenu_scene");
        Time.timeScale = 1f;
        PauzeMenuObject.SetActive(false);
    }
    #endregion
}

