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
    
    [SerializeField] Transform shootingPoint;
    List<Quaternion> bulletSpread;
    bool canShoot;
    public float timeToShoot;

    public bool updateGunVaribles;
    #endregion
    
    [Header("List of weapon types")]
    [Tooltip("Increase the size by number of guns you want to add")]
    [SerializeField] WeaponType[] guns;
    
    

    private void Update()
    {
        if(timeToShoot > 0)
        {
            timeToShoot -= Time.deltaTime;
        }
        
        if (Input.GetButton("Fire1"))
        {            
            if(timeToShoot <= 0)
            {                
                Shoot();
                timeToShoot = guns[activeGun].fireRate;
            }            
        }
    }





    public void EquipGun(int weaponIndex)
    {
        weaponIndex = activeGun;

        #region bullet spray code attempt
        
        bulletSpread = new List<Quaternion>(guns[activeGun].burstAmount);
        for(int i = 0; i < guns[activeGun].burstAmount; i++)
        {
            bulletSpread.Add(Quaternion.Euler(Vector3.zero));
        }
        
        #endregion
    }


    void Shoot()
    {
        #region bullet spray code attempt

        for(int i = 0; i < bulletSpread.Count; i++)
        {
            bulletSpread[i] = Random.rotation;
            GameObject bullet = Instantiate(guns[activeGun].bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            bullet.transform.rotation = Quaternion.RotateTowards(bullet.transform.rotation, bulletSpread[i], guns[activeGun].spreadAngle);
            bullet.GetComponent<BulletPhysics>().speed = guns[activeGun].bulletSpeed;
            bullet.GetComponent<BulletPhysics>().damage = guns[activeGun].damage;
        }
        
        #endregion

        //GameObject bullet = Instantiate(guns[activeGun].bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        //bullet.GetComponent<BulletPhysics>().speed = guns[activeGun].bulletSpeed;
        //bullet.GetComponent<BulletPhysics>().damage = guns[activeGun].damage;
    }


}


#region Weapon type basic variables
[System.Serializable]
public class WeaponType
{
    public string nameOfGun;
    //public Sprite gunSprite;
    public GameObject bulletPrefab;
    public float bulletSpeed, fireRate, damage;
    public int maxAmmo, burstAmount;
    public float spreadAngle;
}
#endregion