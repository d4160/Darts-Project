using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartBoard : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        Debug.Log($"Collide with: {other.gameObject.name}");

        if (!other.collider.attachedRigidbody) return;
        
        other.collider.attachedRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        other.collider.attachedRigidbody.isKinematic = true;
    }
}
