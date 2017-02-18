using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public CarPhysicsController carPhysics;

    void Start ()
    {
        carPhysics = GetComponent<CarPhysicsController>();
    }

    void Update ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            carPhysics.AccelerationRatio = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            carPhysics.AccelerationRatio = -1;
        }
        else
        {
            carPhysics.AccelerationRatio = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            carPhysics.TurnRatio = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            carPhysics.TurnRatio = -1;
        }
        else
        {
            carPhysics.TurnRatio = 0;
        }
    }
}
