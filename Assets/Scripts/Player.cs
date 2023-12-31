using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float jumpHeight, speed;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private int footstepDelay;
    private bool faceRight = true;
    
    //private bool grounded; //using physics overlap method. 
    //private float horizontal; 
    //public float maxSpeed, acceleration, deceleration;
    //[SerializeField] private LayerMask groundLayer; //moved to gameManager
    //enum face {none, left, right}; //Enum to get camera facing direction.   changed to bool.
    
    [Header("Health System")]
    public float health; 
    public float maxHealth; 
    public float ultMaxHealth; 
    private bool isAlive;

    [Header("Combat System")]
    [SerializeField] private Transform attackSpawn;
    [SerializeField] private Transform attackSpawnUp;
    [SerializeField] private float knockBackForce;
    [SerializeField] private float meleeDelay;
    
    private bool actionReady;
    private float actionDelay;
    
    [Header("Magic system")]
    public int maxMana, ultMaxMana;
    public float maxManaCharge;
    [SerializeField] private float spellDelay; 
    public float manaCharge; 
    public float spellCharge; 
    private bool spellCharging;
    public List<ManaCell> manaCells = new List<ManaCell>();

    [Header("Attack Prefabs")]
    [SerializeField] private GameObject meleeAttack;
    [SerializeField] private GameObject meleeAttackUp;
    [SerializeField] private GameObject fireSpell;

    [Header("Game Manager")]
    [SerializeField] private GameManager gameManager;
    private Animator anim;
    private Rigidbody2D rbody;
    

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
        gameManager = FindFirstObjectByType<GameManager>();
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isAlive = true;
        //health = 100f;
        //maxHealth = 100f;
        //maxMana = 3;
        
        for (int i = 0; i < maxMana; i++) {

            //ManaCell newCell = Instantiate(new ManaCell());
            manaCells.Add(gameObject.AddComponent<ManaCell>());
            manaCells[i].index = i;
            Debug.Log("Mana Added - Total Mana:" + manaCells.Count);
        }
        
        //keep player object upright. 
        //rbody.constraints = RigidbodyConstraints2D.FreezeRotation; //Enabled in object properties.
        //rigidbody.transform.localRotation = localRotation(0, 0, 0);
    }



    // Update is called once per frame
    void Update() 
    {
        if (isAlive) {
        
            //moved to UIManager Debug.
            /* if (frame % 60 == 0) {
                Debug.Log("Player X Velocity: " + rbody.velocityX);
            } */
            //transform.position.x = transform.position.x + speed*Time.deltaTime;


            /* if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")) && isGrounded()) {
                
                Debug.Log("Jump input");
                //rbody.AddForceY(jumpHeight);
                rbody.velocity = new Vector2(rbody.velocity.x, jumpHeight + Math.Abs(rbody.velocity.x * 0.75f));
                
            }


            //increases jump height longer the key's held. 
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonUp("Jump")) && rbody.velocity.y > 0f) {

                rbody.velocity = new Vector2(rbody.velocity.x, rbody.velocity.y * 0.25f);
            }
 */
            

            //Not sure if this will be used. 
            //I'm very tempted to make a keybinds menu.
            if (Input.GetKey(KeyCode.W)) {

            }

            //Maybe crouch input?
            if (Input.GetKey(KeyCode.S)) {
                
            }


            if (actionDelay >= 0 ) { 

                spellCharge = 0;
                actionReady = false;
                actionDelay -= Time.deltaTime;
            }

            else if (!spellCharging) {

                actionReady = true;
            }

            //Standard Melee attack
            if (Input.GetButtonDown("Fire1") && actionReady) {

                if (Input.GetKey(KeyCode.W)) 
                    attackUp();

                else 
                    attack();    
            }

            

            //Start charging spell when key pressed and held, shoot when released or after 3 seconds. 
            if (Input.GetButtonDown("Fire2") && actionReady) {

                if (manaAvailable() >= 1) {

                    spellCharging = true;
                    //spellCharge = 0;
                }
            }

            if(spellCharging) {

                spellCharge += Time.deltaTime;
            }

            
            if ((Input.GetButtonUp("Fire2") || spellCharge >= 3) && spellCharge > 0 && actionReady) {

                if (manaAvailable() == 1) {

                    spellCharge = 1;
                }

                CastFireball();
                actionDelay = spellDelay;
                spellCharging = false;
                actionReady = false;
            }


            if (Input.GetKeyDown(KeyCode.Escape) && !gameManager.isPaused) {
                gameManager.PauseGame();
            }

            else if (Input.GetKeyDown(KeyCode.Escape) && gameManager.isPaused) {
                gameManager.ResumeGame();
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
            if (Application.isEditor) {

                if (Input.GetKeyDown(KeyCode.F3)) {

                    Debug.Log("Debug Toggle");
                    gameManager._UIManager.toggleDebug();
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

                    manaCost(1);
                }

                if (Input.GetKeyDown(KeyCode.Keypad5)) {

                    manaCost(3);
                }

                if (Input.GetKeyDown(KeyCode.Keypad6)) {

                    manaHeal(1);
                }

                if (Input.GetKeyDown(KeyCode.Keypad9)) {

                    manaUpgrade();
                }

                if (Input.GetKeyDown(KeyCode.KeypadPlus)) {

                    healthUpgrade(15);
                }

            }

            //flip();
        
        }        
    }

    private void FixedUpdate()
    {
        if (isAlive) {

            float horizontalInput = Input.GetAxisRaw("Horizontal");
            //anim.SetFloat("Run", Mathf.Abs(horizontalInput)); //deprecated to use the boolean below. 
            anim.SetBool("isRunning", horizontalInput != 0);
            anim.SetFloat("JumpLoop", rbody.velocity.y);
            anim.SetBool("Grounded", isGrounded());
            

            rbody.velocity = new Vector2(horizontalInput * speed, rbody.velocity.y);
            

            if (horizontalInput != 0 && isGrounded() && Time.frameCount % footstepDelay == 0) 
                gameManager.audioManager.playFootstepClip();
            if ((horizontalInput > 0 && !faceRight) || (horizontalInput < 0 && faceRight)) {

                flip();
            }

            if (Input.GetButtonDown("Jump") && isGrounded()) {
                //
                Debug.Log("Jump input");
                jump();
                gameManager.audioManager.playJumpClip();
                //rbody.AddForceY(jumpHeight);
                //rbody.velocity = new Vector2(rbody.velocity.x, jumpHeight + Math.Abs(rbody.velocity.x * 0.75f));
                
            }

            //old
            //rbody.velocity = new Vector2(horizontal * speed, rbody.velocity.y);
        }
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
        
        
        //grounded = false;
    }

    public void killPlayer() {
            isAlive = false;
            anim.SetTrigger("Death");
            gameManager.GameOver();
    }

    //Health System Methods.
    public void takeDamage(float damage) {

        anim.SetTrigger("Hurt");
        //gameManager.UIManager.healthAdjust = health;
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health <= 0) {
            killPlayer();
        }
        
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

    public void healthUpgrade(float up) {

        if (maxHealth == ultMaxHealth) {

            healFull();
        }

        else {

            maxHealth += up;
            healFull();
        }
    }


    //Magic System Methods
    public int manaAvailable() {

        int count = 0;

        for (int i = 0; i <= manaCells.Count-1; i++) {

            if (manaCells[i].ready == true) {

                count++;
            }
        }

        Debug.Log("Mana Available: " + count);
        return count;
    }

    public void manaCost(int cost) {

        int spend = cost;

        if (cost <= manaAvailable()) {
            
            for (int i = manaCells.Count - 1; i >= 0; i--) {
            
                if (manaCells[i].ready == true) {

                    Debug.Log("Mana cast");
                    manaCells[i].ready = false;
                    manaCells[i].cast();
                    spend -= 1;
                    
                    if (spend == 0) break;

                }
            }
        }

        //couldn't get blinking to work. Everytime it was run in unity the entire engine would hang - not helpful
        /* else {
            gameManager.UIManager.manaBlink(cost);
        } */
        /* 
        mana -= spend;
        mana = Mathf.Clamp(mana, 0, maxMana); */
    }

    public void manaHeal(int restore) {

        for (int i = 0; i < manaCells.Count; i++) {

            manaCells[i].ready = true;
        }
    }

    public void manaUpgrade() {
        
        if (maxMana == ultMaxMana) {

            manaHeal(maxMana - 1);
        }

        else {

            maxMana += 1;
        
            manaCells.Add(gameObject.AddComponent<ManaCell>());
            manaCells[manaCells.Count - 1].index = manaCells.Count - 1;
            gameManager._UIManager.addManaOrb();
            Debug.Log("Mana Added - Total Mana:" + manaCells.Count);
       
        }

    }

    private void CastFireball() {

        if(spellCharge <= 1) {

            manaCost(1);
            Instantiate(fireSpell, attackSpawn.position, Quaternion.identity);
        }

        else {

            manaCost(2);
            Instantiate(fireSpell, attackSpawn.position, Quaternion.identity);
        }

        
    }

    public void attack() {

        attackFX();
        var swing = Instantiate(meleeAttack, attackSpawn.position, Quaternion.identity);
        
    }

    public void attackUp() {
        
        attackFX();
        Instantiate(meleeAttackUp, attackSpawnUp.position, Quaternion.Euler(0, 0, 90));
    }
    
    public void attackFX() {

        anim.SetTrigger("Attack");
        gameManager.audioManager.playAttackClip();
        actionDelay = meleeDelay;
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

        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, gameManager.whatIsGround());
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

    private void OnCollisionEnter2D(Collision2D other) {

        //knockBackForce = other.gameObject.transform;

        if(other.collider.tag == "Enemy")
        {
            Vector2 difference = (transform.position - other.transform.position).normalized;
            Vector2 force = difference * knockBackForce;
            rbody.AddForce(force, ForceMode2D.Impulse); //if you don't want to take into consideration enemy's mass then use ForceMode.VelocityChange
        }

        if (other.collider.tag == "KillVolume") {
            killPlayer();
        }
    }

}
