using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPhysics : MonoBehaviour
{
    Rigidbody2D body;

    public float damage = 1f;
    public float speed = 10f;
    public float bulletDuration = 3f;



    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(bulletDuration);
        Destroy(gameObject);
    }

    private void Update()
    {
        body.velocity = transform.right * speed;
    }


    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if(collision.gameObject.tag == ("Player") || collision.gameObject.tag == ("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }

    
}
