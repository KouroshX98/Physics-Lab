using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFaceCamera : MonoBehaviour
{
    public PhysicObject Object;
    public Text Mass, Force, Acceleration, Velocity, PotentialEnergy, KineticEnergy,Name;
    public Vector3 Offset = Vector3.zero;
    void Start()
    {
        
    }

    void Update()
    {
        Name.text = Object.name;
        transform.position = Object.transform.TransformPoint(Object.physicObjectRigidbody.centerOfMass) + Offset;
        Vector3 g = Camera.main.transform.position -transform.position;
        g.x = g.z = 0.0f;
        gameObject.transform.LookAt(Camera.main.transform.position - g);
        transform.Rotate(Vector3.up * 180);
        Acceleration.text = "Acceleration : " + Object.Acceleration.ToString();
        Velocity.text = "Velocity : " + Object.physicObjectRigidbody.velocity.ToString();
        PotentialEnergy.text = "Potential Energy : " + Object.PotentialEnergy.ToString() + " J";
        KineticEnergy.text = "Kinetic Energy : " + Object.KineticEnergy.ToString() + " J";
        Mass.text = "Mass : " + Object.Mass.ToString() + " kG";
        Force.text = "Net Force : " + (Object.constantForce.force + Object.transform.TransformVector(Object.constantForce.relativeForce)).ToString();

    }
}
