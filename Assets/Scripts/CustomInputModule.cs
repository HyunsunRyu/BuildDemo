using System;
using UnityEngine;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
    public class TouchEventData
    {
        public int inputCount;
        public PointerEventData[] touchEventData;

        public TouchEventData(int maxTouchCount)
        {
            touchEventData = new PointerEventData[maxTouchCount];

            //PointerEventData data;

            //data.
        }
    }

    public class CustomInputModule : StandaloneInputModule
    {
        [SerializeField]
        [Range(2, 10)]
        private int maxTouchCount = 0;

        private List<IGesture> gestures;

        private TouchEventData touchData;

        private System.Text.StringBuilder builder = new System.Text.StringBuilder();

        protected override void Awake()
        {
            base.Awake();

            touchData = new TouchEventData(maxTouchCount);

            gestures = new List<IGesture>();
        }

        public override void ActivateModule()
        {
            base.ActivateModule();

            SetGestures();
        }

        private void SetGestures()
        {
            gestures.Clear();

            foreach (IGesture gesture in transform.GetComponentsInChildren<IGesture>(true))
            {
                gestures.Add(gesture);

                gesture.Init();
            }

            gestures.Sort((a, b) => a.SortIndex.CompareTo(b.SortIndex));
        }

        public override void DeactivateModule()
        {
            base.DeactivateModule();
        }

        public override void Process()
        {
            base.Process();

            if (Application.isMobilePlatform)
                SetTouchEventData();
            else
                SetMouseEventData();


            //if (RecognizeGestures())
            //    return;


            //base.Process();
            DebugData();
        }

        private void SetMouseEventData()
        {
            bool pressed = input.GetMouseButtonDown(0);
            bool released = input.GetMouseButtonUp(0);
            bool pressing = input.GetMouseButton(0);

            if (pressed || pressing)
            {
                touchData.inputCount = 1;

                PointerEventData pointerData = GetMousePointerEventData().GetButtonState(PointerEventData.InputButton.Left).eventData.buttonData;

                SetPointerEventData(pointerData, pressed, released);
                
                touchData.touchEventData[0] = pointerData;
            }
            else
            {
                touchData.inputCount = 0;
            }
        }

        private void SetTouchEventData()
        {
            touchData.inputCount = input.touchCount;

            for (int i = 0; i < touchData.inputCount; i++)
            {
                //pressed, released 는 첫 동작 그 순간에만 true. 나머지는 false.
                bool pressed, released;
                PointerEventData data = GetTouchPointerEventData(input.GetTouch(i), out pressed, out released);

                //SetPointerEventData(data, pressed, released);

                touchData.touchEventData[i] = data;
            }
        }

        private void SetPointerEventData(PointerEventData data, bool pressed, bool released)
        {
            if (pressed)
            {
                data.eligibleForClick = true;
                data.delta = Vector2.zero;
                data.pressPosition = data.position;
                data.pointerPressRaycast = data.pointerCurrentRaycast;

                //if (data.pointerEnter != data.pointerCurrentRaycast.gameObject)
                //{
                //    data.pointerEnter = data.pointerCurrentRaycast.gameObject;
                //}
                data.pointerEnter = data.pointerCurrentRaycast.gameObject;

                //ExecuteEvents.

                /*
                 * * //IEventSystemEndler
                 * 
                 * public static EventFunction<IBeginDragHandler> beginDragHandler { get; }
                 * public static EventFunction<ICancelHandler> cancelHandler { get; }
                 * public static EventFunction<IDeselectHandler> deselectHandler { get; }
                 * public static EventFunction<IDropHandler> dropHandler { get; }
                 * public static EventFunction<IDragHandler> dragHandler { get; }
                 * public static EventFunction<IEndDragHandler> endDragHandler { get; }
                 * public static EventFunction<IInitializePotentialDragHandler> initializePotentialDrag { get; }
                 * public static EventFunction<IMoveHandler> moveHandler { get; }
                 * public static EventFunction<IPointerClickHandler> pointerClickHandler { get; }
                 * public static EventFunction<IPointerDownHandler> pointerDownHandler { get; }
                 * public static EventFunction<IPointerEnterHandler> pointerEnterHandler { get; }
                 * public static EventFunction<IPointerExitHandler> pointerExitHandler { get; }
                 * public static EventFunction<IPointerUpHandler> pointerUpHandler { get; }
                 * public static EventFunction<IScrollHandler> scrollHandler { get; }
                 * public static EventFunction<ISelectHandler> selectHandler { get; }
                 * public static EventFunction<ISubmitHandler> submitHandler { get; }
                 * public static EventFunction<IUpdateSelectedHandler> updateSelectedHandler { get; }
                 */
            }

            if (released)
            {
            }
        }

        private bool RecognizeGestures()
        {
            foreach (IGesture gesture in gestures)
            {
                if (gesture.Recognize(touchData))
                    return true;
            }
            return false;
        }

        private string GetValue<T>(T obj) where T : Object
        {
            if (obj == null)
                return "null";
            return obj.name;
        }

        private string GetRaycastResult(RaycastResult result)
        {
            if (result.gameObject != null)
                return result.gameObject.name;
            return "null";
        }

        private void DebugData()
        {
            builder.Length = 0;
            bool pressed, released;
            if (input.touchCount > 0)
            {
                GetTouchPointerEventData(input.GetTouch(0), out pressed, out released);
                builder.Append("pressed : " + pressed.ToString() + "\n");
                builder.Append("released : " + released.ToString() + "\n");
            }
            else
            {
                builder.Append("pressed : false no\n");
                builder.Append("released : false no\n");
            }

            return;
            if (touchData.inputCount > 0)
            {
                PointerEventData data = touchData.touchEventData[touchData.inputCount - 1];

                builder.Length = 0;

                //set values auto.
                //builder.Append("enterEventCamera : " + GetValue(data.enterEventCamera) + "\n");
                //builder.Append("pointerId : " + data.pointerId + "\n");
                //builder.Append("pointerCurrentRaycast : " + GetRaycastResult(data.pointerCurrentRaycast) + "\n");
                //builder.Append("position : " + data.position + "\n");

                //set values base.Process.
                //builder.Append("clickCount : " + data.clickCount + "\n");
                //builder.Append("clickTime : " + data.clickTime + "\n");
                //builder.Append("eligibleForClick : " + data.eligibleForClick + "\n");
                //builder.Append("pointerEnter : " + GetValue(data.pointerEnter) + "\n");
                //builder.Append("pointerPressRaycast : " + GetValue(data.pointerPressRaycast.gameObject));
                //builder.Append("pressPosition : " + data.pressPosition + "\n");
                //builder.Append("rawPointerPress : " + GetValue(data.rawPointerPress) + "\n");

                //not.
                //builder.Append("button : " + data.button + "\n");
                //builder.Append("currentInputModule : " + GetValue(data.currentInputModule) + "\n");
                //builder.Append("delta : " + data.delta + "\n");
                //builder.Append("dragging : " + data.dragging + "\n");
                //builder.Append("lastPress : " + GetValue(data.lastPress) + "\n");
                //builder.Append("pointerPress : " + GetValue(data.pointerPress) + "\n");
                //builder.Append("pointerDrag : " + GetValue(data.pointerDrag) + "\n");
                //builder.Append("selectedObject : " + GetValue(data.selectedObject) + "\n");

                //check
                Log.Set(builder.ToString());
            }
        }
    }
}