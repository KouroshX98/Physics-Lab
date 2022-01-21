using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class look : MonoBehaviour
{
    public Transform a;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 g = a.position - transform.position;
        g.x = g.z = 0.0f;
        gameObject.transform.LookAt(a.position - g);
        transform.Rotate(Vector3.up * 180);

    }
}
