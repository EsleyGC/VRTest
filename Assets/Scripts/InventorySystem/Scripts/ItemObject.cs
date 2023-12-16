using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour
{
    #region Variables

    public ItemData ItemData;

    [SerializeField] private float moveToPlayerDuration; 
    [SerializeField] private AnimationCurve moveToPlayerDistanceCurve;
    [SerializeField] private AnimationCurve moveToPlayerScaleCurve;
    
    [SerializeField] private TextMeshProUGUI tooltipText;
    
    [SerializeField] private Collider collider;
    [SerializeField] private XRGrabInteractable interactable;

    [SerializeField] private AudioSource audioSource;

    private Vector3 _startScale;
    private IEnumerator _moveToPlayerRoutine;

    #endregion

    #region Events

    public UnityEvent OnItemCollected;

    #endregion

    #region Messages

    private void Start()
    {
        _startScale = transform.localScale;

        if (tooltipText)
        {
            tooltipText.text = ItemData.TooltipText;
            LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipText.rectTransform);
        }
    }

    #endregion

    #region Methods

    public void HandleGetItem(Transform target)
    {
        interactable.enabled = false;
        if (collider.attachedRigidbody)
            collider.attachedRigidbody.isKinematic = true;
        
        collider.enabled = false;
        audioSource.Play();
        MoveToPlayer(target);
    }

    private void MoveToPlayer(Transform target)
    {
        if(_moveToPlayerRoutine != null)
            StopCoroutine(_moveToPlayerRoutine);

        _moveToPlayerRoutine = MoveToPlayerRoutine(target);
        StartCoroutine(_moveToPlayerRoutine);
    }

    private IEnumerator MoveToPlayerRoutine(Transform target)
    {
        var normalizedTime = 0.0f;
        while (normalizedTime < 1)
        {
            normalizedTime += Time.deltaTime / moveToPlayerDuration;
            var positionNormalizedValue = moveToPlayerDistanceCurve.Evaluate(normalizedTime);
            var scaleNormalizedValue = _startScale * moveToPlayerScaleCurve.Evaluate(normalizedTime);
            transform.position = Vector3.LerpUnclamped(transform.position, target.position, positionNormalizedValue);
            transform.localScale = scaleNormalizedValue;
            yield return new WaitForEndOfFrame();
        }
        OnItemCollected?.Invoke();
        DestroySelf();
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    #endregion
}
