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
    [SerializeField] private bool faceRight;





    // Start is called before the first frame update
    void Start()
    {
        //owner = parent;
        faceRight = owner.GetComponent<Player>().getFace();
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
    
}
