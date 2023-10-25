using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Game Components")]
    //[SerializeField] private TextMeshProUGUI _debugTime, _debugVelY, _debugVelX, _debugFrames; //
    public static UIManager instance;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject _debugUI;
    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject GameOverMenu;
    [SerializeField] private Player player;
    
    //public float manaRecharge; 
    //private Transform healthAdjPos, healthBarPos;

    [Header("Health Bar")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image healthChange;
    [SerializeField] private GameObject healthBarFullPos;
    public float healthAdjust;

    [Header("Mana Charge Bar")]
    [SerializeField] private Image manaChargeBar;
    //[SerializeField] private Image manaChargeChange; //not needed for charge up
    [SerializeField] private GameObject manaChargeBarFullPos;
    private float maxManaCharge;
    public float manaCharge;


    [Header("Mana Cells")]
    [SerializeField] private GameObject manaOrb;
    [SerializeField] private GameObject manaOrbPos;
    [SerializeField] private List<GameObject> manaCount = new List<GameObject>();
    private bool debugVisible;

    
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

        debugVisible = false;
        
        gameManager = FindFirstObjectByType<GameManager>();
        player = FindFirstObjectByType<Player>();
        
        for (int i = 0; i < player.maxMana; i += 1) {

            //Drawn successfully using below forum post:
            //https://discussions.unity.com/t/instantiate-as-a-child-of-the-parent/43354
            var manaOrbNew = Instantiate(manaOrb, new Vector2(manaOrbPos.transform.position.x 
            + (60 * i), manaOrbPos.transform.position.y), manaOrbPos.transform.rotation);
            manaOrbNew.transform.SetParent(manaOrbPos.transform);
            manaOrbNew.transform.localScale = new Vector3(1, 1, 1);
            manaOrbNew.GetComponent<ManaUIManager>().index = i;
            //new Vector2(6,2);
            //manaOrbNew.transform.set-
            manaCount.Add(manaOrbNew);
            
        }

        //Set end point for maximum HP bar. 
        healthBarFullPos.transform.localPosition = healthBar.transform.localPosition;
        manaChargeBarFullPos.transform.localPosition = manaChargeBar.transform.localPosition;
        maxManaCharge = player.maxManaCharge / 2;
        /* Moved and updated in DebugUIManager
        rbody = player.GetRigidbody2D();

        _debugVelY.text = "Velocity Y: " + rbody.velocityY.ToString();
        _debugVelX.text = "Velocity X: " + rbody.velocityX.ToString();
        _debugTime.text = Time.frameCount.ToString();
        _debugFrames.text = Time.time.ToString(); */

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) {

            player = FindFirstObjectByType<Player>();
        }
        /* Moved and updated in DebugUIManager
        _debugVelY.text = "Velocity Y: " + player.GetComponent<Rigidbody2D>().velocityY.ToString();
        _debugVelX.text = "Velocity X: " + player.GetComponent<Rigidbody2D>().velocityX.ToString();
        _debugTime.text = "Time: " + Time.time.ToString();
        _debugFrames.text = "Frame count: " + Time.frameCount.ToString(); 
        for(int i = 0; i <= manaCount.Count; i++) {
            //manaCount[i].SetActive(true);
        }*/


        //Calculate health Slow damage amount
        if (healthAdjust > player.health) {
            healthAdjust -= (math.abs((player.health - healthAdjust) / player.maxHealth) / 1.5f * Time.deltaTime) + 0.5f;
        }
        else healthAdjust = player.health;
        

        //Health Bar slow damage
        healthChange.fillAmount = healthAdjust / player.maxHealth;
        healthChange.transform.localPosition = new Vector3(healthBarFullPos.transform.localPosition.x * (healthAdjust / player.maxHealth), healthBar.transform.localPosition.y, healthBar.transform.localPosition.z);

        //Health Bar Actual Damage
        healthBar.fillAmount = player.health / player.maxHealth;
        healthBar.transform.localPosition = new Vector3(healthBarFullPos.transform.localPosition.x * (player.health / player.maxHealth) , healthBar.transform.localPosition.y, healthBar.transform.localPosition.z);
        

        manaCharge = Mathf.Clamp(player.spellCharge, 0, maxManaCharge) ;

        //Mana charge bar adjust
        manaChargeBar.fillAmount = player.spellCharge / maxManaCharge;
        manaChargeBar.transform.localPosition = new Vector3(manaChargeBarFullPos.transform.localPosition.x * (manaCharge / maxManaCharge) , manaChargeBar.transform.localPosition.y, manaChargeBar.transform.localPosition.z);

        //Debug.Log(manaCount.Count);

       /*  if (Time.time % 10 == 0) {}
        for (int i = 0; i <= 2; i++) {
            if (player.mana >= i) {
                //manaCount[i]
            }   
            
            
        }  */
    }
        
    
   /*  public void manaUpgrade() {
        
            var manaOrbNew = Instantiate(manaOrb, new Vector2(manaOrbPos.transform.position.x + (60 * (manaCount.Count + 1)), manaOrbPos.transform.position.y), manaOrbPos.transform.rotation);
            manaOrbNew.transform.SetParent(manaOrbPos.transform);
            manaOrbNew.GetComponent<ManaCell>().index = manaCount.Count + 1;
            //new Vector2(6,2);
            //manaOrbNew.transform.set-
            manaCount.Add(manaOrbNew);
            
        
    } */

    //Debug Display Toggle Method. 
    public void toggleDebug() {

        if (debugVisible) {
            
            debugVisible = false;
            _debugUI.SetActive(false);
        }

        else {

            debugVisible = true;
            _debugUI.SetActive(true);
        }
    }

    public void togglePause() {
        //Turn off gameUI and turn on pause Menu
        // if (gameManager.isPaused) {
            GameUI.SetActive(!gameManager.isPaused);
            pauseMenu.SetActive(gameManager.isPaused);
        /* }

        else {
            GameUI.SetActive(true);
            pauseMenu.SetActive(false);
        } */


        //Turn on gameUI and turn off pause Menu
        
    }

    public void toggleGameOver() {

        //Turn off gameUI and turn on game over
        GameUI.SetActive(!gameManager.isGameOver);
        GameOverMenu.SetActive(gameManager.isGameOver);
    }

    public void addManaOrb() {

        var manaOrbNew = Instantiate(manaOrb, new Vector2(manaOrbPos.transform.position.x + (60 * manaCount.Count), manaOrbPos.transform.position.y), manaOrbPos.transform.rotation);
        manaOrbNew.transform.SetParent(manaOrbPos.transform);
        manaOrbNew.transform.localScale = new Vector3(1, 1, 1);
        manaOrbNew.GetComponent<ManaUIManager>().index = manaCount.Count;
        manaCount.Add(manaOrbNew);
    }

    //couldn't get blinking to work. Everytime it was run in unity the entire engine would hang - not helpful
    /* public void manaBlink(int cost) {
        for (int i = 0; i < cost; i += 1) {
            if (player.manaCells[i].ready == false){
                manaCount[i].GetComponent<ManaUIManager>().blink();
            }
        }
    }
 */

}
