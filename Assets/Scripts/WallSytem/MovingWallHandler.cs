using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallHandler : MonoBehaviour
{
    #region Fields

    [SerializeField] private bool _isOpen;
    [SerializeField] private float _openDuration;
    [SerializeField] private float _closeDuration;
    [SerializeField] private MovingWallData[] _wall;

    private IEnumerator _changeWallStateRoutine;

    #endregion

    #region Methods

    [ContextMenu("OpenWall")]
    public void OpenWall()
    {
        ChangeWallState(true);
    }

    [ContextMenu("CloseWall")]
    public void CloseWall()
    {
        ChangeWallState(false);
    }

    public void ChangeWallState(bool open)
    {
        if (open == _isOpen)
            return;

        if (_wall == null)
            return;

        _isOpen = open;
        
        if (_changeWallStateRoutine != null)
            StopCoroutine(_changeWallStateRoutine);

        _changeWallStateRoutine = ChangeWallStateRoutine(open);
        StartCoroutine(_changeWallStateRoutine);
    }

    private IEnumerator ChangeWallStateRoutine(bool open)
    {
        var duration = open ? _openDuration : _closeDuration;
        var normalizedTime = 0.0f;
        
        RegisterCurrentWallPositions();
        
        while (normalizedTime < 1)
        {
            normalizedTime += Time.deltaTime / duration;
            ChangeWallPosition(open, normalizedTime);
            yield return null;
        }

        _changeWallStateRoutine = null;
    }

    private void ChangeWallPosition(bool open, float normalizedTime)
    {
        foreach (var wall in _wall)
        {
            var startPosition = wall.GetStartPosition();
            var targetPosition = open ? wall.OpenPosition : wall.ClosePosition;
            var newPosition = Vector3.Lerp(startPosition, targetPosition, normalizedTime);
            
            var startRotation = wall.GetStartRotation();
            var targetRotation = open ? wall.OpenRotation : wall.CloseRotation;
            var newRotation = Quaternion.Lerp(startRotation, targetRotation, normalizedTime);
            
            wall.Transform.SetLocalPositionAndRotation(newPosition, newRotation);
        }
    }
    
    private void RegisterCurrentWallPositions()
    {
        foreach (var wall in _wall)
        {
            wall.SetStartPositionAndRotation();
        }
    }

    #endregion
}
