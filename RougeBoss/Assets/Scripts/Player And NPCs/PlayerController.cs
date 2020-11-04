using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    float activeMoveSpeed, dashCounter;
    [SerializeField] float moveSpeed;    
    [SerializeField] Transform aimDirection;
    [SerializeField] float dashSpeed, dashLength, grenadeSpeed;
    [SerializeField] bool hasItem;
    [SerializeField] GameObject grenade;

    Rigidbody2D body;
    Camera gameCamera;
    Vector2 moveInput;
    Health playerHealth;

    private void Awake()
    {
        instance = this;
        gameCamera = Camera.main;
        body = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<Health>();
    }

    void Update()
    {
        Move();
        Aim();
        UseItem();
    }
    
    void Move()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        body.velocity = moveInput * activeMoveSpeed;
    }


    void Aim()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = gameCamera.WorldToScreenPoint(transform.localPosition);
        
        if(mousePosition.x < screenPoint.x) 
        { 
            transform.localScale = new Vector3(-1f, 1f, 1f);
            aimDirection.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            aimDirection.localScale = Vector3.one;
        }

        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        aimDirection.rotation = Quaternion.Euler(0, 0, angle);
    }

    void UseItem()
    {
        if(hasItem)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                playerHealth.invincible = true;
                hasItem = false;
            }
            
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Weapon.instance.ShootGrenade(grenade, grenadeSpeed);
                hasItem = false;
            }

            if(Input.GetKeyDown(KeyCode.E))
            {
                Weapon.instance.EMP();
                hasItem = false;
            }


        }

        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if(dashCounter<=0)
            {
                activeMoveSpeed = moveSpeed;
                playerHealth.invincible = false;
            }
        }


    }

    
}
