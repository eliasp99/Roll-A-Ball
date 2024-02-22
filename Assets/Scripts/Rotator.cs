using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 1;
    public Vector3 rotationValue = new Vector3(15, 30, 45);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationValue *Time.deltaTime * speed);
    }
}
