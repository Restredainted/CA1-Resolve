using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;

public class Enemy_Slime : MonoBehaviour
{

    [SerializeField] private float maxHealth, detectRange, groundCheckDisctance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Animator anim;
    private float health, jumpDelay;
    private bool faceRight;
    

    // Start is called before the first frame update
    void Awake()
    {
        health = maxHealth;
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (jumpDelay <= 0) {

            jump();
        }
    }

    private void jump() {
        addforce
        anim.SetBool("Jump", true);
    }

    private void flip() {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        faceRight = !faceRight;
    }
    private void takeDamage(float damage) {
        health -= damage;
    }

    private int playerNearby() {
        if
        //player on left return -1
        if (player.transform.position.x >= transform.position.x - detectRange && ) {
            return -1;
        }
        //player on right return 1
        else if (transform.position.x + detectRange >= player.transform.position.x) {
            return 1;
        }
        else return false;
    }
}
