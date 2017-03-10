using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public void killOrthogonalVelocity()
    {
        var body = GetComponent<Rigidbody2D>();
        var localPoint = transform.TransformPoint(Vector3.zero);
        var localVelocity = body.GetPointVelocity(localPoint);

        var sideAxis = transform.up;
        sideAxis *= Vector2.Dot(localVelocity, sideAxis);
        body.velocity = sideAxis;
    }

    void OnDrawGizmos()
    {
        var body = GetComponent<Rigidbody2D>();
        if (transform == null || body == null)
        {
            return;
        }
        var localPoint = transform.TransformPoint(Vector3.zero);
        var sideAxis = transform.right;
        var frontAxis = transform.up;
        var globalVelocity = body.velocity;
        var localVelocity = body.GetPointVelocity(localPoint);

        // Draw lateral velocity
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector2.Dot(localVelocity, sideAxis) * sideAxis);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector2.Dot(localVelocity, frontAxis) * frontAxis);
    }

    void Start()
    {
    }
}
