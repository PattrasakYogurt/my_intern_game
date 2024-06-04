using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum state
{
    idle = 0,
    walk = 1,
    run = 2,
    attack = 3,    
}
public class EnemyAi : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float detectedDistance = 10f;
    [SerializeField] private float forceDetectedDistance = 4f;
    [SerializeField] private float detectionAngle = 90f;
    [SerializeField] private float huntTime;
    [SerializeField] private float huntTimeCount;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AudioSource detectedSound;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioSource runBGM;
    [SerializeField] private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateState()
    {

    }
}
