using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _debugTime, _debugVelY, _debugVelX, _debugFrames;
    
    
    [SerializeField] private Player player;
    private Rigidbody2D rbody;
    // Start is called before the first frame update
    void Start()
    {
        
        rbody = player.GetRigidbody2D();

        _debugVelY.text = "Velocity Y: " + rbody.velocityY.ToString();
        _debugVelX.text = "Velocity X: " + rbody.velocityX.ToString();
        _debugTime.text = Time.frameCount.ToString();
        _debugFrames.text = Time.time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
        _debugVelY.text = "Velocity Y: " + player.GetComponent<Rigidbody2D>().velocityY.ToString();
        _debugVelX.text = "Velocity X: " + player.GetComponent<Rigidbody2D>().velocityX.ToString();
        _debugTime.text = "Time: " + Time.time.ToString();
        _debugFrames.text = "Frame count: " + Time.frameCount.ToString();
       
    }

   /* void toggleDebug() {
        if (enabled) {
            enabled = false;
        }
        else enabled = true;

    } */


}
