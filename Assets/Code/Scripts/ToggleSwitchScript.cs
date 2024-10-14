using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

public class ToggleSwitchScript : MonoBehaviour
{
    public UnityEvent eventButtonClickFeedback;
    public BoolEvent _boolEvent;

    [SerializeField] private RectTransform uiHandleRectTransform;
    [SerializeField] private Color backgroundActiveColor;
    [SerializeField] private Color handleActiveColor;

    private Image backgroundImage, handleImage;

    private Color backgroundDefaultColor, handleDefaultColor;

    [SerializeField] private Toggle _toggle;
    private Vector2 handlePosition;

    private bool boolToggleEvent;
    private bool noFeedbackOnStartFlag = true;

    public bool BoolToggleEvent
    {
        get => boolToggleEvent;
        private set
        {
            if (boolToggleEvent != value && noFeedbackOnStartFlag == false) { eventButtonClickFeedback.Invoke(); }
            boolToggleEvent = value;
        }
    }

    private void Awake()
    {
        handlePosition = uiHandleRectTransform.anchoredPosition;

        backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();
        handleImage = uiHandleRectTransform.GetComponent<Image>();


        backgroundDefaultColor = backgroundImage.color;
        handleDefaultColor = handleImage.color;

        _toggle.onValueChanged.AddListener(OnSwitch);

        if (_toggle.isOn)
        {
            OnSwitch(true);
            BoolToggleEvent = true;
        }
        else
        {
            OnSwitch(false);
            BoolToggleEvent = false;
        }

        noFeedbackOnStartFlag = false;
    }

    private void OnSwitch(bool _bool)
    {
        if (_bool)
        {
            BoolToggleEvent = true;
        }
        else
        {
            BoolToggleEvent = false;
        }

        // switch changing color and position
        uiHandleRectTransform.DOAnchorPos(_bool ? handlePosition * -1 : handlePosition, .4f).SetEase(Ease.InOutBack).SetUpdate(true);
        backgroundImage.DOColor(_bool ? backgroundActiveColor : backgroundDefaultColor, .6f).SetUpdate(true);
        handleImage.DOColor(_bool ? handleActiveColor : handleDefaultColor, .4f).SetUpdate(true);

        _boolEvent.Invoke(_bool);
    }

    private void OnDestroy()
    {
        transform.DOKill();
        _toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}