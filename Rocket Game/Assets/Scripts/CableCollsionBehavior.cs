using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableCollsionBehavior : MonoBehaviour
{
    void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.name.Contains("Boulder") && gameObject.GetComponent<FixedJoint>() == null  && col.gameObject.GetComponent<FixedJoint>() == null) 
        {
            Debug.Log("Contact between Rocket and " + col.gameObject.name + " detected");
            gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<FixedJoint>().connectedBody = col.gameObject.GetComponent<Rigidbody>();
        }
    }

}
