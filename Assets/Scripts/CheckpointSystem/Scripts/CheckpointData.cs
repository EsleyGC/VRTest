using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckpointData
{
    public Vector3 playerPosition;
    public List<ItemData> inventory;

    public CheckpointData(Vector3 playerPosition, List<ItemData> inventory)
    {
        this.playerPosition = playerPosition;
        this.inventory = inventory;
    }
}
