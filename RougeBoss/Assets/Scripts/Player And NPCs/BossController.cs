using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossController : MonoBehaviour
{    
    [Header("List of Actions")]
    [Tooltip("Increase the size by number of actions you want to add")]
    [SerializeField] BossAction[] actions;

    public int currentAction;
    float actionCounter, shotCounter;
    Vector2 moveDirection;
    bool halfHealth;
    Rigidbody2D body;
    

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        SetActionCounter();
    }

    void Update()
    {
        ActionTimer();
    }

    void SetActionCounter()
    {
        actionCounter = actions[currentAction].actionLength;
    }

    void ActionTimer()
    {
        if(actionCounter > 0)
        {
            actionCounter -= Time.deltaTime;
            
            Movement();
            Shoot();
        }
        else
        {
            currentAction++;            
            if(currentAction >= actions.Length)
            {
                currentAction = 0;
            }
            SetActionCounter();
        }
    }

    void Movement()
    {
        moveDirection = Vector2.zero;
        if(actions[currentAction].shouldChase)
        {
            Debug.Log("Moving towards player not fuctioning");
            moveDirection = PlayerController.instance.transform.position - transform.position;
            moveDirection.Normalize();
        }
        if (actions[currentAction].moveToPoint)
        {
            Debug.Log("Moving to a point function not available yet. This is for testing purposes");
            //moveDirection = actions[currentAction].pointToMoveTo.position - transform.position;
        }
        else return;
        body.velocity = moveDirection * actions[currentAction].moveSpeed;
    }

    void Shoot()
    {

        if (actions[currentAction].shouldShoot)
        {
            if(actions[currentAction].rotate)
            {
                actions[currentAction].shootingPointHolder.Rotate(0, 0, actions[currentAction].rotationSpeed * 100 * Time.deltaTime);
            }
            if(actions[currentAction].aimAtPlayer)
            {
                actions[currentAction].shootingPointHolder.LookAt(PlayerController.instance.transform);
            }

            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = actions[currentAction].fireRate;
                foreach (Transform transform in actions[currentAction].shootingPoints)
                {
                    Instantiate(actions[currentAction].bulletPrefab, transform.position, transform.rotation);
                }
            }
        }
        else return;
    }

}
[System.Serializable]
public class BossAction
{    
    public string actionType;
    public float actionLength;

    [Tooltip("Set health action to true if you want the action to only occur when the bosses health is less than or equal to half its max health")]
    public bool halfHealthAction;
    [Tooltip("If true, please create empty game objects, place them at points with in the arena, drag and drop the points transforms to the array 'Points to move to' and set 'Should chase' to false")]
    public bool moveToPoint;
    [Tooltip("If true, set 'move to point' to false")]
    public bool shouldChase;
    public float moveSpeed;
    public Transform[] pointsToMoveTo;

    [Tooltip("If true, fill in the fire rate, shooting point holder and shooting points")]
    public bool shouldShoot;
    [Tooltip("If true, set 'rotate' to false")]
    public bool aimAtPlayer;
    [Tooltip("If true, set 'Aim at player' to false, and set a speed")]
    public bool rotate;
    public float rotationSpeed;
    public GameObject bulletPrefab;
    public float fireRate;
    [Tooltip("This is the point of rotation for the shooting points")]
    public Transform shootingPointHolder;
    public Transform[] shootingPoints;
}