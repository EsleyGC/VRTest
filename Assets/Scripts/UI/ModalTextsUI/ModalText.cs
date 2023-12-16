using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ModalText : MonoBehaviour
{
    #region Fields

    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private float defaultDuration;
    
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    
    [SerializeField] private AnimationCurve entranceCurve;
    
    [SerializeField] private float fadeInDuration;
    [SerializeField] private float fadeOutDuration;
    
    [SerializeField] private Transform _transformToAnimate;
    
    [SerializeField] private AudioSource audioSource;
    
    private ModalTextsHandler _modalTextsHandler;
    private IEnumerator _waitToDisableRoutine;
    private IEnumerator _animateStartRoutine;
    private IEnumerator _animateExitRoutine;

    #endregion
    
    #region Methods

    internal void Init(ModalTextsHandler newModalTextHandler)
    {
        _modalTextsHandler = newModalTextHandler;
    }

    internal void Show(string newText, float delayToPlay)
    {
        if(!gameObject.activeSelf)
            gameObject.SetActive(true);

        textField.text = newText;
        LayoutRebuilder.ForceRebuildLayoutImmediate(textField.rectTransform);
        WaitToDisable(defaultDuration);
        AnimateStart(delayToPlay);
    }

    private void AnimateStart(float delayToPlay)
    {
        if(_animateStartRoutine != null)
            StopCoroutine(_animateStartRoutine);

        _animateStartRoutine = AnimateEntranceRoutine(delayToPlay);
        StartCoroutine(_animateStartRoutine);
    }

    private IEnumerator AnimateEntranceRoutine(float delayToPlay)
    {
        var fadeInNormalizedValue = 0.0f;
        yield return new WaitForSeconds(delayToPlay);
        audioSource.Play();
        while (fadeInNormalizedValue < 1)
        {
            fadeInNormalizedValue += Time.deltaTime / fadeInDuration;
            var value = entranceCurve.Evaluate(fadeInNormalizedValue);
            _transformToAnimate.localPosition = Vector3.LerpUnclamped(startPosition, endPosition, value);
            
            yield return null;
        }

        _animateStartRoutine = null;
    }

    private void AnimateExit()
    {
        if(_animateExitRoutine!= null)
            StopCoroutine(_animateExitRoutine);

        _animateExitRoutine = AnimateExitRoutine();
        StartCoroutine(_animateExitRoutine);
    }

    private IEnumerator AnimateExitRoutine()
    {
        yield return new WaitUntil(() => _animateStartRoutine == null);
        var fadeOutNormalizedValue = 0.0f;
        while (fadeOutNormalizedValue < 1)
        {
            fadeOutNormalizedValue += Time.deltaTime / fadeOutDuration;
            _transformToAnimate.localPosition = Vector3.Lerp(endPosition, startPosition, fadeOutNormalizedValue);

            yield return null;
        }

        _animateExitRoutine = null;
        ReturnToPool();
    }

    private void WaitToDisable(float duration)
    {
        if(_waitToDisableRoutine != null)
            StopCoroutine(_waitToDisableRoutine);

        _waitToDisableRoutine = WaitToDisableRoutine(duration);
        StartCoroutine(_waitToDisableRoutine);
    }

    private IEnumerator WaitToDisableRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        AnimateExit();
    }

    private void ReturnToPool()
    {
        if(gameObject.activeSelf)
            gameObject.SetActive(false);

        _modalTextsHandler.ReturnModalText(this);
    }

    #endregion
}
