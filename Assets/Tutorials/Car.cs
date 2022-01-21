using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Rigidbody car;
    public WheelCollider FR, FL, RR, RL;   //Front Right -- Front Left -- Rear Right -- Rear Left
    public WheelHit HFR, HFL, HRR, HRL;
    public float motorToruqe, brakeTorque, internalResistivity, minTurnAngle, maxTurnAngle,currentTurnAngle,maxForwardSpeed,maxBackwardSpeed;
    public float turnAngle;
    public float wheelBase, tread;
    public GameObject a, b;
    private Vector3 circleCenter;
    public float tractionMultyplier;
    public float frontAntiRollValue;
    public float rearAntiRollValue;
    public Transform downForcePosition;
    public float steerDeltaTime;
    public float steerStep;

    public float coefficientOfDrag, coefficientOfLift;
    public float airDensity;
    public float areaDrag, areaLift;

    float frAngle;
    float flAngle;
    float rrAngle;
    float rlAngle;

    float FRR;
    float FLR;
    float RRR;
    float RLR;

    float desiredFRV;
    float desiredFLV;
    float desiredRRV;
    float desiredRLV;

    public float antiRollBar;

    private float forwardVelocity;

    private float frontRightInertia;
    private float frontLeftInertia;
    private float rearRightInertia;
    private float rearLeftInertia;

    private float carVelocity;
    public float tractionTreshold;

    float omega;

    void Start()
    {

        FR.ConfigureVehicleSubsteps(0.1f, 12, 36);
        FL.ConfigureVehicleSubsteps(0.1f, 12, 36);
        RR.ConfigureVehicleSubsteps(0.1f, 12, 36);
        RL.ConfigureVehicleSubsteps(0.1f, 12, 36);

        frontRightInertia = (13f / 250f) * FR.mass * Mathf.Pow(FR.radius, 2);
        frontLeftInertia = (13f / 250f) * FL.mass * Mathf.Pow(FL.radius, 2);
        rearRightInertia = (13f / 250f) * RR.mass * Mathf.Pow(RR.radius, 2);
        rearLeftInertia = (13f / 250f) * RL.mass * Mathf.Pow(RL.radius, 2);

        StartCoroutine("Turn");

    }

    void FixedUpdate()
    {


        car.AddForce(((-car.velocity) / car.velocity.magnitude) * 0.5f * coefficientOfDrag * airDensity * Mathf.Pow(car.velocity.magnitude, 2) * areaDrag);
        car.AddForceAtPosition(-downForcePosition.up * 0.5f * coefficientOfLift * airDensity * Mathf.Pow(car.velocity.magnitude, 2) * areaLift, downForcePosition.position);

        FR.GetGroundHit(out HFR);
        FL.GetGroundHit(out HFL);
        RR.GetGroundHit(out HRR);
        RL.GetGroundHit(out HRL);

        Steer();

        if (Input.GetAxis("Vertical") > 0)
        {

            if (RR.rpm >= 0)
            {

                RR.motorTorque = motorToruqe;
                RL.motorTorque = motorToruqe;
                FR.motorTorque = motorToruqe;
                FL.motorTorque = motorToruqe;

                RR.brakeTorque = 0;
                RL.brakeTorque = 0;
                FR.brakeTorque = 0;
                FL.brakeTorque = 0;

            }
            else
            {

                RR.motorTorque = 0;
                RL.motorTorque = 0;
                FR.motorTorque = 0;
                FL.motorTorque = 0;

                RR.brakeTorque = brakeTorque;
                RL.brakeTorque = brakeTorque;
                FR.brakeTorque = brakeTorque;
                FL.brakeTorque = brakeTorque;

            }

        }
        if (Input.GetAxis("Vertical") < 0)
        {

            if (RR.rpm <= 0)
            {

                RR.motorTorque = -motorToruqe;
                RL.motorTorque = -motorToruqe;
                FR.motorTorque = -motorToruqe;
                FL.motorTorque = -motorToruqe;

                RR.brakeTorque = 0;
                RL.brakeTorque = 0;
                FR.brakeTorque = 0;
                FL.brakeTorque = 0;

            }
            else
            {

                RR.motorTorque = 0;
                RL.motorTorque = 0;
                FR.motorTorque = 0;
                FL.motorTorque = 0;

                RR.brakeTorque = brakeTorque;
                RL.brakeTorque = brakeTorque;
                FR.brakeTorque = brakeTorque;
                FL.brakeTorque = brakeTorque;

            }

        }
        if (Input.GetAxis("Vertical") == 0)
        {

            RR.brakeTorque = internalResistivity;
            RL.brakeTorque = internalResistivity;
            FR.brakeTorque = internalResistivity;
            FL.brakeTorque = internalResistivity;

            RR.motorTorque = 0;
            RL.motorTorque = 0;
            FR.motorTorque = 0;
            FL.motorTorque = 0;


        }

        /*flAngle = Mathf.Abs(FL.steerAngle);
        frAngle = Mathf.Abs(FR.steerAngle);
        rlAngle = Mathf.Abs(RL.steerAngle);
        rrAngle = Mathf.Abs(RR.steerAngle);

        RRR = Mathf.Abs((wheelBase / Mathf.Tan(Mathf.Abs(frAngle) * Mathf.Deg2Rad)));
        RLR = Mathf.Abs((wheelBase / Mathf.Tan(Mathf.Abs(flAngle) * Mathf.Deg2Rad)));

        FRR = Mathf.Abs(RRR / Mathf.Cos(Mathf.Abs(frAngle) * Mathf.Deg2Rad));
        FLR = Mathf.Abs(RLR / Mathf.Cos(Mathf.Abs(flAngle) * Mathf.Deg2Rad));

        desiredFRV = WheelSpeed(FR, HFR);
        desiredFLV = WheelSpeed(FL, HFL);*/

        if (Input.GetAxis("Horizontal") > 0 && car.velocity.magnitude/maxForwardSpeed > tractionTreshold)
        {

            //Steer2();

            omega = desiredFLV / FLR;
            Traction(RL, HRL, ref desiredRLV, WheelSpeed(FL, HFL), rearLeftInertia);
            Traction(FR, HFR, ref desiredFRV, WheelSpeed(RL, HRL), frontRightInertia);
            Traction(RR, HRR, ref desiredRRV, WheelSpeed(FR, HFR), rearRightInertia);

        }
        if (Input.GetAxis("Horizontal") < 0 && car.velocity.magnitude / maxForwardSpeed > tractionTreshold)
        {

            //Steer2();

            omega = desiredFRV / FRR;
            Traction(RR, HRR, ref desiredRRV, WheelSpeed(FR, HFR), rearRightInertia);
            Traction(FL, HFL, ref desiredFLV, WheelSpeed(RR, HRR), frontLeftInertia);
            Traction(RL, HRL, ref desiredRLV, WheelSpeed(FL, HFL), rearLeftInertia);

        }

        float travelL, travelR;

        if (FR.isGrounded)
        {

            travelR = ((FR.transform.position + FR.transform.up * FR.center.y) - HFR.point).magnitude - FR.radius + FR.suspensionDistance * FR.suspensionSpring.targetPosition;
            travelR /= FR.suspensionDistance;
            travelR = 1 - travelR;

        }
        else
        {

            travelR = 0;

        }
        if (FL.isGrounded)
        {

            travelL = ((FL.transform.position + FL.transform.up * FL.center.y) - HFL.point).magnitude - FL.radius + FL.suspensionDistance * FL.suspensionSpring.targetPosition;
            travelL /= FL.suspensionDistance;
            travelL = 1 - travelL;

        }
        else
        {

            travelL = 0;

        }
        if (FR.isGrounded && FL.isGrounded)
        {
            car.AddForceAtPosition(FR.transform.up * travelR * frontAntiRollValue, FR.transform.position);
            car.AddForceAtPosition(FL.transform.up * travelL * frontAntiRollValue, FL.transform.position);
        }



        if (RR.isGrounded)
        {

            travelR = ((RR.transform.position + RR.transform.up * RR.center.y) - HRR.point).magnitude - RR.radius + RR.suspensionDistance * RR.suspensionSpring.targetPosition;
            travelR /= RR.suspensionDistance;
            travelR = 1 - travelR;

        }
        else
        {

            travelR = 0;

        }
        if (RL.isGrounded)
        {

            travelL = ((RL.transform.position + RL.transform.up * RL.center.y) - HRL.point).magnitude - RL.radius + RL.suspensionDistance * RL.suspensionSpring.targetPosition;
            travelL /= RL.suspensionDistance;
            travelL = 1 - travelL;

        }
        else
        {

            travelL = 0;

        }
        if (RR.isGrounded && RL.isGrounded)
        {
            car.AddForceAtPosition(RR.transform.up * travelR * frontAntiRollValue, RR.transform.position);
            car.AddForceAtPosition(RL.transform.up * travelL * frontAntiRollValue, RL.transform.position);
        }

        turnAngle = Mathf.Lerp(minTurnAngle, maxTurnAngle,1 - (car.velocity.magnitude/maxForwardSpeed));

    }

    public void Traction(WheelCollider wheelCollider,WheelHit wheelHit,ref float desiredVelocity,float speedLimit,float inertia)
    {

        desiredVelocity = omega * wheelCollider.radius;

        float differenceVelocity;
        float differenceAngularMomentum;
        float brakeNeeded;
        float forwardVelocity;
        forwardVelocity = ((wheelCollider.rpm / 60f) * 2 * Mathf.PI * wheelCollider.radius) / (wheelHit.forwardSlip + 1);
        if(forwardVelocity < desiredVelocity && wheelHit.forwardSlip > wheelCollider.forwardFriction.extremumSlip)
        {
            desiredVelocity = forwardVelocity;
        }

        if (2 * Mathf.PI * Mathf.Abs(wheelCollider.rpm/60f) * wheelCollider.radius - desiredVelocity > -2f || 2 * Mathf.PI * Mathf.Abs(wheelCollider.rpm/60f) * wheelCollider.radius > speedLimit)
        {

            differenceVelocity = 2 * Mathf.PI * Mathf.Abs(wheelCollider.rpm/60f) * wheelCollider.radius - desiredVelocity;
            differenceAngularMomentum = (differenceVelocity / wheelCollider.radius) * inertia;
            brakeNeeded = differenceAngularMomentum / Time.fixedDeltaTime;
            wheelCollider.brakeTorque = Mathf.Abs(brakeNeeded) * tractionMultyplier;
            wheelCollider.motorTorque = 0;

        }
        else
        {

            wheelCollider.motorTorque = motorToruqe * Input.GetAxis("Vertical");
            wheelCollider.brakeTorque = 0;

        }


    }

    public float WheelSpeed(WheelCollider wheelCollider,WheelHit wheelHit)
    {

        return ((wheelCollider.rpm / 60f) * 2 * Mathf.PI * wheelCollider.radius) / (wheelHit.forwardSlip + 1); 

    }
    float dir;
    IEnumerator Turn()
    {   

        while(true)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                dir = 1;
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                dir = -1;
            }
            if (Input.GetAxis("Horizontal") == 0)
            {
                if (currentTurnAngle != 0)
                {
                    if (currentTurnAngle > 0)
                    {
                        dir = -1;
                    }
                    if (currentTurnAngle < 0)
                    {
                        dir = 1;
                    }
                }
                else
                {
                    dir = 0;
                }
            }

            float j = dir * steerStep;
            currentTurnAngle += j;
            currentTurnAngle = Mathf.Clamp(currentTurnAngle, -turnAngle, turnAngle);
            //print("here");
            yield return new WaitForSeconds(steerDeltaTime);

        }


    }

    private void Steer()
    {

        currentTurnAngle *= Mathf.Abs(Input.GetAxis("Horizontal"));

        FR.steerAngle = currentTurnAngle;
        FL.steerAngle = currentTurnAngle;

        flAngle = Mathf.Abs(FL.steerAngle); //  - Mathf.Abs((HFL.sidewaysSlip * Mathf.Rad2Deg));
        frAngle = Mathf.Abs(FR.steerAngle); //  - Mathf.Abs((HFR.sidewaysSlip * Mathf.Rad2Deg));
        rlAngle = Mathf.Abs(RL.steerAngle); //  - Mathf.Abs((HRL.sidewaysSlip * Mathf.Rad2Deg));
        rrAngle = Mathf.Abs(RR.steerAngle); //  - Mathf.Abs((HRR.sidewaysSlip * Mathf.Rad2Deg));

        RRR = Mathf.Abs((wheelBase / Mathf.Tan(Mathf.Abs(frAngle) * Mathf.Deg2Rad)));
        RLR = Mathf.Abs((wheelBase / Mathf.Tan(Mathf.Abs(flAngle) * Mathf.Deg2Rad)));

        FRR = Mathf.Abs(RRR / Mathf.Cos(Mathf.Abs(frAngle) * Mathf.Deg2Rad));
        FLR = Mathf.Abs(RLR / Mathf.Cos(Mathf.Abs(flAngle) * Mathf.Deg2Rad));



        if (currentTurnAngle < 0)
        {

            circleCenter = a.transform.position + (a.transform.right * Mathf.Abs(FRR) * -1f);
            Vector3 differ = b.transform.position - circleCenter;
            FL.steerAngle = -1f * (Vector3.Angle(differ, RR.transform.right));

            a.transform.localEulerAngles = new Vector3(0, Mathf.Abs(FR.steerAngle) * -1f, 0);
            b.transform.localEulerAngles = new Vector3(0, Mathf.Abs(FL.steerAngle) * -1f, 0);

            flAngle = Mathf.Abs(FL.steerAngle); //  - Mathf.Abs((HFL.sidewaysSlip * Mathf.Rad2Deg));
            frAngle = Mathf.Abs(FR.steerAngle); //  - Mathf.Abs((HFR.sidewaysSlip * Mathf.Rad2Deg));
            rlAngle = Mathf.Abs(RL.steerAngle); //  - Mathf.Abs((HRL.sidewaysSlip * Mathf.Rad2Deg));
            rrAngle = Mathf.Abs(RR.steerAngle); //  - Mathf.Abs((HRR.sidewaysSlip * Mathf.Rad2Deg));

            RRR = Mathf.Abs((wheelBase / Mathf.Tan(Mathf.Abs(frAngle) * Mathf.Deg2Rad)));
            RLR = Mathf.Abs((wheelBase / Mathf.Tan(Mathf.Abs(flAngle) * Mathf.Deg2Rad)));

            FRR = Mathf.Abs(RRR / Mathf.Cos(Mathf.Abs(frAngle) * Mathf.Deg2Rad));
            FLR = Mathf.Abs(RLR / Mathf.Cos(Mathf.Abs(flAngle) * Mathf.Deg2Rad));

            Debug.DrawLine(a.transform.position, a.transform.position + (a.transform.right * Mathf.Abs(FRR) * -1f));
            Debug.DrawLine(b.transform.position, b.transform.position + (b.transform.right * Mathf.Abs(FLR) * -1f));
            Debug.DrawLine(RR.transform.position, RR.transform.position + (RR.transform.right * Mathf.Abs(RRR) * -1f), Color.cyan);
            Debug.DrawLine(RL.transform.position, RL.transform.position + (RL.transform.right * Mathf.Abs(RLR) * -1f), Color.cyan);

        }

        if (currentTurnAngle > 0)
        {

            circleCenter = b.transform.position + (b.transform.right * Mathf.Abs(FLR));
            Vector3 differ = a.transform.position - circleCenter;
            FR.steerAngle = Vector3.Angle(differ, -RR.transform.right);

            a.transform.localEulerAngles = new Vector3(0, Mathf.Abs(FR.steerAngle), 0);
            b.transform.localEulerAngles = new Vector3(0, Mathf.Abs(FL.steerAngle), 0);

            flAngle = Mathf.Abs(FL.steerAngle); //  - Mathf.Abs((HFL.sidewaysSlip * Mathf.Rad2Deg));
            frAngle = Mathf.Abs(FR.steerAngle); //  - Mathf.Abs((HFR.sidewaysSlip * Mathf.Rad2Deg));
            rlAngle = Mathf.Abs(RL.steerAngle); //  - Mathf.Abs((HRL.sidewaysSlip * Mathf.Rad2Deg));
            rrAngle = Mathf.Abs(RR.steerAngle); //  - Mathf.Abs((HRR.sidewaysSlip * Mathf.Rad2Deg));

            RRR = Mathf.Abs((wheelBase / Mathf.Tan(Mathf.Abs(frAngle) * Mathf.Deg2Rad)));
            RLR = Mathf.Abs((wheelBase / Mathf.Tan(Mathf.Abs(flAngle) * Mathf.Deg2Rad)));

            FRR = Mathf.Abs(RRR / Mathf.Cos(Mathf.Abs(frAngle) * Mathf.Deg2Rad));
            FLR = Mathf.Abs(RLR / Mathf.Cos(Mathf.Abs(flAngle) * Mathf.Deg2Rad));

            Debug.DrawLine(a.transform.position, a.transform.position + (a.transform.right * Mathf.Abs(FRR)));
            Debug.DrawLine(b.transform.position, b.transform.position + (b.transform.right * Mathf.Abs(FLR)));
            Debug.DrawLine(RR.transform.position, RR.transform.position + (RR.transform.right * Mathf.Abs(RRR)));
            Debug.DrawLine(RL.transform.position, RL.transform.position + (RL.transform.right * Mathf.Abs(RLR)));

        }


    }
    private void Steer2()
    {

        currentTurnAngle *= Mathf.Abs(Input.GetAxis("Horizontal"));


        //FR.steerAngle = Input.GetAxis("Horizontal") * turnAngle;
        //FL.steerAngle = Input.GetAxis("Horizontal") * turnAngle;

        flAngle = FL.steerAngle - (HFL.sidewaysSlip * Mathf.Rad2Deg);
        frAngle = FR.steerAngle - (HFR.sidewaysSlip * Mathf.Rad2Deg);
        rlAngle = RL.steerAngle - (HRL.sidewaysSlip * Mathf.Rad2Deg);
        rrAngle = RR.steerAngle - (HRR.sidewaysSlip * Mathf.Rad2Deg);

        RRR = Mathf.Abs((wheelBase / Mathf.Tan(Mathf.Abs(frAngle) * Mathf.Deg2Rad)));
        RLR = Mathf.Abs((wheelBase / Mathf.Tan(Mathf.Abs(flAngle) * Mathf.Deg2Rad)));

        FRR = Mathf.Abs(RRR / Mathf.Cos(Mathf.Abs(frAngle) * Mathf.Deg2Rad));
        FLR = Mathf.Abs(RLR / Mathf.Cos(Mathf.Abs(flAngle) * Mathf.Deg2Rad));

        if (Input.GetAxis("Horizontal") < 0)
        {

            a.transform.localEulerAngles = new Vector3(0, Mathf.Abs(frAngle) * -1, 0);
            b.transform.localEulerAngles = new Vector3(0, Mathf.Abs(flAngle) * -1, 0);

            circleCenter = a.transform.position + (a.transform.right * Mathf.Abs(FRR) * -1f);
            Vector3 differ = b.transform.position - circleCenter;
            FL.steerAngle = -1f * (Vector3.Angle(differ, RR.transform.right));

            //Debug.DrawLine(a.transform.position, a.transform.position + (a.transform.right * Mathf.Abs(FRR) * -1f));
            //Debug.DrawLine(b.transform.position, b.transform.position + (b.transform.right * Mathf.Abs(FLR) * -1f));
            //Debug.DrawLine(RR.transform.position, RR.transform.position + (RR.transform.right * Mathf.Abs(RRR) * -1f), Color.cyan);
            //Debug.DrawLine(RL.transform.position, RL.transform.position + (RL.transform.right * Mathf.Abs(RLR) * -1f), Color.cyan);

        }

        if (Input.GetAxis("Horizontal") > 0)
        {

            a.transform.localEulerAngles = new Vector3(0, Mathf.Abs(frAngle), 0);
            b.transform.localEulerAngles = new Vector3(0, Mathf.Abs(flAngle), 0);

            circleCenter = b.transform.position + (b.transform.right * Mathf.Abs(FLR));
            Vector3 differ = a.transform.position - circleCenter;
            FR.steerAngle = Vector3.Angle(differ, -RR.transform.right);

            //Debug.DrawLine(a.transform.position, a.transform.position + (a.transform.right * Mathf.Abs(FRR)));
            //Debug.DrawLine(b.transform.position, b.transform.position + (b.transform.right * Mathf.Abs(FLR)));
            //Debug.DrawLine(RR.transform.position, RR.transform.position + (RR.transform.right * Mathf.Abs(RRR)));
            //Debug.DrawLine(RL.transform.position, RL.transform.position + (RL.transform.right * Mathf.Abs(RLR)));

        }

    }

}