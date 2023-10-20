using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private Collision2D collision2D;
    [SerializeField] private Transform hit;
    [SerializeField] private GameObject owner;

    private Player gamePlayer; 
    //[SerializeField] private bool faceRight; //Calls player method directly on spawn





    // Start is called before the first frame update
    void Start()
    {

        gamePlayer.spellCharge
        //owner = parent;
        if (owner.GetComponent<Player>().getFace()) {
            flip();
        }

        collision2D = GetComponent<Collision2D>();

        if (damage > 1) {
            transform.localScale = transform.localScale * damage;
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed, 0,0) * Time.deltaTime;
    }

    

    private void OnCollisionEnter2D(Collision2D other) {
        speed = 0;
        //anim poof
        //do damage to the collided
        //other
    }

    private void flip() {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        
    }
    
}
