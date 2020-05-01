using d4160.GameFramework;
using UnityEngine;

public abstract class MovementMotor : AuthoringBehaviourBase<MovementU>
{
    [SerializeField] protected Transform _targetToMove;
    [SerializeField] protected Transform _targetToGo;

    public virtual Transform TargetToMove
    {
        get => _targetToMove;
        set => _targetToMove = value;
    }

    public virtual Transform TargetToGo
    {
        get => _targetToGo;
        set => _targetToGo = value;
    }

    public virtual Vector3 MovementValue
    {
        get => _data.value;
        set => _data.value = value;
    }

    public virtual void UpdateValueAsTargetToGo()
    {
        if (_targetToGo)
        {
            _data.value = _targetToGo.position;
        }
    }

    public abstract void Move();
}
