using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingBehavior : MonoBehaviour
{
   public Rigidbody cablePart;

    void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.name.Contains("Boulder")) 
        {
            Debug.Log("Contact between Drop Zone and " + col.gameObject.name + " detected");
            Destroy(cablePart.GetComponent<FixedJoint>());
            col.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            col.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            col.gameObject.AddComponent<FixedJoint>();
            col.gameObject.GetComponent<FixedJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();
        }
    }
    
}
