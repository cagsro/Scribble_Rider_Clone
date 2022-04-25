using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    public Rigidbody targetRB;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        speed = 8;
        rb=GetComponent<Rigidbody>();
        if(targetRB!=null)
        {
            offset = rb.position - targetRB.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddTorque(Vector3.right * speed);

        if (targetRB != null)
        {
            Vector3 rbPos = rb.position;
            rbPos.z = targetRB.position.z + offset.z;
            rb.position = rbPos;
        }
    }
}
