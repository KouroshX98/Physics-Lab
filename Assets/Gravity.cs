using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Rigidbody[] a;
    void FixedUpdate()
    {
        foreach(Rigidbody b in a)
        {
            b.AddForce(9.81f *((transform.position - b.transform.position) / (transform.position - b.transform.position).magnitude), ForceMode.Acceleration);
        }
    }
}
