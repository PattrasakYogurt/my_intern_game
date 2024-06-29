using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAi : MonoBehaviour
{
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer; 
    public NavMeshAgent agent;   
    private Animator animator;
    [SerializeField] private PlayerController playerController;   

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
        animator = GetComponent<Animator>();
        //playerController = GetComponent<PlayerController>();
        playerController = player.GetComponent<PlayerController>();
    }
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if(!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
            animator.SetTrigger("Walk");
        }
        if(playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            animator.SetTrigger("Run");
        }
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
            animator.SetTrigger("Attack");
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
            PlayerController playerCon = player.GetComponent<PlayerController>();
            if(playerCon != null)
            {
                playerCon.TakeDamage(attackDamage);
            }           
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
