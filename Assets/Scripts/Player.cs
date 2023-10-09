using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Player : MonoBehaviour
{
    public float jumpHeight, speed;
    private float horizontal;
    //public float maxSpeed, acceleration, deceleration;
    public float health, maxHealth, manaCharge;
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    //enum face {none, left, right}; //Enum to get camera facing direction.   changed to bool.
    public int maxMana;
    public List<ManaCell> manaCells = new List<ManaCell>();
    private bool faceRight = true;
    private bool grounded;
    [SerializeField] private GameManager gameManager;
    private Animator anim;
    
    //Updated movement using the tutorial below.
    //https://www.youtube.com/watch?v=K1xZ-rycYY8

    // Start is called before the first frame update
    void Awake()
    {
        //frame = 0;
        Debug.Log("Player Start");
        //jumpHeight = 15f;
        //speed = 7.5f;
        //maxSpeed = 10f;
        //acceleration = 15f; 
        //deceleration = 1.5f;
        //faceRight = true;
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //health = 100f;
        //maxHealth = 100f;
        maxMana = 3;
        for (int i = 0; i < maxMana; i++) {
            //ManaCell newCell = Instantiate(new ManaCell());
            manaCells.Add(gameObject.AddComponent<ManaCell>());
            manaCells[i].index = i;
        }
        
        //keep player object upright. 
        //rbody.constraints = RigidbodyConstraints2D.FreezeRotation; //Enabled in object properties.
        //rigidbody.transform.localRotation = localRotation(0, 0, 0);
    }



    // Update is called once per frame
    void Update() 
    {
        
    
    
        //moved to UIManager Debug.
        /* if (frame % 60 == 0) {
            Debug.Log("Player X Velocity: " + rbody.velocityX);
        } */
        //transform.position.x = transform.position.x + speed*Time.deltaTime;


        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")) && isGrounded()) {
            //
            Debug.Log("Jump input");
            //rbody.AddForceY(jumpHeight);
            rbody.velocity = new Vector2(rbody.velocity.x, jumpHeight + Math.Abs(rbody.velocity.x * 0.75f));
            
        }


        //increases jump height longer the key's held. 
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonUp("Jump")) && rbody.velocity.y > 0f)
        {
            rbody.velocity = new Vector2(rbody.velocity.x, rbody.velocity.y * 0.25f);
        }

        

        //Not sure if this will be used. 
        //I'm very tempted to make a keybinds menu.
        if (Input.GetKey(KeyCode.W)) {

        }

        //Maybe crouch input?
        if (Input.GetKey(KeyCode.S)) {
            
        }



        //changed out for movement script from class.
        /* //Move left input.
        if (Input.GetKey(KeyCode.A)) {
            //set face left
            playerFace = face.left;
            GetComponent<SpriteRenderer>().flipX = true;
            //speed += acceleration * Time.deltaTime;
            //rbody.velocity = Vector2.left * speed;
            
            
        }
        
        //Move right input.
        else if (Input.GetKey(KeyCode.D)) {
            playerFace = face.right;
            GetComponent<SpriteRenderer>().flipX = false;
            //speed += acceleration * Time.deltaTime;
            //rbody.velocity = Vector2.right * speed;
            
            
        }  */

        /* else {
            if (speed > deceleration * Time.deltaTime) {
                speed -= deceleration * Time.deltaTime;
            }
            else if (speed < -deceleration * Time.deltaTime) {
                speed -= deceleration * Time.deltaTime;
            }
            else {
                speed = 0;
                speed = Mathf.Clamp(speed, 0 ,maxSpeed);
            }
            //Debug.Log(speed);
        } */
        
        //rbody.velocityX = speed;

        
        

        //Debug test keys. 
        if (gameManager.debugEnabled) {

            if (Input.GetAxisRaw ("Enable Debug Button 1") > 0) {
                Debug.Log("Debug Toggle");
                gameManager.UIManager.toggleDebug();
            }

            if (Input.GetKeyDown(KeyCode.Keypad1)) {
                takeDamage(10);
            }

            if (Input.GetKeyDown(KeyCode.Keypad2)) {
                takeDamage(50);
            }

            if (Input.GetKeyDown(KeyCode.Keypad3)) {
                healFull();
            }

            if (Input.GetKeyDown(KeyCode.Keypad4)) {
                if (manaAvailable()) {
                    manaCost();
                }
                
            }

            if (Input.GetKeyDown(KeyCode.Keypad5)) {
                manaHeal(1);
            }

        }

        
        

        //flip();
        
        
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Run", Mathf.Abs(horizontalInput));
        anim.SetBool("isRunning", horizontalInput != 0);
        anim.SetFloat("JumpLoop", rbody.velocity.y);
        anim.SetBool("Grounded", isGrounded());
        

        rbody.velocity = new Vector2(horizontalInput * speed, rbody.velocity.y);
        
        if ((horizontalInput > 0 && !faceRight) || (horizontalInput < 0 && faceRight)) {
            flip();
        }

        if (Input.GetButtonDown("Jump") && isGrounded()) {
            //
            Debug.Log("Jump input");
            jump();
            //rbody.AddForceY(jumpHeight);
            //rbody.velocity = new Vector2(rbody.velocity.x, jumpHeight + Math.Abs(rbody.velocity.x * 0.75f));
            
        }


        
        //old
        //rbody.velocity = new Vector2(horizontal * speed, rbody.velocity.y);
    }

    public bool getFace() {
        /* if (playerFace == face.right) {
            return 1;
        }
        else if (playerFace == face.left) {
            return -1;
        } */
        //else 
        return faceRight;
    }

    //Make player lose speed on wall contact
    /* void onCollisionEnter2D(Collision2D collision) {
        if (playerFace == face.right) { 
            rbody.velocityX = -1; 
        }
        else if (playerFace == face.left) {
            rbody.velocityX = 1;
        }
    } */

    /* public Rigidbody2D getRigidbody2D() {
        return rbody;
    } */

    private void jump() {
        rbody.velocity = new Vector2(rbody.velocity.x, jumpHeight);
        anim.SetTrigger("Jump");
        
        grounded = false;
    }
    public void takeDamage(float damage) {
        anim.SetTrigger("Hurt");
        gameManager.UIManager.healthAdjust = health;
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void heal(float healing) {
        anim.SetTrigger("Hurt");
        health += healing;
        health = Mathf.Clamp(health, 0, maxHealth);
    }
    public void healFull() {
        health = maxHealth;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public bool manaAvailable() {
        
        for (int i = 0; i <= manaCells.Count; i++) {
            if (manaCells[i].ready == true) {
                return true;
                
            }
        }
        return false;
    }

    public void manaCost() {
        for (int i = manaCells.Count; i >=0; i--) {
            if (manaCells[i].ready == true) {
                manaCells[i].ready = false;
                manaCells[i].cast();
                break;
            }
        }
        /* 
        mana -= spend;
        mana = Mathf.Clamp(mana, 0, maxMana); */
    }

    public void manaHeal(int restore) {
        for (int i = 0; i < manaCells.Count;i++) {
            manaCells[i].ready = true;
        }
    }
    /* 
    public void manaRecharge() {
        for (int i = 0; i < manaCells.Count; i++) {
            if (manaCells[i] == false) {
                manaCells[i] = true;
                break;
            }
        }
        
        /* mana += 1;
        mana = Mathf.Clamp(mana, 0, maxMana); * /
    } */



    private bool isGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //From movement tutorial, found better functionality through use of sprite.flipX

    //returned and updated from lab class. 

    /* private void flip() {
        
        if ( playerFace == face.right && horizontal > 0f || playerFace == face.left && horizontal < 0f) {
            
            /* if (playerFace == face.left) {
                playerFace = face.right;
            }
            else playerFace = face.left; 

            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    } */

    private void flip() {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        faceRight = !faceRight;
    }

}
