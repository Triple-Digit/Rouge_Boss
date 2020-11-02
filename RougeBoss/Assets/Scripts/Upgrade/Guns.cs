using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu]
public class Guns : ScriptableObject
{
    public string nameOfGun;
    //public Sprite gunSprite;
    public GameObject bulletPrefab;
    public float bulletSpeed, fireRate, damage;
    public int maxAmmo, burstAmount;
    public float spreadAngle;
    public int weaponID;
    public Sprite sprite;

}
