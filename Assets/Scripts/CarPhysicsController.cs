using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysicsController : MonoBehaviour
{
    private float engineSpeed;
    private float steeringAngle;

    public List<WheelController> wheels;
    public float MaxEngineTorque = 0.0001f;

    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10));
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            engineSpeed = MaxEngineTorque;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            engineSpeed = -MaxEngineTorque;
        }
        else
        {
            engineSpeed = 0;
        }
        if (Input.GetKey(KeyCode.A))
        {
            steeringAngle = -30;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            steeringAngle = 30;
        }
        else
        {
            steeringAngle = 0;
        }
    }

    void FixedUpdate()
    {
        JointMotor2D rotateMotor = new JointMotor2D();

        foreach (var cW in wheels)
        {
            cW.killOrthogonalVelocity();
        }

        for (int i = 0; i < 2; i++)
        {
            var direction = wheels[i].transform.up;
            direction *= engineSpeed;

            wheels[i].GetComponent<Rigidbody2D>().AddForce(direction,
                                                           ForceMode2D.Force);

            var mspeed =
               steeringAngle - wheels[i].GetComponent<HingeJoint2D>().jointAngle;
            rotateMotor.motorSpeed = mspeed * 2f;
            Debug.Log(mspeed);
            rotateMotor.maxMotorTorque = 300;
            wheels[i].GetComponent<HingeJoint2D>().motor = rotateMotor;
            wheels[i].GetComponent<HingeJoint2D>().useMotor = true;
        }
    }
}
