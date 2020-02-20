using System;
using UnityEngine.Events;

namespace TPOne.Events
{
    public class EventSystem
    {
        public static UnityAction<int> OnCardClicked = (id) => { };
        public static UnityAction<int> OnCardCanceled = (id) => { };

        public static UnityAction OnSlideCard = () => { };
        public static UnityAction OnSlideOver = () => { };
    }
}