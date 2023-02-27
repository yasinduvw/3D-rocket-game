using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        if(player.position.x >= 32f) 
        {
            transform.position = new Vector3(32f, transform.position.y, transform.position.z);
        }
        else if(player.position.x <= -116f) 
        {
            transform.position = new Vector3(-116f, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.position.x, transform.position.y, player.position.z - 20);
        }
        // transform.position = new Vector3(player.position.x, player.position.y, player.position.z - 20);  //If the camera is to be adjusted according to the veritcal position of the rocket
    }
    
}
