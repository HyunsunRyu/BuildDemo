using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIScrollbar : MonoBehaviour, IPinchHandler, IPinchStartHandler
{
    public Scrollbar scrollbar;
    public Text textValue;

    private void Start()
    {
        scrollbar.onValueChanged.AddListener(ChangedScrollbarValue);

        textValue.text = scrollbar.value.ToString();
    }

    private void ChangedScrollbarValue(float value)
    {
        textValue.text = value.ToString();
    }

    void IPinchStartHandler.OnPinchStart(Vector2 point)
    {
    }

    void IPinchHandler.OnPinch(Vector2 point, float delta)
    {
        textValue.text = delta.ToString();
    }
}
