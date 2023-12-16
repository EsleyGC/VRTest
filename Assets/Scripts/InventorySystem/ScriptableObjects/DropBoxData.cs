using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DropBoxData : ScriptableObject
{
    public AudioClip openSound;
    public abstract void StartDropBoxEffect(Transform parent);
}