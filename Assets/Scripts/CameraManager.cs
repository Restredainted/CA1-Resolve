using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class CameraManager : MonoBehaviour
{

    /*
    This class contains sections of commented code where I 
    tried to figure out how to reference the player object in the scene,
    Eventually figured out I needed to reference MonoBehaviour to call scene objects.
    Camera follow code from :
    https://docs.unity3d.com/ScriptReference/Vector3.SmoothDamp.html
    */

    public Transform target;
    public float smoothTime = 1F;
    private Vector3 velocity = Vector3.zero, targetPosition;
    
    Player player;// = GetComponent<Player>;

    // Start is called before the first frame update
        void Start()
    {
        //target = gameObject.GetComponent<Player>().getPosition();
        targetPosition = new Vector3(0, 1, -10);
        
    }

    // Update is called once per frame
    void Update()
    {
        player = MonoBehaviour.FindFirstObjectByType<Player>();
        target = player.transform;

        
        //target = gameObject.GetComponent<Player>().getPosition();
        //Debug.Log(target.ToString()); //Debug line that didn't help at all. 
        
        // Define a target position above and behind the target transform,
        //adjusts for left or right face
        if (player.getFace() == 1) {
        targetPosition = target.TransformPoint(new Vector3(3, 1, -10));
        }    
        else if (player.getFace() == -1) {
        targetPosition = target.TransformPoint(new Vector3(-3, 1, -10));
        }      
        
        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
