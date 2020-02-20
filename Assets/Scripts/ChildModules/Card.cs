/* 
    create by baihan 2020.02.20 
    Card脚本
    1. 提供card隐藏显示的行为
    2. 提供card播放动画的行为
    3. card点击相应进行分发
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace TPOne.Submodule
{
    public class Card : MonoBehaviour
    {
        public CardInfoSO m_infoSO;
        public RawImage m_riBg;
        public RawImage m_riNum;
        public RawImage m_riFlower1;
        public RawImage m_riFlower2;
        public GameObject m_objBlckMask;

        public int m_iId;
        public bool m_bSelected = false;
        public bool m_bSelectedTemp = false;
        bool m_bFirstTouch = false;

        bool BSelected
        {
            set
            {
                m_bSelectedTemp = value;
                m_objBlckMask.SetActive(value);
            }

            get
            {
                return m_bSelectedTemp;
            }
        }

        public void OnEnable()
        {
            TPOne.Events.EventSystem.OnTouchBegin += OnTouchBeginEvent;
            TPOne.Events.EventSystem.OnTouchOver += OnTouchOverEvent;
            TPOne.Events.EventSystem.OnDrag += OnDragEvent;
        }

        public void OnDisable()
        {
            TPOne.Events.EventSystem.OnTouchBegin -= OnTouchBeginEvent;
            TPOne.Events.EventSystem.OnTouchOver -= OnTouchOverEvent;
            TPOne.Events.EventSystem.OnDrag -= OnDragEvent;
        }

        #region event
        void OnTouchBeginEvent()
        {
            DoTouchEvent(Input.mousePosition);
        }

        void OnTouchOverEvent()
        {
            m_bFirstTouch = false;

            if (BSelected)
            {
                m_bSelected = !m_bSelected;
            }
            BSelected = false;

            MoveUpAnimation(m_bSelected);
            if (m_bSelected)
            {
                TPOne.Events.EventSystem.OnCardClicked(m_iId);
            }
            else
            {
                TPOne.Events.EventSystem.OnCardCanceled(m_iId);
            }
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

            var distance = v2StartP - v2EndP;

            var deltaX = m_riBg.rectTransform.sizeDelta.x / 10f;
            var iTimes = (int)(Mathf.Abs(distance.x) / deltaX);

            BSelected = false;
            DoTouchEvent(v2StartP);
            DoTouchEvent(v2EndP);

            for (int i = 0; i < iTimes; ++i)
            {
                var fScreenX = Mathf.Lerp(v2StartP.x, v2EndP.x, i / (float)iTimes);
                var fScreenY = Mathf.Lerp(v2StartP.y, v2EndP.y, i / (float)iTimes);

                DoTouchEvent(new Vector2(fScreenX, fScreenY));
                if (BSelected)
                {
                    break;
                }
            }

        }

        #endregion

        void DoTouchEvent(Vector2 v2CcreenPosition)
        {
            if (IsPointerOverGameObject(v2CcreenPosition))
            {
                BSelected = true;
            }
        }

        private bool IsPointerOverGameObject(Vector2 v2MousePosition)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = v2MousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>();

            EventSystem.current.RaycastAll(eventData, raycastResults);
            if (raycastResults[0].gameObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                return true;
            }

            return false;
        }

        public void Refresh(CardData data)
        {
            m_iId = data.m_iId;

            gameObject.SetActive(true);

            Open();
        }

        public void HideCard()
        {
            gameObject.SetActive(false);
            MoveUpAnimation(false);
        }

        public void Open()
        {
            UpdateMainImage();
        }

        bool UpdateMainImage()
        {
            bool bOk = false;
            int texIndex = m_infoSO.m_infos.FindIndex((_cData) => _cData.m_iId == m_iId);
            if (texIndex != -1)
            {
                bOk = true;
                if (m_infoSO.m_infos[texIndex].m_bJoker)
                {
                    m_riBg.texture = m_infoSO.m_infos[texIndex].m_t2Num;
                    m_riNum.gameObject.SetActive(false);
                    m_riFlower1.gameObject.SetActive(false);
                    m_riFlower2.gameObject.SetActive(false);
                }
                else
                {
                    m_riBg.texture = m_infoSO.whiteTex;
                    m_riNum.gameObject.SetActive(true);
                    m_riFlower1.gameObject.SetActive(true);
                    m_riFlower2.gameObject.SetActive(true);

                    m_riNum.texture = m_infoSO.m_infos[texIndex].m_t2Num;
                    m_riFlower1.texture = m_infoSO.m_infos[texIndex].m_t2Flower1;
                    m_riFlower2.texture = m_infoSO.m_infos[texIndex].m_t2Flower2;
                }
                m_objBlckMask.SetActive(false);
                m_bSelected = false;
            }

            return bOk;
        }

        public void Fold()
        {
            m_riBg.texture = m_infoSO.backTex;
            m_riNum.texture = null;
            m_riFlower1.texture = null;
            m_riFlower2.texture = null;
        }

        void MoveUpAnimation(bool isMoveUp)
        {
            if (isMoveUp)
                m_riBg.transform.DOLocalMoveY(50, 0.1f).SetLink(gameObject).SetLink(gameObject);
            else
                m_riBg.transform.DOLocalMoveY(0, 0.1f).SetLink(gameObject).SetLink(gameObject);

        }
    }
}
