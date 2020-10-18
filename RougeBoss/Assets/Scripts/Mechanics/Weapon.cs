using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Singleton
    public static Weapon instance;
    private void Awake()
    {
        instance = this;
        EquipGun(activeGun);
        canShoot = true;
    }
    #endregion    

    [Tooltip("Choose a number from 0 to the total number of weapon types in the list below to set as the active weapon")]
    public int activeGun;

    #region Active Gun Variables
    SpriteRenderer activeGunSprite;
    GameObject activeBulletPrefab;
    float activeFireRate,activeBulletSpeed;
    int activeMaxAmmo, currentAmmo, activeBurstAmount;
    float activeSpreadAngle;
    [SerializeField] Transform shootingPoint;
    List<Quaternion> bulletSpread;
    bool canShoot;
    #endregion
    
    [Header("List of weapon types")]
    [Tooltip("Increase the size by number of guns you want to add")]
    [SerializeField] WeaponType[] guns;
    
    private void Start()
    {        
        
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    public void EquipGun(int weaponIndex)
    {
        weaponIndex = activeGun;
        //activeGunSprite.sprite = guns[activeGun].gunSprite;        
        activeBulletPrefab = guns[activeGun].bulletPrefab;
        activeFireRate = guns[activeGun].fireRate;
        activeBulletSpeed = guns[activeGun].bulletSpeed;
        activeMaxAmmo = guns[activeGun].maxAmmo;
        activeBurstAmount = guns[activeGun].burstAmount;
        activeSpreadAngle = guns[activeGun].spreadAngle;
        currentAmmo = activeMaxAmmo;

        bulletSpread = new List<Quaternion>(activeBurstAmount);
        for(int i = 0; i < activeBurstAmount; ++i)
        {
            bulletSpread.Add(Quaternion.Euler(Vector3.zero));
        }
    }
      

    void Shoot()
    {
        if (!canShoot) return;
        int i = 0;
        foreach(Quaternion quaternion in bulletSpread)
        {
            bulletSpread[i] = Random.rotation;
            GameObject bullet = Instantiate(activeBulletPrefab, shootingPoint.position, shootingPoint.rotation);
            bullet.transform.rotation = Quaternion.RotateTowards(bullet.transform.rotation, bulletSpread[i], activeSpreadAngle);
            ++i;
        }

        StartCoroutine(CanShoot());
    }

    IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(activeFireRate);
        canShoot = true;
    }


    


}
#region Weapon type basic variables
[System.Serializable]
public class WeaponType
{
    public string nameOfGun;
    //public Sprite gunSprite;
    public GameObject bulletPrefab;
    public float bulletSpeed, fireRate;
    public int maxAmmo, burstAmount;
    public float spreadAngle;
}
#endregion