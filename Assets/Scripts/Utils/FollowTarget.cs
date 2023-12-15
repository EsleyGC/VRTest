using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    #region Variables

    [SerializeField] private Transform _targetToFollow;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Vector3 _startPositionOffset;
    [SerializeField] private float _smoothSpeed;

    private Vector3 _convertedPosition;

    #endregion

    #region Messages

    private void OnEnable()
    {
        ConvertPosition();
        MoveToStartPosition();
    }

    private void LateUpdate()
    {
        HandleFollow();
    }

    #endregion

    #region Methods

    private void HandleFollow()
    {
        if (!_targetToFollow)
            return;

        var desiredPosition = _targetToFollow.position + _convertedPosition;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * _smoothSpeed);
    }

    private void ConvertPosition()
    {
        var direction = _targetToFollow.forward;
        direction.y = 0;
        _convertedPosition = Quaternion.LookRotation(direction.normalized) * _positionOffset;
    }

    private void MoveToStartPosition()
    {
        transform.position = _targetToFollow.position + _startPositionOffset;
    }

    #endregion
}