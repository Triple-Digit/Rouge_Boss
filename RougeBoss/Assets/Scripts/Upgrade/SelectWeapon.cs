using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour
{
    public Guns guns;
    public Image sprite; 

    public void SelectGun()
    {
        GameManager.instance.playerWeaponIndex = guns.weaponID;
    }

    private void Start()
    {
        sprite.sprite = guns.sprite;
    }
}
