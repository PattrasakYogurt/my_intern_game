using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private int maxHealth = 100;
    bool isGrounded;
    private bool isRun = false;
    private bool isCouch = false;
    private CharacterController characterController;
    private Vector3 velocity;
    [SerializeField] private int currentHealth;
    [SerializeField] private Healthbar healthbar;
    public float walkSpeed = 12f;
    public InteractionUI interactionUI;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private Transform orieantation;
    [SerializeField] private Transform cameraPos;
    [SerializeField] private LayerMask raycastLayer;
    [SerializeField] private float raycastDistance = 6f;
    [SerializeField] private string currentHit;

    //private Rigidbody rigidBody;
    [SerializeField] private LayerMask groundMask;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar = GetComponent<Healthbar>();
        characterController = GetComponent<CharacterController>();
        //rigidBody = GetComponent<Rigidbody>();
        healthbar.SetHealth(maxHealth);
    }
    void Update()
    {
        PlayerMove();
        PlayerRayCast();
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        healthbar.SetHealth(currentHealth);
    }
    public void SetSpeed(float newSpeed)
    {
        walkSpeed = newSpeed;
    }
    private void PlayerMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direction = orieantation.right * h + orieantation.forward * v;
        characterController.Move(direction* walkSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);          
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }       
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRun = true;
            isCouch = false;
            walkSpeed += 10f;
            Debug.Log("Run");
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRun = false;
            isCouch = false;
            walkSpeed -= 10f;
            Debug.Log("StopRun");
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCouch = true;
            isRun = false;
            walkSpeed -= 5f;
            Debug.Log("Couch");
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCouch = false;
            isRun = false;
            walkSpeed += 5f;
            Debug.Log("StopCouch");
        }
        
    }
    public void PlayerRayCast()
    {
        if(Physics.Raycast(cameraPos.position, cameraPos.forward, out RaycastHit hit, raycastDistance, raycastLayer))
        {
            if(hit.collider.tag == "IInteractable") 
            {
                var interactable = hit.collider.GetComponent<IInteractable>();
                interactionUI.Show(interactable.GetInteractionText());
                if(Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
            else
            {
                interactionUI.Hide();
            }
            currentHit = hit.collider.name;
            Debug.DrawRay(cameraPos.position, cameraPos.forward * raycastDistance, Color.green);
        }
        else
        {
            interactionUI.Hide();
            Debug.DrawRay(cameraPos.position, cameraPos.forward * raycastDistance, Color.red);
        }
    }
    
}
