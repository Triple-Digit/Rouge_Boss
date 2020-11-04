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
    int waypointIndex;
    public bool halfHealth; 
    bool goingbackwardsthroughWaypoints;
    Rigidbody2D body;
    private Animator animator;
    

    private void Awake()
    {
        animator = transform.Find("Boss_Sprite").GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        halfHealth = false;
        goingbackwardsthroughWaypoints = false;
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
            if(halfHealth && !actions[currentAction].halfHealthAction)
            {
                actionCounter = 0;                
            }
            else
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
        if (actions[currentAction].shouldMove || actions[currentAction].rotate)
        {
            actions[currentAction].shootingPointHolder.Rotate(0, 0, actions[currentAction].rotationSpeed * 100 * Time.deltaTime);

            moveDirection = Vector2.zero;

            if (actions[currentAction].shouldChase)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
                moveDirection.Normalize();
            }
            if (actions[currentAction].moveToPoint)
            {
                moveDirection = actions[currentAction].pointsToMoveTo[waypointIndex].position - transform.position;
                if (Vector2.Distance(transform.position, actions[currentAction].pointsToMoveTo[waypointIndex].position) < 0.1f)
                {
                    if (waypointIndex < actions[currentAction].pointsToMoveTo.Length - 1 && !goingbackwardsthroughWaypoints)
                    {
                        waypointIndex++;
                    }
                    else
                    {
                        waypointIndex--;
                        goingbackwardsthroughWaypoints = true;

                        if (waypointIndex <= 0)
                        {
                            goingbackwardsthroughWaypoints = false;
                        }
                    }
                }
            }
            body.velocity = moveDirection * actions[currentAction].moveSpeed;
        }
        else
            return;
      
    }

    void Shoot()
    {

        if (actions[currentAction].shouldShoot)
        {
            if(actions[currentAction].rotate)
            {
                actions[currentAction].shootingPointHolder.Rotate(0, 0, actions[currentAction].rotationSpeed * 100 * Time.deltaTime);
            }
            

            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = actions[currentAction].fireRate;
                foreach (Transform transform in actions[currentAction].shootingPoints)
                {                    
                    GameObject bullet = Instantiate(actions[currentAction].bulletPrefab, transform.position, transform.rotation);
                    bullet.GetComponent<BulletPhysics>().speed = actions[currentAction].bulletSpeed;
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
    [Tooltip("If true, set either 'move to point' or 'should chase' to true")]
    public bool shouldMove;
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
    public float fireRate, bulletSpeed;
    [Tooltip("This is the point of rotation for the shooting points")]
    public Transform shootingPointHolder;
    public Transform[] shootingPoints;
}