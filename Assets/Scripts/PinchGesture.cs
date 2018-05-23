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

    public class PinchGesture : Gesture
    {
        public override void Init()
        {
            //public static EventFunction<IUpdateSelectedHandler> updateSelectedHandler { get; }
            //ExecuteEvents.Execute<IPinchStartHandler>(gameObject, )

        }

        public override void ConvertTouchData()
        {
            if (touchCount != 2)
                return;

            
        }

        public override bool Recognize()
        {
            if (touchCount != 2)
                return false;

            return false;
        }
    }
}