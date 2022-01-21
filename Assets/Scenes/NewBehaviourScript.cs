using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Rigidbody a;
    public void Start()
    {
        a = GetComponent<Rigidbody>();
    }
    void Update()
    {
        a.constraints = RigidbodyConstraints.FreezeAll;
        a.constraints -= RigidbodyConstraints.FreezePositionX & RigidbodyConstraints.FreezePositionY;
    }
}
