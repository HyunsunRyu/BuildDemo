using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour, IMoveGesture, IPinchGesture
{
    public Camera cam;
    public CameraMover camMover;
    public CameraZoomer camZoomer;

    private void Awake()
    {
        GestureEventHandler<IMoveGesture>.AddHandler(this);
        GestureEventHandler<IPinchGesture>.AddHandler(this);
    }

    private void Start()
    {
        camMover.Init(cam);
        camZoomer.Init(cam);
    }

    void IMoveGesture.OnMove(MoveGesture gesture)
    {
        camMover.Move(gesture.delta);
    }

    void IPinchGesture.OnPinch(PinchGesture gesture)
    {
        camZoomer.Zoom(gesture.delta);
    }
}
