using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.EventSystems
{
    public class PinchGesture : MonoBehaviour, IGesture
    {
        [SerializeField]
        private int sortIndex;
        PointerEventData data;
        int IGesture.SortIndex { get { return sortIndex; } }

        void IGesture.Init()
        {
        }

        void IGesture.SetData(TouchEventData[] touchData)
        {
        }

        bool IGesture.Recognize(TouchEventData[] touchData)
        {
            if (TouchEventData.touchCount != 2)
                return false;


            return true;
        }
    }
}