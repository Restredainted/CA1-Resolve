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
    public float healthAdjust; 
    private Transform healthAdjPos, healthBarPos;
    [SerializeField] private Image healthBar, healthChange, manaOrb;
    [SerializeField] private GameObject healthBarFullPos;
    [SerializeField] private GameObject[] ManaCount;
    private bool debugVisible;

    
    // Start is called before the first frame update
    void Start()
    {
        debugVisible = false;
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
        
        if (healthAdjust > player.health) {
            healthAdjust -= (math.abs(player.health-healthAdjust) / 1.5f * Time.deltaTime) + 0.5f;
        }
        else healthAdjust = player.health;

        healthChange.fillAmount = healthAdjust / 100f;
        healthChange.transform.localPosition = new Vector3(healthBarFullPos.transform.localPosition.x * (healthAdjust / 100f), healthBar.transform.localPosition.y, healthBar.transform.localPosition.z);

        healthBar.fillAmount = player.health / 100f;
        healthBar.transform.localPosition = new Vector3(healthBarFullPos.transform.localPosition.x * (player.health / 100f), healthBar.transform.localPosition.y, healthBar.transform.localPosition.z);

         
        
        
    }

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
