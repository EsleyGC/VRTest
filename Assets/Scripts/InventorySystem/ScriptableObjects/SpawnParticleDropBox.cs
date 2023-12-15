using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnParticleDropBox", menuName = "DropBox/SpawnParticleDropBox", order = 0)]
public class SpawnParticleDropBox : DropBoxData
{
    #region Variables

    public ParticleSystem particleToSpawn;
    public string textToShow;

    #endregion

    #region Methods

    public override void StartDropBoxEffect()
    {
        if (particleToSpawn)
            particleToSpawn.Play();
    }

    #endregion
}