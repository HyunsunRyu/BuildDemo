using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoomer : MonoBehaviour
{
    private float invScreenWidth;

    private Camera cam;

    private float zoomRate;
    private float totalZoomDistance;
    
    public void Init(Camera cam)
    {
        Init(cam, 0f);
    }

    public void Init(Camera cam, float zoomRate)
    {
        Init(cam, zoomRate, 30f);
    }

    public void Init(Camera cam, float zoomRate, float totalZoomDistance)
    {
        this.cam = cam;
        this.zoomRate = zoomRate;
        this.totalZoomDistance = totalZoomDistance;

        invScreenWidth = 1f / (float)Screen.width;
    }

    public void Zoom(float delta)
    {
        if (delta == 0f)
            return;

        delta = delta * invScreenWidth;
        float nextZoomRate = zoomRate + delta;
        nextZoomRate = Mathf.Clamp01(nextZoomRate);

        if (zoomRate == nextZoomRate)
            return;

        Vector3 dir = cam.transform.forward;
        float nextDis = (nextZoomRate - zoomRate) * totalZoomDistance;
        cam.transform.localPosition += dir * nextDis;

        zoomRate = nextZoomRate;
    }
}
