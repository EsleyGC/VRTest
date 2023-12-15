using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    #region Variables

    [SerializeField] private int requiredItemId;
    [SerializeField] private DropBoxData dropBoxData;

    #endregion
    
    #region Messages

    void Start()
    {
        
    }

    #endregion

    #region Methods

    public void TryOpenBox(Inventory inventory)
    {
        if (!inventory.HasItem(requiredItemId))
            return;
        
        OpenBox();
    }

    private void OpenBox()
    {
        dropBoxData.StartDropBoxEffect();
    }

    #endregion
}
