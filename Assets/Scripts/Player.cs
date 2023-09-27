using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float jumpHeight;
    private float speed;
    public float maxSpeed;
    public float acceleration; 
    public float deceleration;
    private Rigidbody2D rbody;
    enum face {none, left, right}; //Enum to get camera facing direction.  
    private face playerFace;
    private bool inAir;
    private int frame, delay;
    [SerializeField] private Canvas _debugCanvas;
    private bool debugDisplay = false;


    // Start is called before the first frame update
    void Start()
    {
        frame = 0;
        Debug.Log("Player Start");
        jumpHeight = 500f;
        speed = 0f;
        maxSpeed = 10f;
        acceleration = 15f; 
        deceleration = 10f;
        playerFace = face.none;
        rbody = GetComponent<Rigidbody2D>();

        //keep player object upright. 
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
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


        if (Input.GetKeyDown(KeyCode.Space) && !inAir) {
            inAir = true;
            Debug.Log("Jump input");
            rbody.AddForceY(jumpHeight);
            rbody.velocityY += 1f * Time.deltaTime;
            
        }

        if (rbody.velocityY == 0 && inAir) {
            inAir = false;
        }

        //Not sure if this will be used. 
        //I'm very tempted to make a keybinds menu.
        if (Input.GetKey(KeyCode.W)) {

        }

        //Maybe crouch input?
        if (Input.GetKey(KeyCode.S)) {
            
        }

        //Move left input.
        if (Input.GetKey(KeyCode.A) && (speed > -maxSpeed)) {
            //set face left
            playerFace = face.left;
            
            speed -= acceleration * Time.deltaTime;
            
        }
        
        //Move right input.
        else if (Input.GetKey(KeyCode.D) && (speed < maxSpeed)) {
            playerFace = face.right;
            
            speed += acceleration * Time.deltaTime;
        }

        else {
            if (speed > deceleration * Time.deltaTime) {
                speed -= deceleration * Time.deltaTime;
            }
            else if (speed < -deceleration * Time.deltaTime) {
                speed += deceleration * Time.deltaTime;
            }
            else speed = 0;
            //Debug.Log(speed);
        }
        
        rbody.velocityX = speed;

        
        if (Input.GetKey(KeyCode.F3) && delay < 0) {
            Debug.Log("Debug Toggle");
            _debugCanvas.enabled = true;
            delay = 180;
        }
        delay--;
        
    }

    public int getFace() {
        if (playerFace == face.right) {
            return 1;
        }
        else if (playerFace == face.left) {
            return -1;
        }
        else return 0;
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

    public Rigidbody2D GetRigidbody2D() {
        return rbody;
    }

    
    
}
