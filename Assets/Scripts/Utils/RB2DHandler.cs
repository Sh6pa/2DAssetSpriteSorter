using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RB2DHandler : MonoBehaviour
{
    /// <summary>
    /// Reset GravityScale, Velocity and adds it to simulation if not already simulated
    /// </summary>
    public static void ResetRigidBody(Rigidbody2D rb)
    {
        ResetVelocity(rb);
        Simulate(rb);
    }

    /// <summary>
    /// Resets the chosen rigidbody's velocity 
    /// </summary>
    /// <param name="rb"></param>
    public static void ResetVelocity(Rigidbody2D rb)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
    }

    /// <summary>
    /// Puts the chosen rigidbody into Simulation
    /// </summary>
    public static void Simulate(Rigidbody2D rb)
    {
        rb.simulated = true;
    }


    /// <summary>
    /// Puts the element into simulation if not already and sets the gravity scale factor
    /// </summary>
    /// <param name="rb">RigidBody Of the Chosen element</param>
    /// <param name="GravityScale">The Gravity Scale That needs to be applied</param>
    public static void Gravitate(Rigidbody2D rb, float GravityScale = 1)
    {
        Simulate(rb);
        rb.gravityScale = GravityScale;
    }

}
