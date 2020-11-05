using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public class GunHandlerItem
    {
        public Weapon_ID id;
        public bool isAcquired;
    }
    private List<GunHandlerItem> Weapons = new List<GunHandlerItem>();

    private void Awake()
    {
        foreach(Weapon_ID g in Enum.GetValues(typeof(Weapon_ID)))
        {
            GunHandlerItem newItem = new GunHandlerItem();
            newItem.id = g;
            newItem.isAcquired = false;
            Weapons.Add(newItem);
        }
    }

    public void SetWeaponAcquired(Weapon_ID _id, bool _isAcquired)
    {
        foreach(GunHandlerItem i in Weapons)
        {
            if(i.id == _id)
            {
                i.isAcquired = _isAcquired;
                return;
            }
        }
    }

    public bool IsWeaponAcquired(Weapon_ID _id)
    {
        foreach(GunHandlerItem i in Weapons)
        {
            if(i.id == _id)
            {
                return i.isAcquired;
            }
        }
        return false;
    }
}
public enum Weapon_ID
{

}
