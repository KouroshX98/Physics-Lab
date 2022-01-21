using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicObject : MonoBehaviour
{
    /// <summary>
    /// Force UI
    /// </summary>
    public float FX, FY, FZ;
    /// <summary>
    /// Relative Force UI
    /// </summary>
    public float FRX, FRY, FRZ;
    /// <summary>
    /// Torque UI
    /// </summary>
    public float TX, TY, TZ;
    /// <summary>
    /// Relative Torque UI
    /// </summary>
    public float TRX, TRY, TRZ;

    public float DynamicFrictionConstant;
    public float StaticFrictionConstant;
    private GameManager gameManager;
    private PhysicMaterial physicMaterial;
    public float Mass = 1;
    public float ElectricCharge = 0;
    public PhysicSystem physicSystem;
    [HideInInspector] public Vector3 NetForce = Vector3.zero;
    [HideInInspector] public float PotentialEnergy = 0;
    [HideInInspector] public float KineticEnergy = 0;
    [HideInInspector] public Vector3 Acceleration = Vector3.zero;
    private Vector3 ElectricForce = Vector3.zero;
    private Vector3 GravityForce = Vector3.zero;
    [HideInInspector] public Rigidbody physicObjectRigidbody;
    private Vector3 v = Vector3.zero;
    [HideInInspector] public ConstantForce constantForce;
    //public UIFaceCamera InfoUI;
    public PhysicMaterialCombine physicMaterialCombine;
    private Collider collider;
    public float Bounciness = 0;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameObject.tag != "PhysicObjectKinematic")
        {
            physicMaterial = new PhysicMaterial();
            physicObjectRigidbody = gameObject.GetComponent<Rigidbody>();
            constantForce = gameObject.GetComponent<ConstantForce>();
            physicObjectRigidbody.maxAngularVelocity = 100f;
            collider = GetComponent<Collider>();
        }
    }
    void Update()
    {
        if (gameObject.tag != "PhysicObjectKinematic")
        {
            if (physicObjectRigidbody.velocity.magnitude <= 0.1)
            {
                physicMaterial.staticFriction = StaticFrictionConstant;
                physicMaterial.dynamicFriction = 0;
            }
            else
            {
                physicMaterial.staticFriction = 0;
                physicMaterial.dynamicFriction = DynamicFrictionConstant;
            }
            physicMaterial.frictionCombine = physicMaterialCombine;
            physicMaterial.bounceCombine = physicMaterialCombine;
            physicMaterial.bounciness = Bounciness;
            collider.material = physicMaterial;
        }
    }
    public void FixedUpdate()
    {
        if (gameObject.tag != "PhysicObjectKinematic")
        {

            constantForce.force = Vector3.zero;
            constantForce.relativeForce = Vector3.zero;
            constantForce.torque = Vector3.zero;
            constantForce.relativeTorque = Vector3.zero;

            physicObjectRigidbody.mass = Mass;
            physicObjectRigidbody.mass = Mass;
            GravityForce = Vector3.zero;
            NetForce = Vector3.zero;
            PotentialEnergy = 0;
            KineticEnergy = 0;
            GravityForce += physicSystem.Gravity * Mass;
            Vector3 differce = transform.TransformPoint(physicObjectRigidbody.centerOfMass) - physicSystem.transform.position;
            PotentialEnergy = GravityForce.x * differce.x + GravityForce.y * differce.y + GravityForce.z * differce.z;
            KineticEnergy = 0.5f * Mass * Mathf.Pow(physicObjectRigidbody.velocity.magnitude, 2f);
            foreach (PhysicObject a in physicSystem.Objects)
            {
                if (a != this && a.tag != "PhysicObjectKinematic")
                {
                    Vector3 distance = a.physicObjectRigidbody.worldCenterOfMass - physicObjectRigidbody.worldCenterOfMass;
                    GravityForce += ((PhysicSystem.GravitationalConstant * Mass * a.Mass) / Mathf.Pow(distance.magnitude, 2f)) * (distance / distance.magnitude);
                    ElectricForce += (PhysicSystem.ColoumbsConstant * (ElectricCharge * a.ElectricCharge) / Mathf.Pow(distance.magnitude, 2f)) * (distance / distance.magnitude) * -1f;
                }
            }
            Acceleration = physicObjectRigidbody.velocity - v;
            v = physicObjectRigidbody.velocity;
            Acceleration /= Time.fixedDeltaTime;
            NetForce += (GravityForce + ElectricForce);
            constantForce.force += NetForce;

            constantForce.force += new Vector3(FX, FY, FZ);
            constantForce.relativeForce += new Vector3(FRX, FRY, FRZ);
            constantForce.torque += new Vector3(TX, TY, TZ);
            constantForce.relativeTorque += new Vector3(TRX, TRY, TRZ);


            PotentialEnergy *= -1f;
            KineticEnergy = ((int)(KineticEnergy * 100)) / 100f;
            PotentialEnergy = ((int)(PotentialEnergy * 100)) / 100f;
            if (gameManager.objectEdit.gameObject.activeSelf)
            {
                gameManager.objectEdit.Up();
            }
        }
    }
}