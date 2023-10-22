using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Game States")]
    public bool isPaused;
    public bool isGameOver;

    [Header("Primary Game Components")]
    public Player player;
    public UIManager UIManager;
    
    //Came from a tutorial, unused, not sure what they do.
    //private float updateCount = 0;
    //private float fixedUpdateCount = 0;
    //private float updateUpdateCountPerSecond;
    //private float updateFixedUpdateCountPerSecond;

    //moved into player scrip, used for debug ui toggle and keys.
    //private bool debugEnabled = false;
    
    [Header("Game Layers")]
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private LayerMask playerLayer; 
    [SerializeField] private LayerMask deathLayer; 
    [SerializeField] private LayerMask waterLayer;
    [SerializeField] private LayerMask enemyLayer;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
            return;
        }
        Application.targetFrameRate = 60;


    }

    void Start() {
        //moved into player
       /*  if (Application.isEditor) {
            debugEnabled = true;
        } */
    }

    // Update is called once per frame
    // Increase the number of calls to Update.
    void Update()
    {
        //unused from a tutorial in attempt to get debug overlay to work. 
        /* updateCount += 1;
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        } */

        if (Input.GetKeyDown(KeyCode.R)) {
            
        }
    }

    // Increase the number of calls to FixedUpdate.
    void FixedUpdate()
    {
        //fixedUpdateCount += 1;
    }

    public void PauseGame() {

        isPaused = true;
        Time.timeScale = 0f;

        //Pause Menu call
    }

    public void ResumeGame() {
        isPaused = false;
        Time.timeScale = 1f;
        //hide pause menu
    }

    public void GameOver() {
        isGameOver = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnMainMenu() {
        SceneManager.LoadScene(0);
    }

    // Show the number of calls to both messages.
    /* void OnGUI()
    {
        GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
        fontSize.fontSize = 24;
        GUI.Label(new Rect(100, 100, 200, 50), "Update: " + updateUpdateCountPerSecond.ToString(), fontSize);
        GUI.Label(new Rect(100, 150, 200, 50), "FixedUpdate: " + updateFixedUpdateCountPerSecond.ToString(), fontSize);
    } */
    // Update both CountsPerSecond values every second.
    /* IEnumerator Loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            updateUpdateCountPerSecond = updateCount;
            updateFixedUpdateCountPerSecond = fixedUpdateCount;

            updateCount = 0;
            fixedUpdateCount = 0;
        }
    } */

    public LayerMask whatIsGround() {
        return groundLayer;
    }
    public LayerMask whatIsPlayer() {
        return playerLayer;
    }
    public LayerMask whatIsDeath() {
        return deathLayer;
    }
    public LayerMask whatIsWater() {
        return waterLayer;
    }
    public LayerMask whatIsEnemy() {
        return enemyLayer;
    }
}
