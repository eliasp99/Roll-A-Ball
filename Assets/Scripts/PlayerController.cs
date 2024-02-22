using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody rb;
    private int pickUpCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Get the number of pickups in our scebe
        pickUpCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Run the Check Pickups Function
        CheckPickUps();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            print(other.name);
            Destroy(other.gameObject);
            //Decrement the pickup count
            pickUpCount--;
            //Run the Check Pick Ups function
            CheckPickUps();
        }

    }

    void CheckPickUps()
    {
        print("Pick Ups Left: " + pickUpCount);
        if(pickUpCount == 0)
        {
            print("Yay! You won");
        }
    }
}
