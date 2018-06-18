using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.EventSystems
{
    public abstract class Gesture : MonoBehaviour
    {
        private static readonly int maxTouchCount = 5;

        protected static int touchCount;

        protected static PointerEventData[] pointerEventData;
        protected static bool[] pressed;
        protected static bool[] released;

        [SerializeField] protected int sortIndex;

        static Gesture()
        {
            pointerEventData = new PointerEventData[maxTouchCount];
            pressed = new bool[maxTouchCount];
            released = new bool[maxTouchCount];
        }

        public static void SetTouchCount(int count)
        {
            touchCount = Mathf.Min(count, maxTouchCount);
        }

        public static void SetData(int idx, bool pressed, bool released, PointerEventData pointerEventData)
        {
            if (idx >= maxTouchCount)
                return;

            Gesture.pointerEventData[idx] = pointerEventData;
            Gesture.pressed[idx] = pressed;
            Gesture.released[idx] = released;
        }

        public abstract void Init();

        public abstract bool Recognize();

        public int GetSortIndex() { return sortIndex; }
    }

    public static class GestureEventHandler<T> where T : IEventSystemHandler
    {
        private static Dictionary<System.Type, List<T>> handler;

        static GestureEventHandler()
        {
            handler = new Dictionary<System.Type, List<T>>();
        }

        public static List<T> GetHandlers()
        {
            System.Type type = typeof(T);
            if (!handler.ContainsKey(type))
                handler.Add(type, new List<T>());
            return handler[type];
        }

        public static void AddHandler(T eventHandler)
        {
            var handler = GetHandlers();

            if (!handler.Contains(eventHandler))
                handler.Add(eventHandler);
        }

        public static void RemoveHandler(T eventHandler)
        {
            var handler = GetHandlers();

            if (handler.Contains(eventHandler))
                handler.Remove(eventHandler);
        }
    }
}