using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickScript : MonoBehaviour
{
    public GameEvent gameEvent;

    [SerializeField] private Button _button;

    private void Awake()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        //Debug.Log("button click method called");
        gameEvent.TriggerEvent();
    }
}
