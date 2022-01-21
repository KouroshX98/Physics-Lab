using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fv : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody>().AddRelativeForce(transform.forward * 39.5f, ForceMode.VelocityChange);
    }
}
