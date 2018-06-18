using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveGesture : Gesture
{


    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    public override bool Recognize()
    {
        if (touchCount > 1)
            return false;



        throw new System.NotImplementedException();
    }
}
