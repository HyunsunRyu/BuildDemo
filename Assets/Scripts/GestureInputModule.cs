using System;
using UnityEngine;
using System.Collections.Generic;

//https://gist.github.com/stramit/b654082eb226492d9a04

namespace UnityEngine.EventSystems
{
    public class GestureInputModule : StandaloneInputModule
    {
        [SerializeField]
        [Range(2, 10)]
        private int maxTouchCount = 2;

        private List<Gesture> gestures;

        private System.Text.StringBuilder builder = new System.Text.StringBuilder();

        protected override void Awake()
        {
            base.Awake();

            gestures = new List<Gesture>();
        }

        public override void ActivateModule()
        {
            base.ActivateModule();

            SetGestures();
        }

        private void SetGestures()
        {
            gestures.Clear();

            foreach (Gesture gesture in transform.GetComponentsInChildren<Gesture>(true))
            {
                gestures.Add(gesture);

                gesture.Init();
            }

            gestures.Sort((a, b) => a.GetSortIndex().CompareTo(b.GetSortIndex()));
        }

        public override void DeactivateModule()
        {
            base.DeactivateModule();
        }

        public override void Process()
        {
            //============ Custom IGesture's Module ============/
            //0. 기본 데이터 세팅. 
            if (Application.isMobilePlatform)
                SetTouchEventData();
            else
                SetMouseEventData();

            foreach (Gesture gesture in gestures)
            {
                //1. 추가된 입력받을 IGesture의 데이터를 별도로 세팅한다. //
                gesture.ConvertTouchData();

                //2. 세팅된 데이터를 가지고 각자 제스쳐를 인지한다. //
                if (gesture.Recognize())
                    return;
            }

            //============ Custom IGesture's Module ============/
            base.Process();
        }

        private void SetMouseEventData()
        {
            bool pressed = input.GetMouseButtonDown(0);
            bool released = input.GetMouseButtonUp(0);
            bool pressing = input.GetMouseButton(0);

            if (pressed || pressing || released)
            {
                Gesture.SetTouchCount(1);
                Gesture.SetData(0, pressed, released, GetMousePointerEventData().GetButtonState(PointerEventData.InputButton.Left).eventData.buttonData);
            }
            else
            {
                Gesture.SetTouchCount(0);
            }
        }

        private void SetTouchEventData()
        {
            Gesture.SetTouchCount(input.touchCount);

            for (int i = 0; i < input.touchCount; i++)
            {
                //pressed, released 는 첫 동작 그 순간에만 true. 나머지는 false.
                bool pressed, released;
                PointerEventData data = GetTouchPointerEventData(input.GetTouch(i), out pressed, out released);

                Gesture.SetData(i, pressed, released, data);

                
            }
        }

        private void Test()
        {
            ////IEventSystemEndler

            //public static EventFunction<IBeginDragHandler> beginDragHandler { get; }
            //public static EventFunction<ICancelHandler> cancelHandler { get; }
            //public static EventFunction<IDeselectHandler> deselectHandler { get; }
            //public static EventFunction<IDropHandler> dropHandler { get; }
            //public static EventFunction<IDragHandler> dragHandler { get; }
            //public static EventFunction<IEndDragHandler> endDragHandler { get; }
            //public static EventFunction<IInitializePotentialDragHandler> initializePotentialDrag { get; }
            //public static EventFunction<IMoveHandler> moveHandler { get; }
            //public static EventFunction<IPointerClickHandler> pointerClickHandler { get; }
            //public static EventFunction<IPointerDownHandler> pointerDownHandler { get; }
            //public static EventFunction<IPointerEnterHandler> pointerEnterHandler { get; }
            //public static EventFunction<IPointerExitHandler> pointerExitHandler { get; }
            //public static EventFunction<IPointerUpHandler> pointerUpHandler { get; }
            //public static EventFunction<IScrollHandler> scrollHandler { get; }
            //public static EventFunction<ISelectHandler> selectHandler { get; }
            //public static EventFunction<ISubmitHandler> submitHandler { get; }
            //public static EventFunction<IUpdateSelectedHandler> updateSelectedHandler { get; }

            //Drag
            //IBeginDragHandler
            //IDragHandler
            //IEndDragHandler
            //IInitializePotentialDragHandler
            //IDropHandler

            //Pointer
            //IPointerClickHandler
            //IPointerDownHandler
            //IPointerEnterHandler
            //IPointerExitHandler
            //IPointerUpHandler

            //Select
            //IDeselectHandler
            //ISelectHandler
            //IUpdateSelectedHandler

            //for UI
            //ISubmitHandler
            //ICancelHandler

            //Key
            //IMoveHandler

            //Mouse
            //IScrollHandler
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
            if (input.touchCount > 0)
            {
                //PointerEventData data = touchData[TouchEventData.touchCount - 1].touchEventData;

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