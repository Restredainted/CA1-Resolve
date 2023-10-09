using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCell : MonoBehaviour
{
    [SerializeField] private Player player;
    public float recharge;
    public int index;
    public bool ready;

    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<Player>();
        ready = true;
        recharge = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!ready) {
            recharge += Time.fixedUnscaledTime;
        }
        if (recharge > player.manaCharge) {
            ready = true;
        }
    }

    public void cast() {
        Debug.Log("Debug Spell Cast");
        recharge = 0;
        ready = false;
    }

}
