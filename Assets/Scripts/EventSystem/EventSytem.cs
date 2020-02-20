using System;
using UnityEngine;
using UnityEngine.Events;

namespace TPOne.Events
{
    public class EventSystem
    {
        public static UnityAction<int> OnCardClicked = (id) => { };
        public static UnityAction<int> OnCardCanceled = (id) => { };

        public static UnityAction OnSlideCard = () => { };
        public static UnityAction OnSlideOver = () => { };

        // Input events
        public static UnityAction OnTouchBegin = () => { };
        public static UnityAction OnTouchOver = () => { };
        public static UnityAction<Vector2, Vector2> OnDrag = (startPosition, endPosition) => { };
    }
}