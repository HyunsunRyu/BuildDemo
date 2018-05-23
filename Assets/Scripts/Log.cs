using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Log : MonoBehaviour, IPinchStartHandler, IPinchHandler, IPinchEndHandler
{
    public Text text;

    private static Log instance;

    private void Awake()
    {
        instance = this;

        GestureEventHandler<IPinchStartHandler>.AddHandler(this);
        GestureEventHandler<IPinchHandler>.AddHandler(this);
        GestureEventHandler<IPinchEndHandler>.AddHandler(this);
    }

    public static void Set(string data)
    {
        //if (instance != null)
        //    instance.text.text = data;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnPinchStart(Vector2 point)
    {
        text.text = "Pinch Start : " + point.ToString();
    }

    public void OnPinch(Vector2 point, float delta)
    {
        text.text = "Pinch : " + point.ToString() + ", " + delta.ToString();
    }

    public void OnPinchEnd(Vector2 point)
    {
        text.text = "Pinch End : " + point.ToString();
    }
}
