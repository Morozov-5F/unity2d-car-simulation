using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WheelDrive
{
    FrontWheels,
    RearWheels
};

public class CarPhysicsController : MonoBehaviour
{
    public float MaxTorque = 1000;

    public float AirDragRatio = 10;
    public float RoadDragRatio = 300;

    public float AccelerationRatio = 0;
    public float BrakeRatio = 1000;

    public int TurnRatio = 0;
    public float MaxTurnAngle = 45;

    public List<GameObject> FrontWheels;
    public List<GameObject> RearWheels;
    public WheelDrive WheelDrive;

    private Rigidbody2D body;
    public float CurrentTorque;

    private List<GameObject> DrivingWheels
    {
        get
        {
            return (WheelDrive == WheelDrive.FrontWheels) ? FrontWheels : RearWheels;
        }
    }

    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        CurrentTorque = 0;
    }

    void Update ()
    {

    }

    void FixedUpdate()
    {
        // Сопротивление вохдуха
        Vector2 airDrag  = -body.velocity * AirDragRatio * body.velocity.magnitude;
        body.AddForce(airDrag, ForceMode2D.Force);

        foreach(var wheel in FrontWheels)
        {
            var wheelController = wheel.GetComponent<WheelBehaviour>();
            wheelController.Rotate(TurnRatio);
        }

        foreach(var wheel in DrivingWheels)
        {
            var wheelController = wheel.GetComponent<WheelBehaviour>();
            CurrentTorque = Mathf.Lerp(CurrentTorque, MaxTorque * AccelerationRatio, 0.01f);
            wheelController.ApplyTorque(CurrentTorque);
        }
    }
}
