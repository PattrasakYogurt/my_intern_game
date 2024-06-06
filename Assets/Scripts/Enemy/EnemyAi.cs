using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAi : MonoBehaviour
{
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer; 
    public NavMeshAgent agent;
    private Healthbar playerHealthbar;
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerController 
    { get {  return playerController;  }
      set 
        { 
            playerController = GetComponent<PlayerController>(); 
        } 
    }

    [Header("walk")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("Attack")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public int attackDamage = 20;

    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();        
    }
    void Start()
    {
        playerHealthbar = GetComponent<Healthbar>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if(!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        if(playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if(playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            playerController.currentHealth -= attackDamage;
           
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }

    }
    private void Patroling()
    {
        if(!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceWalkPoint = transform.position - walkPoint;
        if(distanceWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    public void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        
        if(!alreadyAttacked)
        {
            alreadyAttacked = true;
            playerController.TakeDamage(attackDamage);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    public void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
