using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private GameManager gameManager;
    [SerializeField] private float detectRadius;
    [SerializeField] private float respawnTimer;
    [SerializeField] private GameObject enemyToSpawn;
    private float respawn;
    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null && playerClose() && respawn <= 0) {

            enemy = Instantiate(enemyToSpawn, this.transform.position, Quaternion.identity);
            respawn = respawnTimer;
        }

        else respawnTimer -= Time.deltaTime;
    }

    //Detects if the player is nearby. 
    private bool playerClose() {
        
        return Physics2D.OverlapCircle(transform.position, detectRadius, gameManager.whatIsPlayer());
    }
}
