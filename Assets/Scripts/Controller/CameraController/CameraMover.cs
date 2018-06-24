using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraMover : MonoBehaviour
{
    private Camera cam;
    private float invScreenWidth;
    private float targetHeight;

    public void Init(Camera cam)
    {
        Init(cam, 0f);
    }

    public void Init(Camera cam, float targetHeight)
    {
        this.targetHeight = targetHeight;
        this.cam = cam;
        invScreenWidth = 1f / (float)Screen.width;
    }

    public void Move(Vector2 dir)
    {
        float speed = cam.transform.position.y - targetHeight;

        dir = dir * invScreenWidth * speed;
        Vector3 x = cam.transform.right * dir.x;
        Vector3 y = Vector3.Cross(cam.transform.right, Vector3.up).normalized * dir.y;
        cam.transform.localPosition += x + y;
    }
}
