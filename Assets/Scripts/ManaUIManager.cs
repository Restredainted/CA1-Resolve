using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManaUIManager : MonoBehaviour
{
    [SerializeField] public Image _manaBG, _manaCharge, _manaReady; 
    public int index;
    //private bool blinking = false;
    //[SerializeField] Color blinkingColor = new Color(1, 0.1f, 0.1f, 1f);
    [SerializeField] private GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        //_manaBG.transform
    }

    // Update is called once per frame
    void Update()
    {
        


        if (gameManager.player.manaCells[index].ready == false) {
            
            _manaReady.enabled = false;
            _manaCharge.fillAmount = gameManager.player.manaCells[index].recharge / gameManager.player.manaCharge;
            

            //couldn't get blinking to work. Everytime it was run in unity the entire engine would hang - not helpful
            /* for (int i = 0; i < 3; i = i) {
                
                bool activeBlink = false;
                
                if ((Time.frameCount % 10 == 0) && !activeBlink) {
                    _manaBG.GetComponent<SpriteRenderer>().color = blinkingColor;
                    activeBlink = true;
                }

                else if (Time.frameCount % 10 == 0 && activeBlink) {
                    _manaBG.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    activeBlink = false;
                    i++;
                }

                else if (i == 3) {
                    blinking = false;
                    break;
                }
                
            }
 */
        }

        else {
            _manaReady.enabled = true;
        }



    }


    //couldn't get blinking to work. Everytime it was run in unity the entire engine would hang - not helpful
    /* public void blink() {
        blinking = true;
        Debug.Log("Blinking Index: " + index);
    } */
    
}
