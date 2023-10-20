using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip jumpClip;
    public AudioClip footStepClip;
    public AudioClip backgroundMusic;
    public AudioClip combatMusic;
    public AudioClip attackClip;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
            //instance =
        }
        //else
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
