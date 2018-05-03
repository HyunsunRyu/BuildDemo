using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour
{
    public Text text;

    private static Log instance;

    private void Awake()
    {
        instance = this;
    }

    public static void Set(string data)
    {
        if (instance != null)
            instance.text.text = data;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
