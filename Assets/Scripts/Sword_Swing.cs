using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Swing : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] private float expireTime;
    //private Collision2D collision2D;
    //[SerializeField] private Transform hit; //Using oncollisionEnter2D instead
    private Player player; 
    private bool faceRight;

    // Start is called before the first frame update
    void Start()
    {
        
        player = FindAnyObjectByType<Player>();
        
        faceRight = !player.getFace();
        transform.SetParent(player.transform);
        
        if (faceRight) {

            flip();
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
        expireTime -= Time.deltaTime;
        
        if (expireTime <= 0) {

            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {

        other.gameObject.GetComponent<Enemy_Slime>().takeDamage(damage);
        //other.gameObject.GetComponent<enemyController>().
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
