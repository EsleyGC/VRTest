using UnityEngine;

[System.Serializable]
public class MovingWallData
{
    public Transform Transform;
    
    public Vector3 ClosePosition;
    public Quaternion CloseRotation;
    
    public Vector3 OpenPosition;
    public Quaternion OpenRotation;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    public Vector3 GetDesiredPosition(bool opening)
    {
        return opening ? OpenPosition : ClosePosition;
    }

    public Quaternion GetDesiredRotation(bool opening)
    {
        return opening ? OpenRotation : CloseRotation;
    }

    public void SetStartPositionAndRotation()
    {
        _startPosition = Transform.localPosition;
        _startRotation = Transform.localRotation;
    }

    public Vector3 GetStartPosition()
    {
        return _startPosition;
    }

    public Quaternion GetStartRotation()
    {
        return _startRotation;
    }
}
