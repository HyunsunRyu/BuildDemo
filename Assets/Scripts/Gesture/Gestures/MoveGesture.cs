using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityEngine.EventSystems
{
    public interface IMoveGesture : IEventSystemHandler
    {
        void OnMove(MoveGesture gesture);
    }

    public class MoveGesture : Gesture
    {
        private bool bMove;
        public Vector2 position { get; private set; }
        public Vector2 delta { get; private set; }

        public override void Init()
        {
            position = Vector2.zero;
            bMove = false;
        }

        public override bool Recognize()
        {
            if (touchCount != 1)
                return false;

            bool pressed = Gesture.pressed[0];
            bool released = Gesture.released[0];
            Vector2 nowPos = touchPosition[0];

            delta = position - nowPos;
            position = nowPos;

            if (!bMove && pressed)
            {
                bMove = true;
                delta = Vector2.zero;
            }
            else if (bMove && released)
            {
                bMove = false;
            }
            Move();
            return true;
        }

        private void Move()
        {
            foreach (IMoveGesture handler in GestureEventHandler<IMoveGesture>.GetHandlers())
            {
                handler.OnMove(this);
            }
        }
    }
}