using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    //[SerializeField] private TextMeshProUGUI _debugTime, _debugVelY, _debugVelX, _debugFrames; //
    [SerializeField] private GameObject _debugUI;
    [SerializeField] private Player player;
    public float healthAdjust, manaRecharge; 
    //private Transform healthAdjPos, healthBarPos;
    [SerializeField] private Image healthBar, healthChange;
    [SerializeField] private GameObject healthBarFullPos, manaOrb, manaOrbPos;
    [SerializeField] private List<GameObject> manaCount = new List<GameObject>();
    private bool debugVisible;

    
    // Start is called before the first frame update
    void Start()
    {
        debugVisible = false;
        
        for (int i = 0; i < player.maxMana; i += 1) {

            //Drawn successfully using below forum post:
            //https://discussions.unity.com/t/instantiate-as-a-child-of-the-parent/43354
            var manaOrbNew = Instantiate(manaOrb, new Vector2(manaOrbPos.transform.position.x + (60 * i), manaOrbPos.transform.position.y), manaOrbPos.transform.rotation);
            manaOrbNew.transform.SetParent(manaOrbPos.transform);
            manaOrbNew.transform.localScale = new Vector3(1, 1, 1);
            manaOrbNew.GetComponent<ManaUIManager>().index = i;
            //new Vector2(6,2);
            //manaOrbNew.transform.set-
            manaCount.Add(manaOrbNew);
            
        }

        //Set end point for maximum HP bar. 
        healthBarFullPos.transform.localPosition = healthBar.transform.localPosition;
        

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
        
        /* Moved and updated in DebugUIManager
        _debugVelY.text = "Velocity Y: " + player.GetComponent<Rigidbody2D>().velocityY.ToString();
        _debugVelX.text = "Velocity X: " + player.GetComponent<Rigidbody2D>().velocityX.ToString();
        _debugTime.text = "Time: " + Time.time.ToString();
        _debugFrames.text = "Frame count: " + Time.frameCount.ToString(); */
        for(int i = 0; i <= manaCount.Count; i++) {
            //manaCount[i].SetActive(true);
        }


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
        
        Debug.Log(manaCount.Count);

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


}
