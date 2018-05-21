using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.EventSystems
{
    public class PinchGesture : MonoBehaviour, IGesture
    {
        [SerializeField]
        private int sortIndex;

        int IGesture.SortIndex { get { return sortIndex; } }

        void IGesture.Init()
        {
        }

        bool IGesture.Recognize(TouchEventData touchData)
        {
            if (touchData.inputCount != 2)
                return false;

            //touchData.touchEventData[0]

            return true;
        }
    }
}