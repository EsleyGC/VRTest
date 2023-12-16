using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalTextsHandler : MonoBehaviour
{
    #region Fields

    [SerializeField] private ModalText _modalTextPrefab;
    [SerializeField] private Transform _parentToSpawn;
    [SerializeField] private float _delayBetweenModalTexts;
    [SerializeField] private Queue<ModalText> _modalTexts = new Queue<ModalText>();

    private float _lastModalSpawn;
    
    #endregion

    #region Events

    private static Action<string> OnModalTextRequest;

    #endregion

    #region Messages

    private void OnEnable()
    {
        OnModalTextRequest += UseModalText;
    }

    private void OnDisable()
    {
        OnModalTextRequest -= UseModalText;
    }

    #endregion

    #region Methods

    public static void RequestModalText(string text)
    {
        OnModalTextRequest?.Invoke(text);
    }
    
    [ContextMenu("Test modal text")]
    internal void TestModalText()
    {
        UseModalText("Test string");
    }

    internal void ReturnModalText(ModalText modalText)
    {
        _modalTexts.Enqueue(modalText);
    }

    private void UseModalText(string text)
    {
        ModalText modalText;
        if (!_modalTexts.TryDequeue(out modalText))
        {
            modalText = Instantiate(_modalTextPrefab, _parentToSpawn);
            modalText.Init(this);
        }
        modalText.transform.SetAsLastSibling();
        var delayToPlay = Mathf.Max((_lastModalSpawn + _delayBetweenModalTexts) - Time.time, 0);
        modalText.Show(text, delayToPlay);
        _lastModalSpawn = Time.time;
    }

    #endregion
}
