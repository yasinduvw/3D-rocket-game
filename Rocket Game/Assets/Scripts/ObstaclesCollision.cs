using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesCollision : MonoBehaviour
{
    GameObject rocket;
    RocketControls rocketControls;
    
    void Awake()
    {
        rocket = GameObject.Find("Rocket");
        rocketControls = rocket.GetComponent<RocketControls>();
    }

    private void OnCollisionEnter(Collision col) 
    {
        Debug.Log("Collision between obstacle and " + col.gameObject.name + " detected");     
        rocketControls.UpdateOnCollsion();
    }
}
