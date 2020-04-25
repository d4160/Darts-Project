using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartBoard : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.attachedRigidbody) return;
        
        other.attachedRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        other.attachedRigidbody.isKinematic = true;
    }
}
