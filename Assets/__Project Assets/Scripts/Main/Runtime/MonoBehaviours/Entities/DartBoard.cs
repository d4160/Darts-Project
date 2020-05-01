using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartBoard : MonoBehaviour
{
    [SerializeField] private Transform _holder;

    void OnCollisionEnter(Collision other)
    {
        if (!other.collider.attachedRigidbody) return;
        
        other.collider.attachedRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        other.collider.attachedRigidbody.isKinematic = true;

        var contactPoint = other.contacts[0].point;
        CalculateAccuracy(contactPoint);
    }

    void CalculateAccuracy(Vector3 contactPoint)
    {
        contactPoint.x = transform.position.x;
        var distance = Vector3.Distance(contactPoint, transform.position);
        var direction = contactPoint - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;

        var answer = -1;
        var pointsValue = CalculatePointsValue(angle);
        var finalPoints = CalculateFinalPointsAndAnswer(distance, pointsValue, out answer);

        SingleplayerModeManager.Instance.As<SingleplayerModeManager>().AddScore(finalPoints);
    }

    int CalculatePointsValue(float angle)
    {
        int val = 0;

        if (angle > -9f && angle <= 9f)
            val = 6;
        else if (angle > 9f && angle <= 27f)
            val = 13;
        else if (angle <= -9f && angle > -27f)
            val = 10;
        else if (angle > 27f && angle <= 45f)
            val = 4;
        else if (angle <= -27f && angle > -45f)
            val = 15;
        else if (angle > 45f && angle <= 63f)
            val = 18;
        else if (angle <= -45f && angle > -63f)
            val = 2;
        else if (angle > 63f && angle <= 81f)
            val = 1;
        else if (angle <= -63f && angle > -81f)
            val = 17;
        else if (angle > 81f && angle <= 99f)
            val = 20;
        else if (angle <= -81f && angle > -99f)
            val = 3;
        else if (angle > 99f && angle <= 117f)
            val = 5;
        else if (angle <= -99f && angle > -117f)
            val = 19;
        else if (angle > 117f && angle <= 135f)
            val = 12;
        else if (angle <= -117f && angle > -135f)
            val = 7;
        else if (angle > 135f && angle <= 153f)
            val = 9;
        else if (angle <= -135f && angle > -153f)
            val = 16;
        else if (angle > 153f && angle <= 171f)
            val = 14;
        else if (angle <= -153f && angle > -171f)
            val = 8;
        else 
            val = 11;

        return val;
    }

    int CalculateFinalPointsAndAnswer(float distance, int val, out int answer)
    {
        int points = 0;
        if (distance <= 0.0234f)
        {
            answer = 1;
            points = 50;
        } else if (distance <= 0.0572f)
        {
            answer = 2;
            points = 25;
        }
        else if (distance <= 0.3552f)
        {
            answer = 3;
            points = val;
        }
        else if (distance <= 0.3928f)
        {
            answer = 4;
            points = 3 * val;
        }
        else if (distance <= 0.5914f)
        {
            answer = 5;
            points = val;
        }
        else if (distance <= 0.6291f)
        {
            answer = 6;
            points = 2 * val;
        }
        else
        {
            answer = 7;
            points = val;
        }

        return points;
    }

    void OnDrawGizmos()
    {
        //if (_holder)
        //{
        //    Debug.DrawLine(transform.position, _holder.position, Color.green);

        //    Debug.Log($"Distance: {Vector3.Distance(transform.position, _holder.position)}");

        //    // Direction = destination - source
        //    var direction = transform.position - _holder.position;
        //    var angle = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;
        //    Debug.Log($"Angle: {angle}");
        //}
    }
}
