using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManaUIManager : MonoBehaviour
{
    [SerializeField] Image _manaBG, _manaCharge, _manaReady; 
    public int index;
    [SerializeField] private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
        //_manaBG.transform
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameManager.player.manaStatus[index] == false) {
            _manaReady.enabled = false;
            if ((index != 0) && (gameManager.player.manaStatus[index - 1] == true )) {
                _manaCharge.fillAmount = gameManager.player.manaCharge / 100f;
            }
        }
        
    }

    
}
