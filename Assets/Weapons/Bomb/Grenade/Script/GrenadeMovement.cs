using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GrenadeMovement : Movement
{
    private Vector3 target;
    private float distanceToTarget;

    private void SetTargetPosition(Vector3 target)
    {
        this.target = target;
        distanceToTarget = (target - transform.position).magnitude;

    }

    private void InterpolateVelocity()
    {
        float interpolationRation = CalculateDistanceLeft() / distanceToTarget;
        Vector2 velocity = Vector2.Lerp(Vector2.zero, rb.velocity, interpolationRation);
        rb.velocity = velocity;
    }

    private float CalculateDistanceLeft()
    {
        float distanceLeft = (target - transform.position).magnitude;
        return distanceLeft;
    }
}
