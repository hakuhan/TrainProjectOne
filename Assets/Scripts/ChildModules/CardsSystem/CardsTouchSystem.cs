/* 
    create by baihan 2020.02.21 
    管理卡片触摸操作
*/

using System.Collections.Generic;
using TPOne.Datas;
using UnityEngine;

namespace TPOne.Submodule
{
    public class CardsTouchSystem : MonoBehaviour
    {
        public CardsTouchData m_data;

        #region Life circle

        private void Awake()
        {
            m_data = new CardsTouchData();
        }

        private void OnEnable()
        {
            TPOne.Events.TouchEvents.OnTouchBegin += OnTouchBeginEvent;
            TPOne.Events.TouchEvents.OnTouchOver += OnTouchOverEvent;
            TPOne.Events.TouchEvents.OnDrag += OnDragEvent;
        }

        private void OnDisable()
        {
            TPOne.Events.TouchEvents.OnTouchBegin -= OnTouchBeginEvent;
            TPOne.Events.TouchEvents.OnTouchOver -= OnTouchOverEvent;
            TPOne.Events.TouchEvents.OnDrag -= OnDragEvent;
        }

        #endregion

        #region Input events

        void OnTouchBeginEvent()
        {
            // Get touched card
            var lsCards = CardContainer.Instance.m_lsCards;
            var lsCardRunningData = CardContainer.Instance.m_lsCardRunningData;

            // foreach (var c in lsCards)
            for (int i = 0; i < lsCards.Count; ++i)
            {
                var card = lsCards[i];
                if (Utils.IsPointerTouchGameObjectFirst(Input.mousePosition, card.gameObject))
                {
                    m_data.m_bTouchValid = true;
                    m_data.m_bCurrentSeletState = !lsCardRunningData[i].m_bSelected;
                    card.UpdateTempState(true);
                    lsCardRunningData[i].m_bSelectedTemp = true;
                    break;
                }
            }
        }

        void OnTouchOverEvent()
        {
            var lsCardRunningData = CardContainer.Instance.m_lsCardRunningData;
            var lsCards = CardContainer.Instance.m_lsCards;

            if (!m_data.m_bTouchValid)
            {
                return;
            }

            for (int i = 0; i < lsCards.Count; ++i)
            {
                if (lsCardRunningData[i].m_bSelectedTemp)
                {
                    lsCards[i].ChangeState(m_data.m_bCurrentSeletState);
                    lsCardRunningData[i].m_bSelectedTemp = false;
                }
                lsCards[i].UpdateTempState(false);
            }

            m_data.m_bTouchValid = false;
        }

        /// <summary>
        /// 通过插值检查是否拖动到卡片
        /// </summary>
        /// <param name="v2StartP"></param>
        /// <param name="v2EndP"></param>
        void OnDragEvent(Vector2 v2StartP, Vector2 v2EndP)
        {
            // Vector2 v2StartLocalP;
            // Vector2 v2EndLocalP;
            // RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, v2StartP, Camera.main, out v2StartLocalP);
            // RectTransformUtility.ScreenPointToLocalPointInRectangle (transform.parent as RectTransform, v2EndP, Camera.main, out v2EndLocalP);

            // Rect rtBg = new Rect(
            //     new Vector2(transform.position.x - m_riBg.rectTransform.sizeDelta.x / 2, transform.position.y - m_riBg.rectTransform.sizeDelta.y / 2),
            //     m_riBg.rectTransform.sizeDelta);
            // var bCross = Utils.IsLineIntersectRect(v2StartLocalP, v2EndLocalP, rtBg);
            // BSelected = bCross;

            var lsCards = CardContainer.Instance.m_lsCards;
            var lsCardRunningData = CardContainer.Instance.m_lsCardRunningData;
            if (!m_data.m_bTouchValid || (lsCards != null && lsCards.Count <= 0))
            {
                return;
            }

            // Reset card statue
            foreach (var c in lsCards)
            {
                c.UpdateTempState(false);
            }

            // Check
            var distance = v2StartP - v2EndP;

            var deltaX = lsCards[0].m_riBg.rectTransform.sizeDelta.x / 10f;
            var iTimes = (int)(Mathf.Abs(distance.x) / deltaX);

            for (int i = 0; i < iTimes; ++i)
            {
                var fScreenX = Mathf.Lerp(v2StartP.x, v2EndP.x, i / (float)iTimes);
                var fScreenY = Mathf.Lerp(v2StartP.y, v2EndP.y, i / (float)iTimes);

                for (int j = 0; j < lsCards.Count; ++j)
                {
                    var card = lsCards[j];
                    if (Utils.IsPointerTouchGameObjectFirst(new Vector2(fScreenX, fScreenY), card.gameObject))
                    {
                        card.UpdateTempState(true);
                        lsCardRunningData[j].m_bSelectedTemp = true;
                    }
                }
            }

        }


        #endregion
    }
}