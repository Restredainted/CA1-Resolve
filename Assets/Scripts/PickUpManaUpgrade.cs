using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpManaUpgrade : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D other) {

        if (other.collider.tag == "Player") {
            
            FindFirstObjectByType<GameManager>().player.manaUpgrade();
            Destroy(this.gameObject);
        }
    }
    
}
