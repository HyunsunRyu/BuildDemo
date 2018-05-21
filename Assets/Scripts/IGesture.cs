using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.EventSystems
{
    public interface IGesture
    {
        int SortIndex { get; }

        void Init();

        void SetData(TouchEventData[] touchData);

        bool Recognize(TouchEventData[] touchData);
    }
}
