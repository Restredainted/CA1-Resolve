using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    public bool playerClose;
    
   
    public void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "Player") {
            playerClose = true;
        Debug.Log("PlayerClose");
        }
    }
}
