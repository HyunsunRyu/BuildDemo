using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.EventSystems
{
    public interface IGesture
    {
        int SortIndex { get; }

        void Init();
        bool Recognize(TouchEventData touchData);
    }
}
