using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManaUIManager : MonoBehaviour
{
    [SerializeField] public Image _manaBG, _manaCharge, _manaReady; 
    public int index;
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
            
            _manaCharge.fillAmount = gameManager.player.manaCells[index].recharge / 100f;
            
        }
        else {
            _manaReady.enabled = true;
        }


        
    }

    
}
