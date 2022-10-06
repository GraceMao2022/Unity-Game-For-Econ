using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; ////

/* PlayerMovement.cs
   
   This is the code for player movement: moving left and right, jumping, flipping the player, dying

   Animations for moving are connected to the code here

*/

public class CoinMovement : MonoBehaviour
{
    //// General: if there is no modifier before a field or method, it is default private. 
    //// By making a field public, we will be able to edit what is stored in it through the Unity editor.
    //// [SerializeField] makes private variables visible in Unity editor

    private Rigidbody2D rb; // the Rigidbody2D component of the player 

    private float horDir; // -1 means move left, 1 means move right, 0 means stop
    private float vertDir; // 1 means move up, 0 means stop
    public float moveSpeed = 5.59f; // horizontal speed of player. ////We can edit this value later in the Unity editor.

    ////add these after you have showed movement left and right on Unity
    [SerializeField] private float jumpForce = 500f; // jump force of player
                            ////you could also type public float jumpForce
    [SerializeField] private float m_DoubleJumpForce = 100f;
    private bool jump; // true if player is on the ground and about to jump, false otherwise
    private bool isGrounded; // true if player is touching the ground
    public Transform GroundCheck; // The position of a GameObject that will mark the player's feet
    public LayerMask groundLayer; // determines which layers count as the ground

    private bool facingRight = true; // true if player is facing right

    private bool isDead = false; ////Day 3 // true if player is dead

    //public Animator animator; ////Day 3 // the animator of the player
    public GameObject player; ////Day 3 // the player
    public GameObject lowestBound; ////Day 3 // an empty GameObject that marks the lowest point in your game

    private bool double_jump;
    private bool can_double_jump;
    public bool unlockDoubleJump;
    public bool unlockSpeedBoost;
    public float speedBoostMult = 1.3f;

    //ladder
    public float distance;
    public LayerMask whatIsLadder;
    private bool isClimbing;
    public float climbSpeed = 300f;
    private float inputVertical;

    public float origGravScale = 2.4f;

    public float materialVelChange = 1f;

    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            GlobalManager.unlockDoubleJump = false; 
            GlobalManager.unlockSpeedBoost = false;
        }

        //unlockDoubleJump = GlobalManager.unlockDoubleJump;
        //unlockSpeedBoost = GlobalManager.unlockSpeedBoost;
    }

    // intialize variables
    void Awake()
    {
        // find the Rigidbody2D component of the object that this script is attached to
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame. We use this to detect user input
    void Update()
    {
        // horizontal movement detection (a & d or left & right arrows): -1 is left, 1 is right
        horDir = Input.GetAxisRaw("Horizontal");
        vertDir = Input.GetAxisRaw("Vertical");
        
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);

        if (hitInfo.collider != null)
        {
            if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
            {
                isClimbing = true;
            }
        }
        else
            isClimbing = false;

        if (isClimbing)
        {
            inputVertical = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, inputVertical * climbSpeed);
            rb.gravityScale = 0;
        }
        else
            rb.gravityScale = origGravScale;

        if(!isClimbing)
        {
            // jump detection (w or up arrow): 1 is jump, must be grounded to be able to jump
            if (vertDir == 1 && isGrounded) 
            {
                jump = true;
                
            }
            else
            {
                jump = false;
            }
            //double jump detection
            if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && can_double_jump && GlobalManager.unlockDoubleJump)
            {
                double_jump = true;
                can_double_jump = false;
            }
        }

        ////day3
        // check if player is below the lowest bound. If it is, then kill the player.
        if (player.transform.position.y <= lowestBound.transform.position.y) ////day3
        { ////day3
            Die();
        } ////day3
    }

    // similar to Update(), but is better for physics and movement
    private void FixedUpdate()
    {
        // checks if you are within 0.05 position in the Y of the ground layer
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.05f, groundLayer);

        if(!isDead) ////day 3
            Move(); ////NOT Day 3!!!!!!!! DAY 2222222222
    }

    // deals with the the velocity of the player, and calls Flip() and Jump() when applicable
    private void Move()
    {
        // changes horizontal velocity of player
        
        ////Time.deltaTime makes the speed more constant between different computers with different frames per second
        if(GlobalManager.unlockSpeedBoost)
        {
            // Move the character left or right by finding the target velocity
            Vector3 targetVelocity = new Vector2(horDir * moveSpeed * materialVelChange * speedBoostMult, rb.velocity.y);
            // And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            //rb.velocity = new Vector2(horDir * moveSpeed * Time.deltaTime * materialVelChange * speedBoostMult, rb.velocity.y);
        }
        else
        {
            // Move the character left or right by finding the target velocity
            Vector3 targetVelocity = new Vector2(horDir * moveSpeed * materialVelChange, rb.velocity.y);
            // And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            //rb.velocity = new Vector2(horDir * moveSpeed * Time.deltaTime * materialVelChange, rb.velocity.y);
        }

        // jump if needed
        if (jump && isGrounded)
        {
            Jump();
            can_double_jump = true;
        }
        else if (double_jump && !isGrounded)
        {
            DoubleJump();
            double_jump = false;
        }
    }

    // add a vertical force to the player
    private void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce));
    }

    private void DoubleJump()
    {
        rb.velocity = Vector2.up * m_DoubleJumpForce;
    }

    ////Day 3
    // detects if the player has collided with an enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
        else if(other.gameObject.CompareTag("Moving Platform"))
        {
            player.transform.parent = other.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Moving Platform"))
        {
            player.transform.parent = null;
        }
    }

    ////Day 3
    // stop the player midair when it dies
    public void Stasis()
    {
        rb.velocity = Vector2.zero; // set player's velocity to zero
        rb.gravityScale = 0;
    }

    ////Day 3
    // the player dies --> reload scene, reset score
    public void Die()
    {
        // reset score
        ScoreManager.instance.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //specifically for Level 3 utils
        if(SceneManager.GetActiveScene().buildIndex == 3)
            UtilManager.instance.ResetUtil();
    }
}
