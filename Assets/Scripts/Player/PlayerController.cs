using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int maxHealth = 100;
    bool isGrounded = true;
    private bool isRun = false;
    private bool isCouch = false;
    private bool isKeyObtained = false;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private int currentHealth;
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float gravity = 2f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask layerMask;
    void Start()
    {
        currentHealth = maxHealth;
        healthbar = GetComponent<Healthbar>();
        characterController = GetComponent<CharacterController>();
        healthbar.SetHealth(maxHealth);
    }
    void Update()
    {
        PlayerMove();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }
    private void PlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(h, 0, v);
        transform.position = transform.position + direction * walkSpeed * Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.LeftShift))
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
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
