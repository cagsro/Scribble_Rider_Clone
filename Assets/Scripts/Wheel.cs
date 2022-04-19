using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public GameObject Sag, Sol, SagArka, SolArka;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.tag=="Sag")
        {
            this.transform.position = Sag.transform.position;
        }
        if (this.gameObject.tag == "Sol")
        {
            this.transform.position = Sol.transform.position;
        }
        if (this.gameObject.tag == "SagArka")
        {
            this.transform.position = SagArka.transform.position;
            if (this.gameObject.tag == "SolArka")
            {
                this.transform.position = SolArka.transform.position;
            }
        }
    }
}
