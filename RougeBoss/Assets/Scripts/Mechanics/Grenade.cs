using UnityEngine;

public class Grenade : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController.instance.hasItem = true;
    }
}
