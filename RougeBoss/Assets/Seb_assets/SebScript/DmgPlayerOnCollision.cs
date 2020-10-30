using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgPlayerOnCollision : MonoBehaviour
{
    public float damage = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == ("Player") || collision.gameObject.tag == ("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
