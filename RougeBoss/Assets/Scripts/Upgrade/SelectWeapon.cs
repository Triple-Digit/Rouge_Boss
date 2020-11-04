using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour
{
    public Guns guns;
    public Image sprite;
    public int WeaponRequirement;


    
    public void SelectGun()
    {
        GameManager.instance.playerWeaponIndex = guns.weaponID;
    }

    

    private void Start()
    {
        int unlockCounter = GameManager.instance.weaponUnlock;
        bool WeaponUnlocked = unlockCounter >= WeaponRequirement;
        GetComponent<Button>().interactable = WeaponUnlocked;
        sprite.sprite = guns.sprite;
    }
}
