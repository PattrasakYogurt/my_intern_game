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
    public AudioSource chaseSound;
    public AudioSource chaseMusic;    
    [SerializeField] private PlayerController playerController;   

    [Header("walk")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("Attack")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public int attackDamage = 20;
    public SlashEffect slashEffect;
    private Camera playerCamera;

    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private bool isChasing;
    
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();    
    }
    void Start()
    {
        animator = GetComponent<Animator>();       
        playerController = player.GetComponent<PlayerController>();
        isChasing = false;
        playerCamera = player.GetComponentInChildren<Camera>();
    }
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if(!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
            animator.SetTrigger("Walk");
            StopChaseMusic();       
        }
        else if(playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            animator.SetTrigger("Run");
            StartChaseMusic();
        }
        else if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
            animator.SetTrigger("Attack");
            StartChaseMusic();
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
    /*
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
        if (agent.pathStatus == NavMeshPathStatus.PathPartial || agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            walkPointSet = false;
        }
    }
    */
    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f || agent.pathStatus != NavMeshPathStatus.PathComplete)
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
            // Play slash effect in front of the player's camera
            if (slashEffect != null && playerCamera != null)
            {
                Vector3 slashPosition = playerCamera.transform.position + playerCamera.transform.forward * 2f; // Adjust the distance as needed
                Quaternion slashRotation = playerCamera.transform.rotation;
                slashEffect.PlaySlashEffect(slashPosition, slashRotation);
            }
            PlayerController playerCon = player.GetComponent<PlayerController>();
            if(playerCon != null)
            {
                playerCon.TakeDamage(attackDamage);
            }           
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            walkPointSet = false;
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
    private void StartChaseMusic()
    {
        if (!isChasing)
        {
            isChasing = true;
            if (!chaseMusic.isPlaying)
            {
                chaseMusic.Play();
            }
            if (GameManager.instance.main_Music.isPlaying)
            {
                GameManager.instance.main_Music.Stop();
            }
            if (!chaseSound.isPlaying)
            {
                chaseSound.Play();
            }
        }
    }

    private void StopChaseMusic()
    {
        if (isChasing)
        {
            isChasing = false;
            if (chaseMusic.isPlaying)
            {
                chaseMusic.Stop();
            }
            if (!GameManager.instance.main_Music.isPlaying)
            {
                GameManager.instance.main_Music.Play();
            }
            if (chaseSound.isPlaying)
            {
                chaseSound.Stop();
            }
        }
    }
}
