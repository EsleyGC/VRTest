using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnParticleDropBox", menuName = "DropBox/SpawnParticleDropBox", order = 0)]
public class SpawnParticleDropBox : DropBoxData
{
    #region Variables

    public GameObject particleToSpawn;
    public string textToShow;

    #endregion

    #region Methods

    public override void StartDropBoxEffect(Transform parentTransform)
    {
        if (!particleToSpawn)
            return;
        
        //TODO: Create a pooling system for the particles.
        
        Instantiate(particleToSpawn, parentTransform);
        
        ModalTextsHandler.RequestModalText(textToShow);
    }

    #endregion
}