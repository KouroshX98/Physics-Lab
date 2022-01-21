using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectEdit : MonoBehaviour
{
    public InputField objName;
    /// <summary>
    /// Position UI
    /// </summary>
    public InputField PX, PY, PZ;
    /// <summary>
    /// Rotation UI
    /// </summary>
    public InputField RX, RY, RZ;
    /// <summary>
    /// Force UI
    /// </summary>
    public InputField FX, FY, FZ;
    /// <summary>
    /// Relative Force UI
    /// </summary>
    public InputField FRX, FRY, FRZ;
    /// <summary>
    /// Torque UI
    /// </summary>
    public InputField TX, TY, TZ;
    /// <summary>
    /// Relative Torque UI
    /// </summary>
    public InputField TRX, TRY, TRZ;
    /// <summary>
    /// Inertia Tensor UI
    /// </summary>
    public InputField ITX, ITY, ITZ;
    /// <summary>
    /// Center of Mass UI
    /// </summary>
    public InputField CMX, CMY, CMZ;
    /// <summary>
    /// Constraints
    /// </summary>
    public Toggle FSX, FSY, FSZ, PRX, PRZ, PRY;
    /// <summary>
    /// Mass & Charge
    /// </summary>
    public InputField M, C;
    /// <summary>
    /// Static Friction, Kinetic Friction, Bounciness
    /// </summary>
    public InputField SF, DF, B;
    /// <summary>
    /// Material Combine & Force Mode
    /// </summary>
    public Dropdown MC, FM;
    /// <summary>
    /// Drag & Angular Drag
    /// </summary>
    public InputField D, AD;
    /// <summary>
    /// Manual Forces
    /// </summary>
    public InputField FBX, FBY, FBZ, FRBX, FRBY, FRBZ;
    public void Up()
    {
        if(!objName.isFocused)
            objName.text = GameManager.currentPhysicObject.name;

        if (!PX.isFocused)
            PX.text = GameManager.currentPhysicObject.transform.position.x.ToString();
        if (!PY.isFocused)
            PY.text = GameManager.currentPhysicObject.transform.position.y.ToString();
        if (!PZ.isFocused)
            PZ.text = GameManager.currentPhysicObject.transform.position.z.ToString();


        if (!RX.isFocused)
            RX.text = GameManager.currentPhysicObject.transform.eulerAngles.x.ToString();
        if (!RY.isFocused)
            RY.text = GameManager.currentPhysicObject.transform.eulerAngles.y.ToString();
        if (!RZ.isFocused)
            RZ.text = GameManager.currentPhysicObject.transform.eulerAngles.z.ToString();

        //if (!FX.isFocused)
        //    FX.text = GameManager.currentPhysicObject.FX.ToString();
        //if (!FY.isFocused)                           
        //    FY.text = GameManager.currentPhysicObject.FY.ToString();
        //if (!FZ.isFocused)                           
        //    FZ.text = GameManager.currentPhysicObject.FZ.ToString();

        //GameManager.currentPhysicObject.constantForce.force += new Vector3(float.Parse(FX.text), float.Parse(FY.text), float.Parse(FZ.text));
        //GameManager.currentPhysicObject.constantForce.relativeForce += new Vector3(float.Parse(FRX.text), float.Parse(FRY.text), float.Parse(FRZ.text));
        //GameManager.currentPhysicObject.constantForce.torque += new Vector3(float.Parse(TX.text), float.Parse(TY.text), float.Parse(TZ.text));
        //GameManager.currentPhysicObject.constantForce.relativeTorque += new Vector3(float.Parse(TRX.text), float.Parse(TRY.text), float.Parse(TRZ.text));


        //if (!ITX.isFocused)
        //    ITX.text = GameManager.currentPhysicObject.physicObjectRigidbody.inertiaTensor.x.ToString();
        //if (!ITY.isFocused)
        //    ITY.text = GameManager.currentPhysicObject.physicObjectRigidbody.inertiaTensor.y.ToString();
        //if (!ITZ.isFocused)
        //    ITZ.text = GameManager.currentPhysicObject.physicObjectRigidbody.inertiaTensor.z.ToString();
        //
        //if (!CMX.isFocused)
        //    CMX.text = GameManager.currentPhysicObject.physicObjectRigidbody.centerOfMass.x.ToString();
        //if (!CMY.isFocused)
        //    CMY.text = GameManager.currentPhysicObject.physicObjectRigidbody.centerOfMass.y.ToString();
        //if (!CMZ.isFocused)
        //    CMZ.text = GameManager.currentPhysicObject.physicObjectRigidbody.centerOfMass.z.ToString();

    }

    public void setAtt(InputField a)
    {
        switch (a.name)
        {
            case "PY": GameManager.currentPhysicObject.physicObjectRigidbody.position = new Vector3(float.Parse(PX.text), float.Parse(PY.text), float.Parse(PZ.text)); break;
            case "PZ": GameManager.currentPhysicObject.physicObjectRigidbody.position = new Vector3(float.Parse(PX.text), float.Parse(PY.text), float.Parse(PZ.text)); break;
            case "PX": GameManager.currentPhysicObject.physicObjectRigidbody.position = new Vector3(float.Parse(PX.text), float.Parse(PY.text), float.Parse(PZ.text)); break;
                
            case "RX": GameManager.currentPhysicObject.transform.eulerAngles = new Vector3(float.Parse(RX.text), float.Parse(RZ.text), float.Parse(RY.text)); break;
            case "RY": GameManager.currentPhysicObject.transform.eulerAngles = new Vector3(float.Parse(RX.text), float.Parse(RZ.text), float.Parse(RY.text)); break;
            case "RZ": GameManager.currentPhysicObject.transform.eulerAngles = new Vector3(float.Parse(RX.text), float.Parse(RZ.text), float.Parse(RY.text)); break;

            case "FX": GameManager.currentPhysicObject.FX = float.Parse(FX.text); break;
            case "FY": GameManager.currentPhysicObject.FY = float.Parse(FY.text); break;
            case "FZ": GameManager.currentPhysicObject.FZ = float.Parse(FZ.text); break;

            case "FRX": GameManager.currentPhysicObject.FRX = float.Parse(FRX.text); break;
            case "FRY": GameManager.currentPhysicObject.FRY = float.Parse(FRY.text); break;
            case "FRZ": GameManager.currentPhysicObject.FRZ = float.Parse(FRZ.text); break;

            case "TX": GameManager.currentPhysicObject.TX = float.Parse(TX.text); break;
            case "TY": GameManager.currentPhysicObject.TY = float.Parse(TY.text); break;
            case "TZ": GameManager.currentPhysicObject.TZ = float.Parse(TZ.text); break;

            case "TRX": GameManager.currentPhysicObject.TRX = float.Parse(TRX.text); break;
            case "TRY": GameManager.currentPhysicObject.TRY = float.Parse(TRY.text); break;
            case "TRZ": GameManager.currentPhysicObject.TRZ = float.Parse(TRZ.text); break;

            case "ITX": GameManager.currentPhysicObject.physicObjectRigidbody.inertiaTensor = new Vector3(float.Parse(ITX.text), float.Parse(ITY.text), float.Parse(ITZ.text)); break;
            case "ITY": GameManager.currentPhysicObject.physicObjectRigidbody.inertiaTensor = new Vector3(float.Parse(ITX.text), float.Parse(ITY.text), float.Parse(ITZ.text)); break;
            case "ITZ": GameManager.currentPhysicObject.physicObjectRigidbody.inertiaTensor = new Vector3(float.Parse(ITX.text), float.Parse(ITY.text), float.Parse(ITZ.text)); break;

            case "CMX": GameManager.currentPhysicObject.physicObjectRigidbody.centerOfMass = new Vector3(float.Parse(CMX.text), float.Parse(CMY.text), float.Parse(CMZ.text)); break;
            case "CMY": GameManager.currentPhysicObject.physicObjectRigidbody.centerOfMass = new Vector3(float.Parse(CMX.text), float.Parse(CMY.text), float.Parse(CMZ.text)); break;
            case "CMZ": GameManager.currentPhysicObject.physicObjectRigidbody.centerOfMass = new Vector3(float.Parse(CMX.text), float.Parse(CMY.text), float.Parse(CMZ.text)); break;

            case "Mass": GameManager.currentPhysicObject.Mass = float.Parse(M.text);break;
            case "Electric Charge": GameManager.currentPhysicObject.ElectricCharge = float.Parse(C.text); break;

            case "Static Friction":GameManager.currentPhysicObject.StaticFrictionConstant = float.Parse(SF.text);break;
            case "Dynamic Friction": GameManager.currentPhysicObject.DynamicFrictionConstant = float.Parse(DF.text); break;
            case "Bounciness": GameManager.currentPhysicObject.Bounciness = float.Parse(B.text); break;

            case "Drag":GameManager.currentPhysicObject.physicObjectRigidbody.drag = float.Parse(D.text);break;
            case "Angular Drag": GameManager.currentPhysicObject.physicObjectRigidbody.angularDrag = float.Parse(AD.text); break;

            case "Object Name": GameManager.currentPhysicObject.name = objName.text;break;

        }
    }
    public void Initialization()
    {
        PhysicObject a = GameManager.currentPhysicObject;
        FX.text = a.constantForce.force.x.ToString();
        FY.text = a.constantForce.force.y.ToString();
        FZ.text = a.constantForce.force.z.ToString();

        FRX.text = a.constantForce.relativeForce.x.ToString();
        FRY.text = a.constantForce.relativeForce.y.ToString();
        FRZ.text = a.constantForce.relativeForce.z.ToString();

        TX.text = a.constantForce.torque.x.ToString();
        TY.text = a.constantForce.torque.y.ToString();
        TZ.text = a.constantForce.torque.z.ToString();

        TRX.text = a.constantForce.torque.x.ToString();
        TRY.text = a.constantForce.torque.y.ToString();
        TRZ.text = a.constantForce.torque.z.ToString();

        PX.text = GameManager.currentPhysicObject.transform.position.x.ToString();
        PY.text = GameManager.currentPhysicObject.transform.position.y.ToString();
        PZ.text = GameManager.currentPhysicObject.transform.position.z.ToString();

        M.text = GameManager.currentPhysicObject.Mass.ToString();
        C.text = GameManager.currentPhysicObject.ElectricCharge.ToString();

        SF.text = GameManager.currentPhysicObject.StaticFrictionConstant.ToString();
        DF.text = GameManager.currentPhysicObject.DynamicFrictionConstant.ToString();
        B.text = GameManager.currentPhysicObject.Bounciness.ToString();

        MC.value = returnInd();

        D.text = GameManager.currentPhysicObject.physicObjectRigidbody.drag.ToString();
        AD.text = GameManager.currentPhysicObject.physicObjectRigidbody.angularDrag.ToString();

        FSX.isOn = GameManager.currentPhysicObject.physicObjectRigidbody.constraints.HasFlag(RigidbodyConstraints.FreezePositionX);
        FSY.isOn = GameManager.currentPhysicObject.physicObjectRigidbody.constraints.HasFlag(RigidbodyConstraints.FreezePositionY);
        FSZ.isOn = GameManager.currentPhysicObject.physicObjectRigidbody.constraints.HasFlag(RigidbodyConstraints.FreezePositionZ);
        PRX.isOn = GameManager.currentPhysicObject.physicObjectRigidbody.constraints.HasFlag(RigidbodyConstraints.FreezeRotationX);
        PRY.isOn = GameManager.currentPhysicObject.physicObjectRigidbody.constraints.HasFlag(RigidbodyConstraints.FreezeRotationY);
        PRZ.isOn = GameManager.currentPhysicObject.physicObjectRigidbody.constraints.HasFlag(RigidbodyConstraints.FreezeRotationZ);

    }
    public void ToggleConstraints()
    {
        RigidbodyConstraints rigidbodyConstraints = RigidbodyConstraints.None;
        if(FSX.isOn)
        {
            rigidbodyConstraints |= RigidbodyConstraints.FreezePositionX;
        }
        if (FSY.isOn)
        {
            rigidbodyConstraints |= RigidbodyConstraints.FreezePositionY;
        }
        if (FSZ.isOn)
        {
            rigidbodyConstraints |= RigidbodyConstraints.FreezePositionZ;
        }
        if (PRX.isOn)
        {
            rigidbodyConstraints |= RigidbodyConstraints.FreezeRotationX;
        }
        if (PRY.isOn)
        {
            rigidbodyConstraints |= RigidbodyConstraints.FreezeRotationY;
        }
        if (PRZ.isOn)
        {
            rigidbodyConstraints |= RigidbodyConstraints.FreezeRotationZ;
        }
        GameManager.currentPhysicObject.physicObjectRigidbody.constraints = rigidbodyConstraints;
        
    }
    private int returnInd()
    {
        switch(GameManager.currentPhysicObject.physicMaterialCombine)
        {
            case PhysicMaterialCombine.Maximum:return 0;
            case PhysicMaterialCombine.Minimum: return 1;
            case PhysicMaterialCombine.Average: return 2;
            case PhysicMaterialCombine.Multiply: return 3;
        }
        return 0;
    }
    public void setInd()
    {
        switch(MC.value)
        {
            case 0: GameManager.currentPhysicObject.physicMaterialCombine = PhysicMaterialCombine.Maximum;break;
            case 1: GameManager.currentPhysicObject.physicMaterialCombine = PhysicMaterialCombine.Minimum;break;
            case 2: GameManager.currentPhysicObject.physicMaterialCombine = PhysicMaterialCombine.Average;break;
            case 3: GameManager.currentPhysicObject.physicMaterialCombine = PhysicMaterialCombine.Multiply;break;
        }
    }
    public void resetI()
    {
        GameManager.currentPhysicObject.physicObjectRigidbody.ResetInertiaTensor();
    }
    bool shouldI = false;
    public void ApplyManualForce()
    {
        shouldI = true;
    }
    public void DisableManualForce()
    {
        shouldI = false;
    }

    public void FixedUpdate()
    {
        if (shouldI == true)
        {
            GameManager.currentPhysicObject.physicObjectRigidbody.AddForce(new Vector3(float.Parse(FBX.text), float.Parse(FBY.text), float.Parse(FBZ.text)), returnFMode());
            GameManager.currentPhysicObject.physicObjectRigidbody.AddRelativeForce(new Vector3(float.Parse(FRBX.text), float.Parse(FRBY.text), float.Parse(FRBZ.text)), returnFMode());
        }
    }
    private ForceMode returnFMode()
    {
        switch(FM.value)
        {
            case 0: return ForceMode.Acceleration;
            case 1: return ForceMode.Force;
            case 2: return ForceMode.VelocityChange;
        }
        return ForceMode.Force;
    }
}
