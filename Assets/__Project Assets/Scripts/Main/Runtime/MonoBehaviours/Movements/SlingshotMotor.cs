using UnityEngine;

public class SlingshotMotor : MovementMotor
{
    [SerializeField] private float _nearClipPlaneMultiplier = 5.9f;
    [SerializeField] private float _forwardSpeed = 0.5f;
    [SerializeField] private float _distanceLimit = 0.25f;

    private float _lastMouseY;
    private float _objectXMovement;
    private Vector3 _startPosition;

    public float DistanceRateFromStartPosition
    {
        get => Vector3.Distance(_targetToMove.position, _startPosition) / _distanceLimit;
    }

    public override Transform TargetToMove
    {
        get => _targetToMove;
        set
        {
            if (!value && _targetToMove)
            {
                _objectXMovement = 0;

                //_targetToMove.position = _startPosition;
            }

            _targetToMove = value;

            if (value)
            {
                _startPosition = value.position;
            }
        }
    }

    public override void Move()
    {
        if (!_targetToMove)
            return;

        Vector3 pointerPos = default;

#if UNITY_STANDALONE || UNITY_WEBGL
        pointerPos = Input.mousePosition;
#else
        pointerPos = Input.GetTouch(0).position;
#endif

        var forward = (_lastMouseY > pointerPos.y) ? -_forwardSpeed : ((_lastMouseY < pointerPos.y) ? _forwardSpeed : 0);

        _lastMouseY = pointerPos.y;

        pointerPos.z = Camera.main.nearClipPlane * _nearClipPlaneMultiplier;

        _data.value = Camera.main.ScreenToWorldPoint(pointerPos);

        _objectXMovement += -forward * Time.deltaTime;

        _data.value.x += _objectXMovement;

        if (Vector3.Distance(_data.value, _startPosition) > _distanceLimit)
        {
            _objectXMovement -= -forward * Time.deltaTime;

            _targetToMove.position = _startPosition + (_data.value - _startPosition).normalized * +_distanceLimit;
        }
        else
        {
            _targetToMove.position = Vector3.Lerp(_targetToMove.position, _data.value, Time.deltaTime * _data.speed);
        }
    }
}
