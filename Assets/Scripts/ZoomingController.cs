using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ZoomingController : MonoBehaviour, IPinchHandler, IPinchStartHandler
{
    public Scrollbar scrollbar;
    public Camera cam;

    private const float zoomOut = -70f;
    private const float zoomIn = -50f;

    private float invScreenWidth;

    private float rate;

    private void Awake()
    {
        GestureEventHandler<IPinchStartHandler>.AddHandler(this);
        GestureEventHandler<IPinchHandler>.AddHandler(this);

        scrollbar.onValueChanged.AddListener(ChangedScrollbarValue);

        scrollbar.value = 0f;

        invScreenWidth = 1f / (float)Screen.width;
    }

    void IPinchStartHandler.OnPinchStart(Vector2 point)
    {
        rate = scrollbar.value;
    }

    void IPinchHandler.OnPinch(Vector2 point, float delta)
    {
        float value = delta * invScreenWidth;
        rate += value;
        rate = Mathf.Clamp01(rate);

        scrollbar.value = rate;
    }

    private void ChangedScrollbarValue(float value)
    {
        float posZ = Mathf.Lerp(zoomOut, zoomIn, value);
        Vector3 pos = cam.transform.localPosition;
        pos.z = posZ;
        cam.transform.localPosition = pos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
