using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.forward * speed;
    }
}