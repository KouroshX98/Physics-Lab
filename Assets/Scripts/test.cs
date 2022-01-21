using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject g;
    public float m;
    public const float GravitationalConstant = 6.67408e-11f;
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().mass = m;
        Vector3 d = gameObject.transform.position - g.transform.position;
        g.GetComponent<Rigidbody>().AddForce(((GravitationalConstant * g.GetComponent<test>().m * m) / Mathf.Pow(d.magnitude, 2)) * (d / d.magnitude));
    }
}
