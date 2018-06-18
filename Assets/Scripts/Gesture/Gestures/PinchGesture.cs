using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.EventSystems
{
    public interface IPinchStartHandler : IEventSystemHandler
    {
        void OnPinchStart(Vector2 point);
    }

    public interface IPinchHandler : IEventSystemHandler
    {
        void OnPinch(Vector2 point, float delta);
    }

    public interface IPinchEndHandler : IEventSystemHandler
    {
        void OnPinchEnd(Vector2 point);
    }

    public class PinchData
    {

    }

    public class PinchGesture : Gesture
    {
        [SerializeField] private float minPinchDistance = 50f;

        private bool bPinch;
        private Vector2 firFingerPos;
        private Vector2 secFingerPos;
        private float distance;
        private float firstDistance;

        public Vector2 centerPos { get; private set; }
        public float delta { get; private set; }
        public float rateDelta { get; private set; }

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

            centerPos = (pointerEventData[0].position + pointerEventData[1].position) * 0.5f;

            firFingerPos = pointerEventData[0].position;
            secFingerPos = pointerEventData[1].position;

            float nowDistance = Vector2.Distance(firFingerPos, secFingerPos);

            delta = nowDistance - distance;
            distance = nowDistance;

            if (!bPinch)
            {
                bPinch = true;
                delta = 0f;
                firstDistance = distance;
                PinchStart();
            }

            Pinch();
            return true;
        }

        private void PinchStart()
        {
            foreach (IPinchStartHandler handler in GestureEventHandler<IPinchStartHandler>.GetHandlers())
            {
                handler.OnPinchStart(centerPos);
            }
        }

        private void Pinch()
        {
            foreach (IPinchHandler handler in GestureEventHandler<IPinchHandler>.GetHandlers())
            {
                handler.OnPinch(centerPos, delta);
            }
        }

        private void PinchEnd()
        {
            foreach (IPinchEndHandler handler in GestureEventHandler<IPinchEndHandler>.GetHandlers())
            {
                handler.OnPinchEnd(centerPos);
            }
        }
    }
}