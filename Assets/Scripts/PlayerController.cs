using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] private float movementSpeed;

    [SerializeField] private Animator animator;
    [SerializeField] private float runSpeed =7.5f;
    [SerializeField] private float walkSpeed = 5f;

    [SerializeField]  private bool running;
    [SerializeField] private bool idle;
    
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = walkSpeed;
        movement = Vector2.zero;
    }

    private void Awake()
    {
        gameManager = GetComponentInParent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
        HandleAnimations();
    }

    // Fixed upadte happening only on certain frames
    private void FixedUpdate()
    {
        HandleMovement();
    }

    //method to handle passing information to the animator
    private void HandleAnimations()
    {
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);

        if (movement.x != 0)
        {
            animator.SetFloat("LastFacingX", movement.x);
            animator.SetFloat("LastFacingY", 0f);
        }

        if (movement.y !=0)
        {
            animator.SetFloat("LastFacingY", movement.y);
            animator.SetFloat("LastFacingX", 0f);
        }

        animator.SetBool("Idle", idle);
        animator.SetBool("Running", running);
    }

    //method that handles the movement of the player
    private void HandleMovement()
    {
        Vector2 normMovement = movement;
        normMovement.Normalize();
        rb.MovePosition((Vector2)transform.position + (normMovement * movementSpeed * Time.fixedDeltaTime));
    }

    //method that handles logic around player inputs
    private void HandleInputs()
    {

        if (gameManager.gameState != GameManager.GameState.Paused)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        if ((movement.x < 1f && movement.x > -1f) && (movement.y < 1f && movement.y > -1f))
        {
            idle = true;
        }
        else
        {
            idle = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            running = true;
            movementSpeed = runSpeed;
        }
        else
        {
            running = false;
            movementSpeed = walkSpeed;
        }
    }
}
