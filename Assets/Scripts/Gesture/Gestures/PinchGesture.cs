using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.EventSystems
{
    public interface IPinchStartGesture : IEventSystemHandler
    {
        void OnPinchStart(PinchGesture gesture);
    }

    public interface IPinchGesture : IEventSystemHandler
    {
        void OnPinch(PinchGesture gesture);
    }

    public interface IPinchEndGesture : IEventSystemHandler
    {
        void OnPinchEnd(PinchGesture gesture);
    }

    public class PinchGesture : Gesture
    {
        //[SerializeField] private float minPinchDistance = 50f;

        private bool bPinch;
        private Vector2 firFingerPos;
        private Vector2 secFingerPos;
        private float distance;

        public Vector2 centerPos { get; private set; }
        public float delta { get; private set; }

        public override void Init()
        {
            bPinch = false;
            centerPos = Vector2.zero;
            firFingerPos = Vector2.zero;
            secFingerPos = Vector2.zero;
            delta = 0f;
            distance = 0f;
        }

        public override bool Recognize()
        {
            if (touchCount != 2)
            {
                if (bPinch)
                {
                    bPinch = false;
                    PinchEnd();

                    Init();
                    return true;
                }
                return false;
            }

            centerPos = (touchPosition[0] + touchPosition[1]) * 0.5f;

            firFingerPos = touchPosition[0];
            secFingerPos = touchPosition[1];

            float nowDistance = Vector2.Distance(firFingerPos, secFingerPos);

            delta = nowDistance - distance;
            distance = nowDistance;

            if (!bPinch)
            {
                bPinch = true;
                delta = 0f;
                PinchStart();
            }

            Pinch();
            return true;
        }

        private void PinchStart()
        {
            foreach (IPinchStartGesture gesture in GestureEventHandler<IPinchStartGesture>.GetHandlers())
            {
                gesture.OnPinchStart(this);
            }
        }

        private void Pinch()
        {
            foreach (IPinchGesture gesture in GestureEventHandler<IPinchGesture>.GetHandlers())
            {
                gesture.OnPinch(this);
            }
        }

        private void PinchEnd()
        {
            foreach (IPinchEndGesture gesture in GestureEventHandler<IPinchEndGesture>.GetHandlers())
            {
                gesture.OnPinchEnd(this);
            }
        }
    }
}