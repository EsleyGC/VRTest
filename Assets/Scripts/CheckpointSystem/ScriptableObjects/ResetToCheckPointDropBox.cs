using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResetToCheckPointDropBox", menuName = "DropBox/ResetToCheckPointDropBox", order = 0)]
public class ResetToCheckPointDropBox : DropBoxData
{
    #region Methods

    public override void StartDropBoxEffect()
    {
        CheckpointManager.Instance.CallLastCheckPoint();
    }

    #endregion
}