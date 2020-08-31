using System;
using System.Collections;
using System.Collections.Generic;
using d4160.Loops;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;
using Cinemachine;

public class Slingshot : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private GameObjectSelector _selector;
    [SerializeField] private MovementMotor _movementMotor;
    [SerializeField] private Transform _anchor;
    [SerializeField] private CinemachineVirtualCamera _main;
    [SerializeField] private CinemachineVirtualCamera _dartFollow;

    [Header("GAME PLAY")]
    [SerializeField] private float _maxLaunchVelocity = 5f;
    [SerializeField] private float _timeToSpawnDart = 3f;

    private Rigidbody _rb;
    private Vector3 _dartFollowCamStartPos;
    private Quaternion _dartFollowCamStartRot;

    void Awake()
    {
        if (!_selector)
            _selector = GetComponent<GameObjectSelector>();

        if (!_movementMotor)
            _movementMotor = GetComponent<MovementMotor>();
    }

    void Start()
    {
        _dartFollowCamStartPos = _dartFollow.transform.position;
        _dartFollowCamStartRot = _dartFollow.transform.rotation;
    }

    void OnEnable()
    {
        UpdateLoop.OnUpdate += OnUpdate;
        _selector.onSelected += OnSelected;
        _selector.onDeselected += OnDeselected;
    }

    void OnDisable()
    {
        UpdateLoop.OnUpdate -= OnUpdate;
        _selector.onSelected -= OnSelected;
        _selector.onDeselected -= OnDeselected;
    }

    private void OnSelected(GameObject obj)
    {
        _movementMotor.TargetToMove = obj.transform;
        _rb = obj.GetComponent<Rigidbody>();

        _rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        _rb.isKinematic = true;
    }

    private void OnDeselected(GameObject obj)
    {
        _rb.isKinematic = false;
        _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        //_rb.useGravity = true;

        _rb.velocity = (_anchor.position - _movementMotor.TargetToMove.position).normalized
                       * _maxLaunchVelocity * (_movementMotor as SlingshotMotor).DistanceRateFromStartPosition;

        _movementMotor.TargetToMove.GetComponent<RotateAsSpeed>().enabled = true;
        _movementMotor.TargetToMove.GetComponent<ConstantForce>().force = Random.insideUnitSphere * 9.8f;

        _movementMotor.TargetToMove = null;

        Invoke("SetPriority", _timeToSpawnDart / 2.77f);

        Invoke("InstantiateDart", _timeToSpawnDart);
    }

    private void SetPriority()
    {
        _dartFollow.Priority = 20;
    }

    private void InstantiateDart()
    {
        var newObj = SingleplayerModeManager.Instance.As<SingleplayerModeManager>().Instantiate();

        _dartFollow.Priority = 0;
        _dartFollow.Follow = newObj.transform;
        _dartFollow.LookAt = newObj.transform;
        _dartFollow.transform.position = _dartFollowCamStartPos;
        _dartFollow.transform.rotation = _dartFollowCamStartRot;
    }

    private void OnUpdate(float deltaTime)
    {
        _selector.CheckSelection();

        _movementMotor.Move();

        LookAtAnchor();
    }

    void LookAtAnchor()
    {
        if(!_anchor || !_movementMotor.TargetToMove)
            return;

        var targetRot = Quaternion.FromToRotation(Vector3.forward, (_anchor.position - _movementMotor.TargetToMove.position).normalized);
        _movementMotor.TargetToMove.GetChild(0).rotation = Quaternion.Slerp(_movementMotor.TargetToMove.GetChild(0).rotation, targetRot, Time.deltaTime * 14f);
    }
}
