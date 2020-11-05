using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject();
                    singleton.AddComponent<GameManager>();
                    singleton.name = "(Singleton) GameManager";
                }
            }
            return instance;
        }
    }

    public static bool HasInstance()
    {
        return instance != null;
    }

    private void Awake()
    {
        if (HasInstance() && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        startingGame = true;
        HUD = this.gameObject.transform.GetChild(0).gameObject;
        LoadLevel();

    }
    #endregion

    [Header("Level requirements")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject levelExit;
    [SerializeField] GameObject[] easyBossPrefabs;
    [SerializeField] GameObject[] mediumBossPrefabs;
    [SerializeField] GameObject[] hardBossPrefabs;
    public int difficultyFactor = 1;
    public int playerWeaponIndex = 0;
    public int weaponUnlock = 0;

    [Header("Managers")]
    [SerializeField] SoundManager soundManager;
    public UIManager m_UIManager;

    public bool startingGame = true;
    private GameObject HUD;


   
    public void LoadLevel()
    {
        if (!HUD.activeSelf)
        {
            HUD.SetActive(true);
        }

        LoadBossAndArena(difficultyFactor);

        if(startingGame)
        {
            Instantiate(playerPrefab);
            Weapon.instance.activeGun = playerWeaponIndex;
            Weapon.instance.EquipGun();
            startingGame = false;
        }
    }

    void LoadBossAndArena(int difficulty)
    {             
        if (difficulty <= 3 )
        {
            int randomBossInt = Random.Range(0, easyBossPrefabs.Length - 1);
            Instantiate(easyBossPrefabs[randomBossInt]);
        }

        if (difficulty > 3 && difficulty < 6)
        {
            int randomBossInt = Random.Range(0, mediumBossPrefabs.Length - 1);
            Instantiate(mediumBossPrefabs[randomBossInt]);
        }

        if (difficulty >= 6)
        {
            int randomBossInt = Random.Range(0, hardBossPrefabs.Length - 1);
            Instantiate(hardBossPrefabs[randomBossInt]);
        }
    }

    public void ClearLevel()
    {
        PlayerController.instance.playerHealth.invincible = true;
        Instantiate(levelExit);
    }

    public void LevelComplete()
    {
        ++difficultyFactor;
        PlayerController.instance.playerHealth.invincible = false;
        LoadLevel();
    }

    public void LevelFail()
    {
        weaponUnlock += difficultyFactor;
        HUD.SetActive(false);
        SceneManager.LoadScene("Upgrade_scene");
    }
    
    public void ArenaLoad()
    {
        StartCoroutine(WaitForSceneLoad());
    }

    private IEnumerator WaitForSceneLoad()
    {
        startingGame = true;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Seb_scene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        LoadLevel();
    }
}
