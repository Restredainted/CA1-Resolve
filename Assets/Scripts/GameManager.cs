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
    public Player player;
    public UIManager UIManager;
    
    private float updateCount = 0;
    private float fixedUpdateCount = 0;
    private float updateUpdateCountPerSecond;
    private float updateFixedUpdateCountPerSecond;
    public bool debugEnabled = false;

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
        if (Application.isEditor) {
            debugEnabled = true;
        }
    }

    // Update is called once per frame
    // Increase the number of calls to Update.
    void Update()
    {
        updateCount += 1;
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            
        }
    }

    // Increase the number of calls to FixedUpdate.
    void FixedUpdate()
    {
        fixedUpdateCount += 1;
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
        SceneManager.LoadScene("MainMenu");
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
    IEnumerator Loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            updateUpdateCountPerSecond = updateCount;
            updateFixedUpdateCountPerSecond = fixedUpdateCount;

            updateCount = 0;
            fixedUpdateCount = 0;
        }
    }
}
