using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using d4160.Loops;

public class RotateAsSpeed : MonoBehaviour
{
    private Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        UpdateLoop.OnUpdate += OnUpdate;
    }

    void OnDisable()
    {
        UpdateLoop.OnUpdate -= OnUpdate;
    }

    private void OnUpdate(float deltaTime)
    {
        if (_rb.isKinematic) return;

        var targetRot = Quaternion.FromToRotation(Vector3.forward, _rb.velocity.normalized);
        transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, targetRot, deltaTime * 14f);
    }
}
