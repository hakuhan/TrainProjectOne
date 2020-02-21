using System;
using UnityEngine;
using UnityEngine.Events;

namespace TPOne.Events
{
    public class TouchEvents
    {
        public static UnityAction<int> OnCardClicked = (id) => { };
        public static UnityAction<int> OnCardCanceled = (id) => { };

        // Input events
        public static UnityAction OnTouchBegin = () => { };
        public static UnityAction OnTouchOver = () => { };
        public static UnityAction<Vector2, Vector2> OnDrag = (startPosition, endPosition) => { };
    }

    public class RefreshEvents
    {
        public static UnityAction RefreshCard = () => { };
        public static UnityAction RefreshCardDelayWithOpen = () => { };
        public static UnityAction RefreshFoldAndOpen = () => { };
        public static UnityAction OnCardDataRefreshed = () => { };
    }

    // public class UiEvents
    // {
    //     public static UnityAction OnClickSendCard = () => { };
    //     public static UnityAction OnClickPlayCard = () => { };
    //     public static UnityAction<bool> OnClickSort = (positive) => { };
    // }
}