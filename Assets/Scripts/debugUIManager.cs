using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class debugUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _debugTime, _debugFrames, _debugXPos, _debugYPos, _debugVelX, _debugVelY, _debugHealth, _debugMana;
    [SerializeField] private Player player;

    // Start is called before the first frame update
    void Start()
    {
        _debugXPos.text = "X Position: " + player.GetComponent<Rigidbody2D>().position.x.ToString();
        _debugYPos.text = "Y Position: " + player.GetComponent<Rigidbody2D>().position.y.ToString();
        _debugVelY.text = "Velocity Y: " + player.GetComponent<Rigidbody2D>().velocityY.ToString();
        _debugVelX.text = "Velocity X: " + player.GetComponent<Rigidbody2D>().velocityX.ToString();
        _debugHealth.text = "Health: " + player.health + " / " + player.maxHealth;
        _debugMana.text = "Mana: " + player.mana + " / " + player.maxMana;
        _debugTime.text = "Time: " + Time.time.ToString();
        _debugFrames.text = "Frame count: " + Time.frameCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _debugXPos.text = "X Position: " + player.GetComponent<Rigidbody2D>().position.x.ToString();
        _debugYPos.text = "Y Position: " + player.GetComponent<Rigidbody2D>().position.y.ToString();
        _debugVelY.text = "Velocity Y: " + player.GetComponent<Rigidbody2D>().velocityY.ToString();
        _debugVelX.text = "Velocity X: " + player.GetComponent<Rigidbody2D>().velocityX.ToString();
        _debugHealth.text = "Health: " + player.health + " / " + player.maxHealth;
        _debugMana.text = "Mana: " + player.mana + " / " + player.maxMana;
        _debugTime.text = "Time: " + Time.time.ToString();
        _debugFrames.text = "Frame count: " + Time.frameCount.ToString();
    }
}
   /* void toggleDebug() {
        if (enabled) {
            enabled = false;
        }
        else enabled = true;

    } */
