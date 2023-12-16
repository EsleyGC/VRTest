using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    #region Variables

    [SerializeField] private bool requireItem;
    [SerializeField] private int requiredItemId;
    [SerializeField] private Transform transformRef;
    [SerializeField] private DropBoxData dropBoxData;
    [SerializeField] private AudioSource audioSource;

    #endregion

    #region Methods

    public void TryOpenBox(Inventory inventory)
    {
        if (requireItem)
        {
            if (!inventory.HasItem(requiredItemId))
                return;

            inventory.RemoveItem(requiredItemId);
        }

        OpenBox();
    }

    private void OpenBox()
    {
        audioSource.clip = dropBoxData.openSound;
        audioSource.Play();
        dropBoxData.StartDropBoxEffect(transformRef ? transformRef : transform);
    }

    #endregion
}
