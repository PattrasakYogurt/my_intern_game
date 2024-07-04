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

    [Header("StaminaGroup")]
    public float stamina = 0f;
    public float maxStamina = 100f;
    public Slider staminaBar;
    [SerializeField] private float runCost;
    [SerializeField] private float chargeRate = 1f;

    [Header("MovementGroup")]
    public float walkSpeed = 5f;
    public float originalWalkSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundDistance = 0.4f;
    bool isGrounded;

    [Header("InteractionGroup")]
    public InteractionUI interactionUI;
    [SerializeField] private float raycastDistance = 6f;
    
    [Header("TransformGroup")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform orieantation;
    [SerializeField] private Transform cameraPos;
    [SerializeField] private Transform camera_NormalPos;
    [SerializeField] private Transform camera_CrouchPos;

    [Header("LayerMaskGroup")]
    [SerializeField] private LayerMask raycastLayer;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private string currentHit;

    private bool isRunning = false;
    private bool isCrouching = false;
    private CharacterController characterController;
    private Vector3 velocity;
    void Start()
    {
        currentHealth = maxHealth;       
        healthbar = GetComponent<Healthbar>();
        characterController = GetComponent<CharacterController>();       
        healthbar.SetHealth(maxHealth);
        stamina = maxStamina;
    }
    void Update()
    {
        PlayerMove();
        PlayerRayCast();      
        HandleStamina();
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
            cameraPos.position = new Vector3(transform.position.x, camera_CrouchPos.position.y, camera_CrouchPos.position.z);
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
            StartRunning();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopRunning();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCrouching();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StopCrouching();
        }

    }
    private void HandleStamina()
    {
        if (isRunning)
        {
            stamina -= runCost * Time.deltaTime;
            staminaBar.value = stamina;
            if (stamina < 0)
            {
                stamina = 0;
                StopRunning();
            }
        }
        else
        {
            StartCoroutine(RechargeStamina());
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
    private void StartRunning()
    {
        if (stamina > 0)
        {
            isRunning = true;
            isCrouching = false;
            walkSpeed = originalWalkSpeed + 5f;
            Debug.Log("Run");
        }
    }

    private void StopRunning()
    {
        isRunning = false;
        walkSpeed = originalWalkSpeed;
        Debug.Log("StopRun");
    }

    private void StartCrouching()
    {
        isCrouching = true;
        isRunning = false;
        walkSpeed = originalWalkSpeed - 2f;
        cameraPos.position = new Vector3(transform.position.x, camera_CrouchPos.position.y, camera_CrouchPos.position.z);
        Debug.Log("Crouch");
    }

    private void StopCrouching()
    {
        isCrouching = false;
        cameraPos.position = new Vector3(transform.position.x, camera_NormalPos.position.y, camera_NormalPos.position.z);
        walkSpeed = originalWalkSpeed;
        Debug.Log("StopCrouch");
    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);
        while (stamina < maxStamina && !isRunning)
        {
            stamina += chargeRate / 100f;
            if (stamina > maxStamina)
            {
                stamina = maxStamina;
            }
            staminaBar.value = stamina;
            yield return new WaitForSeconds(1f);
        }
    }

}
