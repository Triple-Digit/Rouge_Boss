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
    [SerializeField] GameObject levelExit;
    [SerializeField] GameObject[] easyBossPrefabs;
    [SerializeField] GameObject[] mediumBossPrefabs;
    [SerializeField] GameObject[] hardBossPrefabs;
    [SerializeField] GameObject[] arenas;

    
    public int difficultyFactor = 0;
    public int playerWeaponIndex = 0;
    public int weaponUnlock = 0;


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
<<<<<<< Updated upstream
    {             
        
        if(difficulty < 2)
=======
    {
        PlayerController.instance.playerHealth.invincible = false;

        if (difficulty <= 3 )
>>>>>>> Stashed changes
        {
            int randomBossInt = Random.Range(0, easyBossPrefabs.Length - 1);
            Instantiate(easyBossPrefabs[randomBossInt]);
        }

        if (difficulty > 2)
        {
            int randomBossInt = Random.Range(0, mediumBossPrefabs.Length - 1);
            Instantiate(mediumBossPrefabs[randomBossInt]);
        }

        if (difficulty > 6)
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
        LoadLevel();
    }

    public void LevelFail()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
