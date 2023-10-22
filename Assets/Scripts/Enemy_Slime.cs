using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Enemy_Slime : MonoBehaviour
{
    [Header("Movement and detection")]
    [SerializeField] private float jumpDistance;
    [SerializeField] private float jumpDelay;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float stuckDistance;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] private float delayTimer;
    private float prevPos;
    private bool direction;
    private Animator anim;
    private Rigidbody2D rbody;


    [Header("Player Interaction")]
    [SerializeField] private float detectRadius;
    [SerializeField] private GameManager gameManager;
    private PlayerDetect playerDetect;


    [Header("Enemy Parameters")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float damage;
    private float health;
    
    

    // Start is called before the first frame update
    void Awake()
    {
        health = maxHealth;
        direction = true;
        playerDetect = GetComponentInChildren<PlayerDetect>();
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /* if (playerClose()) {
            
        } */
        //Debug.Log(isGrounded());

        if (delayTimer <= 0) {

            if (!hasMoved()) {

                direction = !direction;
            }
            calcJump();
        }

        else if (isGrounded()) {

            
            
            anim.SetBool("Jumping", false);
            delayTimer -= Time.deltaTime;
        }

        
    }


    private void calcJump() {

        if (isGrounded()) {
            
            
            //using playerClose will allow the slime to jump towards the player if it's close, 
            //Otherwise it will just continue jumping forward. 
            if (playerClose()) {
                
                // if () { 
                //Debug.Log("PlayerCloseJump");
                jump(gameManager.player.transform.position.x < transform.position.x);
                
                /*  }

                else {
                    Debug.Log("PlayerCloseJumpLeft");
                    jump(false);
                } */
            }

            /*     
            else if (!hasMoved()) {

                jump(false);
            } */

            //Non-aggro jump jump
            else {

                //Debug.Log("Idle Jump");
                jump(direction);
            }

            
            delayTimer = jumpDelay;
            anim.SetBool("Jumping", true);
            
        } 
    }

    private void jump(bool direction) {
        //Debug.Log(hasMoved());
        prevPos = rbody.transform.position.x; 
        //Debug.Log("previousPos:" + prevPos)
        //using playerClose will allow the slime to jump towards the player if it's close, 
        //Otherwise it will just continue jumping forward. 
        if (direction) {

            //Debug.Log("JumpLeft");
            rbody.AddRelativeForceX(-jumpDistance);
        }
            
        else {

            //Debug.Log("JumpRight");
            rbody.AddRelativeForceX(jumpDistance);
        }

        rbody.AddRelativeForceY(jumpHeight);

    } 



    //not used sprite always face same direction, but moves left or right conditionally. 
    /*
    private void flip() {

        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        faceRight = !faceRight;
    } */

    private void takeDamage(float damage) {

        health -= damage;
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if (other.collider.tag == "Player") {

            gameManager.player.takeDamage(damage);
        }

    }

    private bool playerClose() {
        
        return Physics2D.OverlapCircle(transform.position, detectRadius, whatIsPlayer);
    }

    private bool isGrounded() {
        //Debug.Log(Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround));
        //bool isGrounded = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        //Debug.Log(isGrounded); 
        
        return Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckDistance, whatIsGround);
    }


    //Calculate if the owner has moved reasonable distance in it's jump, used to reduce possibilities of getting stuck in a corner. 
    private bool hasMoved() {
        Debug.Log("prevPos: " + prevPos + "CurrentPos: " + rbody.transform.position.x);
        Debug.Log("HasMoved: " + Mathf.Abs(prevPos - rbody.transform.position.x));
        return Mathf.Abs(prevPos - rbody.transform.position.x ) > stuckDistance;

        /* if (prevPos - 0.5f < rbody.transform.position.x && rbody.transform.position.x < prevPos + 0.5f) {

            return false;
        }
        else return true; */
    }
    
    
}
