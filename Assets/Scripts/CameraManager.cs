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
    public float smoothTime;
    private Vector3 velocity = Vector3.zero, targetPosition;
    [SerializeField] private Player player;// = GetComponent<Player>;


    void Awake() {
        if (player.getFace()) {
            targetPosition = target.TransformPoint(new Vector3(5, 2, -10));
        }    
        else targetPosition = target.TransformPoint(new Vector3(-5, 2, -10));
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        //target = gameObject.GetComponent<Player>().getPosition();
        //Debug.Log(target.ToString()); //Debug line that didn't help at all. 
        
        // Define a target position above and behind the target transform,
        //adjusts for left or right face
        if (player.getFace()) {
            targetPosition = target.TransformPoint(new Vector3(5, 2, -10));
        }    
        else targetPosition = target.TransformPoint(new Vector3(-5, 2, -10));
          
        /* if (Time.timeAsRational.Count % 5 == 0) {
            Debug.Log("Player Facing right: " + player.getFace());
         }*/
        
        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
