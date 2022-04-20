using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    float speed;
    //public GameObject RBWheel, RFWheel, LBWheel, LFWheel;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right*2);
        //rb.AddForce(Vector3.forward);
        

    }
}
