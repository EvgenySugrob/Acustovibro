using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSmoothMovement : MonoBehaviour
{
    [SerializeField]
    protected Transform target;

    [SerializeField]
    protected EasingFunction.Ease _moveEase;

    [SerializeField]
    protected float _smooth = 0.01f;
    private Vector3 _moveTo;
    private float _moveIndex = 1;

    protected void beginTo(Vector3 a_moveTo)
    {
        _moveIndex = 0;
        _moveTo = a_moveTo;
    }

    virtual protected void FixedUpdate()
    {
        if (_moveIndex <= 1)
        {
            target.position = Vector3.Lerp(target.position, _moveTo, EasingFunction.GetEasingFunction(_moveEase)(0, 1, _moveIndex));
            _moveIndex += _smooth;
        }
    }
}
