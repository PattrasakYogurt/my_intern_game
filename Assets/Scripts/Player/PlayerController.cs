using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("HealthGroup")]
    public int maxHealth = 100;
    public int currentHealth;
    public Healthbar healthbar;
    [Header("BoolGroup")]
    bool isGrounded;
    private bool isRun = false;
    private bool isCouch = false;
    [Header("Other")]
    private CharacterController characterController;
    public InteractionUI interactionUI;
    private Vector3 velocity;    
    public Slider staminaBar;
    [Header("FloatGroup")]
    [SerializeField] private float runCost;
    [SerializeField] private float chargeRate = 1f;
    public float stamina, maxStamina;
    public float walkSpeed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private float raycastDistance = 6f;
    [Header("TransformGroup")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform orieantation;
    [SerializeField] private Transform cameraPos;
    [SerializeField] private Transform camera_NormalPos;
    [SerializeField] private Transform camera_CouchPos;
    [Header("LayerMaskGroup")]
    [SerializeField] private LayerMask raycastLayer;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private string currentHit;

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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            stamina -= runCost * Time.deltaTime;
            staminaBar.value = stamina;
            if(stamina < 0 )
            {
                stamina = 0;
            }
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            cameraPos.position = new Vector3(transform.position.x, camera_CouchPos.position.y, camera_CouchPos.position.z);
        }
        StartCoroutine(RechargeStamina());
        

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            GameManager.instance.PlayerLoose();
        }
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
            walkSpeed -= 7f;
            Debug.Log("Couch");
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCouch = false;
            isRun = false;
            cameraPos.position = new Vector3(transform.position.x, camera_NormalPos.position.y, camera_NormalPos.position.z);
            walkSpeed += 7f;
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
    IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);
        while(stamina < maxStamina)
        {
            stamina += chargeRate/100f;
            if(stamina > maxStamina) 
            { stamina = maxStamina;}
            staminaBar.value = stamina;
            yield return new WaitForSeconds(1f);
        }
    }
    
}
