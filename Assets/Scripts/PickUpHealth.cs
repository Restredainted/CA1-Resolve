using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealth : MonoBehaviour
{
    [SerializeField] private float heal;
    private Rigidbody2D rbody;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocityY = 2;
    }

    public void OnCollisionEnter2D(Collision2D other) {

        if (other.collider.tag == "Player") {

            FindFirstObjectByType<GameManager>().player.heal(heal);
            Destroy(this.gameObject);
        }
    }
}
