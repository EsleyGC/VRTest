using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager>
{
    #region Fields

    [SerializeField] private List<CheckpointData> checkpoints = new List<CheckpointData>();

    #endregion

    #region Events

    public static Action<Vector3> OnRequestPlayerPositionEvent;
    public static Action<Vector3> OnRequestPlayerRotationEvent;
    public static Action<List<ItemData>> OnRequestInventorySetEvent;

    #endregion

    #region Methods

    public void SaveNewCheckpoint(Transform playerTransform, List<ItemData> itemsToSave)
    {
        var newCheckpoint = new CheckpointData(playerTransform.position, itemsToSave);
        checkpoints.Add(newCheckpoint);
    }

    public void CallLastCheckPoint()
    {
        LoadCheckPoint(GetLastCheckpointData());
    }

    private CheckpointData GetLastCheckpointData()
    {
        if (checkpoints == null || checkpoints.Count < 1)
            return null;

        return checkpoints[^1];
    }

    private void LoadCheckPoint(CheckpointData checkpointData)
    {
        OnRequestPlayerPositionEvent?.Invoke(checkpointData.playerPosition);
        OnRequestInventorySetEvent?.Invoke(checkpointData.inventory);
    }

    #endregion
}
