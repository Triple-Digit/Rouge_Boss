using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattle : MonoBehaviour
{
    public void Battle()
    {
        GameManager.Instance.ArenaLoad();
        GameManager.Instance.difficultyFactor = 1;
    }
}
