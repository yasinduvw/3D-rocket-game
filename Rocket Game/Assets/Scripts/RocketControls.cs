using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketControls : MonoBehaviour
{
    public Rigidbody rocket;
    public Rigidbody boulder1;
    public Rigidbody boulder2;
    public Rigidbody boulder3;
    public Rigidbody cablePart;
    public float force = 100000f;
    public float rotationSpeed = 70f;
    public float fuel = 150f;  
    public Image fuelBar;
    public TMPro.TextMeshProUGUI fuelPercentageText;
    public float fuelDepletionRate = 0.5f;    
    private float maxFuel; 
    private Vector3 startingPos;
    private Vector3 b1StartPos;
    private Vector3 b2StartPos;
    private Vector3 b3StartPos;
    private Quaternion startingRotation;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = rocket.position;
        b1StartPos = boulder1.position;
        b2StartPos = boulder2.position;
        b3StartPos = boulder3.position;
        startingRotation = rocket.rotation;
        maxFuel = fuel;
        // Changing gravitational acceleration
        Physics.gravity = new Vector3(0, -2.0f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        KeyStrokeControls();
        LimitFrame();
        CheckFuel();
        CheckSatus();
        UpdateUIComponents();
    }

    void KeyStrokeControls() 
    {
        if(Input.GetKey(KeyCode.UpArrow)) 
        {
            rocket.AddForce(transform.up * force * Time.deltaTime);
            fuel -= fuelDepletionRate*(Time.deltaTime);
        } 
        else 
        {
            if(rocket.velocity.y > 0) 
            {
                //to remove force
                rocket.velocity = Vector3.zero;
                rocket.angularVelocity = Vector3.zero; 
            }
        }
        
        if(Input.GetKey(KeyCode.DownArrow)) 
        {
            rocket.AddForce(-transform.up * force * Time.deltaTime);
            fuel -= fuelDepletionRate*(Time.deltaTime);
        } 

        if(Input.GetKey(KeyCode.LeftArrow)) 
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);
        }
        if(Input.GetKey(KeyCode.RightArrow)) 
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime, Space.Self);
        }

        if(Input.GetKey(KeyCode.R)) 
        {
            ResetGame();
        }

        if(Input.GetKey(KeyCode.F)) 
        {
            rocket.velocity = Vector3.zero;
            rocket.angularVelocity = Vector3.zero; 
        }

        if(Input.GetKey(KeyCode.Escape)) 
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }

        // //to check whether the accelration due to gravity works
        // Debug.Log(rocket.velocity);
        // Debug.DrawRay(transform.position, transform.up*50, Color.red, 0f);
    }

    // to limit the frame in which the rocket moves
    void LimitFrame()
    {
        if(transform.position.y >= 22f) 
        {
            transform.position = new Vector3(transform.position.x, 22f, transform.position.z);
        }

        if(transform.position.y <= 2.15f) 
        {
            transform.position = new Vector3(transform.position.x, 2.15f, transform.position.z);
        }

        if(transform.position.x >= 32f) 
        {
            transform.position = new Vector3(32f, transform.position.y, transform.position.z);
            Debug.Log("You can't go beyond this point");
        }

        if(transform.position.x <= -116f) 
        {
            transform.position = new Vector3(-116f, transform.position.y, transform.position.z);
            Debug.Log("You can't go beyond this point");
        }
    }

    void CheckFuel()
    {
        if (cablePart.GetComponent<FixedJoint>() != null) 
        {
            // Debug.Log(cablePart.GetComponent<FixedJoint>().connectedBody.name);
            if (cablePart.GetComponent<FixedJoint>().connectedBody.name == "Boulder (1)")
            {
                fuelDepletionRate = 1f; 
            }
            if (cablePart.GetComponent<FixedJoint>().connectedBody.name == "Boulder (2)")
            {
                fuelDepletionRate = 5f; 
            }
            if (cablePart.GetComponent<FixedJoint>().connectedBody.name == "Boulder (3)")
            {
                fuelDepletionRate = 3f; 
            }
        }
        else 
        {
            fuelDepletionRate = 0.5f;
        }

        if(fuel <= 0)
        {
            Debug.Log("Fuel fully depleted. Game Over");
            ResetGame();
        }
    }

    void CheckSatus()
    {
        if (boulder1.GetComponent<FixedJoint>() != null && boulder2.GetComponent<FixedJoint>() != null && boulder3.GetComponent<FixedJoint>() != null)
        {
            Debug.Log("Congrats! Game Completed");
            // End the game
            Application.Quit();
            // End the game in unity editor
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    void UpdateUIComponents()
    {
        fuelBar.fillAmount = fuel / maxFuel;
        int fuelPercentage = (int) (fuel / maxFuel * 100);
        fuelPercentageText.text = (fuelPercentage.ToString() + " %");
    }

    public void UpdateOnCollsion()
    {
        fuel -= 10;
        // Debug.Log("Rotation angle " + transform.rotation.z);
        // Debug.Log("Rotation angle " + transform.rotation.eulerAngles);
        if (transform.rotation.eulerAngles.z > 180) 
        {
            transform.position = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
        }
        else 
        {
            transform.position = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);
        }
        rocket.velocity = Vector3.zero;
        rocket.angularVelocity = Vector3.zero; 
    }

    void ResetGame() 
    {
        Destroy(cablePart.GetComponent<FixedJoint>());
        Destroy(boulder1.GetComponent<FixedJoint>());
        Destroy(boulder2.GetComponent<FixedJoint>());
        Destroy(boulder3.GetComponent<FixedJoint>());

        boulder1.position = b1StartPos;
        boulder1.velocity = Vector3.zero;
        boulder1.angularVelocity = Vector3.zero;

        boulder2.position = b2StartPos;
        boulder2.velocity = Vector3.zero;
        boulder2.angularVelocity = Vector3.zero;

        boulder3.position = b3StartPos;
        boulder3.velocity = Vector3.zero;
        boulder3.angularVelocity = Vector3.zero;

        transform.position = startingPos;
        transform.rotation = startingRotation;
        rocket.velocity = Vector3.zero;
        rocket.angularVelocity = Vector3.zero; 

        fuel = 150f;
        fuelDepletionRate = 0.5f;
    }

}
