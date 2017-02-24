using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelBehaviour : MonoBehaviour
{
    public float LinearDrag = 300;
    public float SkidRatio  = 2;

    private Rigidbody2D body;

    private Vector2 ForwardVelocity
    {
        get
        {
            var forwardNormal = transform.up;
            return Vector2.Dot(forwardNormal, body.velocity) * forwardNormal;
        }
    }

    private Vector2 LateralVelocity
    {
        get
        {
            var sideNormal = transform.right;
            return Vector2.Dot(sideNormal, body.velocity) * sideNormal;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            ApplyTorque(10);
        }
        else if (Input.GetKey(KeyCode.S))
        {
             ApplyTorque(-10);
        }
        else
        {
             ApplyTorque(0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Rotate(1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rotate(-1);
        }
        else
        {
            Rotate(0);
        }
    }

    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        body.AddForce(transform.right * 50 + transform.up * 100);
    }

    public void UpdateFriction()
    {
        var impulse = body.mass * -LateralVelocity;
        // if (impulse.magnitude > SkidRatio)
        // {
        //     impulse *= SkidRatio / impulse.magnitude;
        // }
        Debug.Log("Before: " + gameObject.name + LateralVelocity);
        body.AddForce(impulse, ForceMode2D.Impulse);
        Debug.Log("After: " + gameObject.name + LateralVelocity);
        // Убиваем вращение
        body.AddTorque(0.1f * body.inertia * -body.angularVelocity);
        // Применяем трение
        body.AddForce(-LinearDrag * ForwardVelocity, ForceMode2D.Force);
    }

    void OnDrawGizmos()
    {
        Vector3 red = LateralVelocity;
        Vector3 green = ForwardVelocity;
        Gizmos.color = Color.red;
        // Gizmos.DrawLine(transform.position, transform.position + red);
        Gizmos.color = Color.green;
        // Gizmos.DrawLine(transform.position, transform.position + green);
    }

    public void ApplyTorque(float torque)
    {
        body.AddForce(transform.up * torque, ForceMode2D.Force);
    }

    public void Rotate(int direction)
    {
        float torqueValue = direction * 0.1f;
        body.AddTorque(torqueValue);
    }
}
