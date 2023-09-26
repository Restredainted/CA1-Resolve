using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpHeight;
    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player Start");
        jumpHeight = 2.5f;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Jump input");
            rigidbody.AddForceY(jumpHeight);
        }
    }
}
