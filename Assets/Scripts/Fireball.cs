using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed, charge, expireTime;
    //private Collision2D collision2D;
    //[SerializeField] private Transform hit; //Using oncollisionEnter2D instead
    private GameManager gameManager; 
    private AudioSource fxsound;
    private bool expired;
    private bool faceRight; //Calls gameManager method directly on spawn





    // Start is called before the first frame update
    void Start()
    {

        gameManager = FindAnyObjectByType<GameManager>();
        faceRight = !gameManager.player.getFace();
        charge = gameManager.player.spellCharge;
        
        expired = false;
        fxsound = Instantiate<AudioSource>(gameManager.audioManager.playFireBall());
        

        if (faceRight) {
            flip();
            speed *= -1;
        } 

        //collision2D = GetComponent<Collision2D>();



        //To save time I'm not adding a formula for this and just hard coding the damage rates. 
        // Had I the time I would implement a stat system that the strength of the spell would work in conjunction with the charged rate. 
        
        if (charge > 1) {
            
            Debug.Log("Charge scalev  over 1");

            Vector3 chargeScale = gameObject.transform.localScale;
            chargeScale.x *= 1.5f;
            chargeScale.y *= 1.5f;
            
            
            gameObject.transform.localScale = chargeScale;

            
            damage = 30;
        }
        else {

            damage = 10;
        }



    }

    // Update is called once per frame
    void Update()
    {

        
            transform.position += new Vector3(speed, 0,0) * Time.deltaTime;
        
        

        if (expired) {
            if (fxsound != null) 
                gameManager.audioManager.stopFireball(fxsound);
            expireTime -= Time.deltaTime;
        }

        if (expireTime <= 0) {

            Destroy(this.gameObject);
        }
    }

    

    private void OnCollisionEnter2D(Collision2D other) {
        speed = 0;
        expired = true;

        if (other.collider.tag == "Enemy") {
            other.gameObject.GetComponent<Enemy_Slime>().takeDamage(damage);
        }

        
        //anim poof
        //do damage to the collided
        //other
        //if (other.)
        //other.doDamage(damage);
    }

    private void flip() {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        
    }
    
}
