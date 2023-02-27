using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    public float movementSpeed = 0.5f;
    public float rotationSpeed = 10f;
    private Vector3 target;
    private Vector3 targetAbove;
    private Vector3 targetBelow;
    // Start is called before the first frame update
    void Start()
    {
        targetAbove = transform.position;
        targetAbove.y += 1;
        targetBelow = transform.position;
        targetBelow.y -= 1;
        target = targetAbove;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
        if(transform.position == targetAbove) {
            target = targetBelow;
        } 
        else if(transform.position == targetBelow) {
            target = targetAbove;
        }
    }
}
