using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
        startingGame = true;
        uIManager = UIManager.instance;
        LoadLevel();

    }
    #endregion

    [Header("Level requirements")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject[] easyBossPrefabs;
    [SerializeField] GameObject[] mediumBossPrefabs;
    [SerializeField] GameObject[] hardBossPrefabs;
    [SerializeField] GameObject[] arenas;
    [SerializeField] int difficultyFactor = 1;
    public int playerWeaponIndex = 2;

    [Header("Managers")]
    [SerializeField] UIManager uIManager;
    [SerializeField] SoundManager soundManager;

    bool startingGame = true;

    
    void LoadLevel()
    {
        LoadBossAndArena(difficultyFactor);

        if(startingGame)
        {
            Instantiate(playerPrefab);
            Weapon.instance.EquipGun(playerWeaponIndex);
            startingGame = false;
        }

        



    }

    void LoadBossAndArena(int difficulty)
    {             
        
        if(difficulty == 1 )
        {
            //int randomBossInt = Random.Range(0, easyBossPrefabs.Length - 1);
            Instantiate(easyBossPrefabs[0]);
        }

        if (difficulty == 2)
        {
            //int randomBossInt = Random.Range(0, mediumBossPrefabs.Length - 1);
            Instantiate(mediumBossPrefabs[0]);
        }

        if (difficulty == 3)
        {
            //int randomBossInt = Random.Range(0, hardBossPrefabs.Length - 1);
            Instantiate(hardBossPrefabs[0]);
        }

    }

    void ClearLevel()
    {
        
    }

    public void LevelComplete()
    {
        ++difficultyFactor;
        LoadLevel();
    }

    public void LevelFail()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
