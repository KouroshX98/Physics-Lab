using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVelocity : MonoBehaviour
{
    public GameObject Earth;
    void Start()
    {
        float dis = (Earth.transform.position - transform.position).magnitude;
        float v = dis * 9.81f;
        v = Mathf.Sqrt(v);
        GetComponent<Rigidbody>().AddForce(transform.forward * v, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
