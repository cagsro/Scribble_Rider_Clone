using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{ 
    public Transform kasa, wheelFront, wheelBack,firstWheel;
    private Rigidbody rb;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        offset = rb.position - firstWheel.GetComponent<Rigidbody>().position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.position = firstWheel.GetComponent<Rigidbody>().position + offset;
        KasaRotation();
    }
    public void KasaRotation()
    {
        float distXRot = wheelFront.position.y - wheelBack.position.y;
        Vector3 rot = kasa.eulerAngles;
        rot.x = distXRot * -15;
        kasa.eulerAngles = rot;
    }
}
