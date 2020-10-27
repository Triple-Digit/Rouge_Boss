using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPhysics : MonoBehaviour
{
    Rigidbody2D body;
    [SerializeField] int damage = 1;
    public float speed = 10;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void Update()
    {
        body.velocity = transform.right * speed;
    }


    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }

    
}
