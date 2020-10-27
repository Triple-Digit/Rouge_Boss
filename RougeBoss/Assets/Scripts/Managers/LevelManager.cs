using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    #region Singleton
    public static LevelManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
            
    public void LoadLevel(int bossHealth, int weaponIndex)
    {
        
    }

    public void EndLevel(bool bossDefeated)
    {
        if(bossDefeated)
        {
            //Open loading screen, wipe scene of assets and spawn new level
        }
        else
        {

        }
    }



    
}
