using UnityEngine;
using TMPro;
using UnityEngine.Events;
using FMOD.Studio;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class ChangeTextUI : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;

    [SerializeField]
    private TextMeshProUGUI displayTextOff;
    [SerializeField]
    private TextMeshProUGUI displayTextOn;

    private void Awake()
    {
        _toggle.onValueChanged.AddListener(ToggleSwitchOn);

        if (_toggle.isOn)
        {
            ToggleSwitchOn(true);
        }
        else
        {
            ToggleSwitchOn(false);
        }
    }

    private void ToggleSwitchOn(bool _bool)
    {
        displayTextOff.DOFade(_bool ? 0f : 1f, .2f).SetUpdate(true);
        displayTextOn.DOFade(_bool ? 1f : 0f, .2f).SetUpdate(true);
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(ToggleSwitchOn);
    }
}