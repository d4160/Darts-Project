using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Dart : MonoBehaviour
{
    [SerializeField] private float _throwSpeed = 35f;

    private float _speed;
    private float _lastMouseX, _lastMouseY;

    private bool _thrown, _holding, _collided;

    private Rigidbody _rigidbody;
    private Vector3 _newPosition;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Reset();
    }

    void Update()
    {
        if (_holding)
            OnTouch();

        if (_thrown)
        {
            if (!_collided)
                RotateAsSpeed();
            
            return;
        }

        bool pointerDown = false;

#if UNITY_STANDALONE || UNITY_WEBGL
        pointerDown = Input.GetMouseButtonDown(0);
#else
        pointerDown = Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began;
#endif
        if (pointerDown)
        {
            Vector3 pointerPos = default;

#if UNITY_STANDALONE || UNITY_WEBGL
            pointerPos = Input.mousePosition;
#else
            pointerPos = Input.GetTouch(0).position;
#endif

            Ray ray = Camera.main.ScreenPointToRay(pointerPos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                //Debug.Log(hit.transform.name);
                if (hit.transform == transform)
                {
                    _holding = true;
                    transform.SetParent(null);
                }
            }
        }

        bool pointerUp = false;

#if UNITY_STANDALONE || UNITY_WEBGL
        pointerUp = Input.GetMouseButtonUp(0);
#else
        pointerUp = Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended;
#endif

        if (pointerUp)
        {
            Vector3 pointerPos = default;

#if UNITY_STANDALONE || UNITY_WEBGL
            pointerPos = Input.mousePosition;
#else
            pointerPos = Input.GetTouch(0).position;
#endif
            if (_lastMouseY < pointerPos.y)
            {
                ThrowBall(pointerPos);
            }
        }

        bool pointer = false;

#if UNITY_STANDALONE || UNITY_WEBGL
        pointer = Input.GetMouseButton(0);
#else
        pointer = Input.touchCount == 1;
#endif

        if (pointer)
        {
            Vector3 pointerPos = default;

#if UNITY_STANDALONE || UNITY_WEBGL
            pointerPos = Input.mousePosition;
#else
            pointerPos = Input.GetTouch(0).position;
#endif
            _lastMouseX = pointerPos.x;
            _lastMouseY = pointerPos.y;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        _collided = true;
    }

    void RotateAsSpeed()
    {
        var targetRot = Quaternion.FromToRotation(-Vector3.forward, _rigidbody.velocity.normalized);
        transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, targetRot, Time.deltaTime * 14f);
    }

    void Reset()
    {
        CancelInvoke();
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.4f, Camera.main.nearClipPlane * 2.8f));
        _newPosition = transform.position;
        _thrown = _holding = _collided = false;

        if (_rigidbody)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        transform.rotation = Quaternion.Euler(0f, 277f, 0f);
        transform.GetChild(0).localRotation = Quaternion.Euler(0f, 180f, 0f);
        transform.SetParent(Camera.main.transform);
    }

    void OnTouch()
    {
        Vector3 pointerPos = default;

#if UNITY_STANDALONE || UNITY_WEBGL
        pointerPos = Input.mousePosition;
#else
        pointerPos = Input.GetTouch(0).position;
#endif
        pointerPos.z = Camera.main.nearClipPlane * 2.7f;

        _newPosition = Camera.main.ScreenToWorldPoint(pointerPos);

        transform.localPosition = Vector3.Lerp(transform.localPosition, _newPosition, 50f * Time.deltaTime);
    }

    void ThrowBall(Vector2 pointerPos)
    {
        _rigidbody.useGravity = true;

        float differenceY = (pointerPos.y - _lastMouseY) / Screen.height * 100;
        _speed = _throwSpeed * differenceY;

        float x = (pointerPos.x / Screen.width) - (_lastMouseX / Screen.width);

        x = Mathf.Abs(pointerPos.x - _lastMouseX) / Screen.width * 100 * x;

        Vector3 direction = new Vector3(x * 5, 0f, 1f);
        direction = Camera.main.transform.TransformDirection(direction);

        _rigidbody.AddForce((direction * _speed * 1.5f) + (Vector3.up * _speed));

        _holding = false;
        _thrown = true;

        Invoke("Reset", 3f);
    }
}
