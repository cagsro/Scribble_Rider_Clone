using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public GameObject RBWheel, RFWheel, LBWheel,LFWheel;
    [SerializeField] Rigidbody RBWheelRb, RFWheelRb, LBWheelRb ,LFWheelRb;
    // Update is called once per frame
    void Update()
    {
        RBWheel.transform.Rotate(Vector3.right);
        RFWheel.transform.Rotate(Vector3.right);
        LBWheel.transform.Rotate(Vector3.right);
        LFWheel.transform.Rotate(Vector3.right);
        /*RFWheelRb.AddTorque(transform.right * 300);
        LBWheelRb.AddTorque(transform.right * 300);
        LFWheelRb.AddTorque(transform.right * 300);
        */
    }
}
