﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattle : MonoBehaviour
{
    public void Battle()
    {
        GameManager.instance.LoadLevel();
        GameManager.instance.difficultyFactor = 1;
        GameManager.instance.weaponUnlock = 0;
    }
}
