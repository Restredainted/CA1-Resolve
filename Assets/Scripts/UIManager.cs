using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    //[SerializeField] private TextMeshProUGUI _debugTime, _debugVelY, _debugVelX, _debugFrames; //
    [SerializeField] private GameObject _debugUI;
    [SerializeField] private Player player;
    [SerializeField] private Image healthBar, manaBar;
    private bool debugVisible;

    
    // Start is called before the first frame update
    void Start()
    {
        debugVisible = false;
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
        
        healthBar.fillAmount = player.health / 100f;
        manaBar.fillAmount = player.mana / 100f;
       
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
