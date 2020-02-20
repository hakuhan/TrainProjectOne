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

        public void OnEnable()
        {
            TPOne.Events.EventSystem.OnSlideCard += OnSlideCard;
            TPOne.Events.EventSystem.OnSlideOver += OnSlideOver;
        }

        public void OnDisable()
        {
            TPOne.Events.EventSystem.OnSlideCard -= OnSlideCard;
            TPOne.Events.EventSystem.OnSlideOver -= OnSlideOver;
        }

        void OnSlideOver()
        {
            MoveUpAnimation(m_bSelected);

            m_objBlckMask.SetActive(false);
            if (m_bSelected)
            {
                TPOne.Events.EventSystem.OnCardClicked(m_iId);
            }
        }

        void OnSlideCard()
        {
            //判断是否点击UI
            //移动端
            var bClicked = IsPointerOverGameObject(Input.mousePosition);

            if (bClicked && !m_bSelected)
            {
                m_bSelected = true;
                m_objBlckMask.SetActive(true);

                Debug.Log("Click card: " + m_iId);
            }
        }

        private bool IsPointerOverGameObject(Vector2 v2MousePosition)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = v2MousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>();

            EventSystem.current.RaycastAll(eventData, raycastResults);
            if (raycastResults.FindIndex(result => result.gameObject.GetInstanceID() == gameObject.GetInstanceID()) != -1)
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
                    m_riNum.texture = null;
                    m_riFlower1.texture = null;
                    m_riFlower2.texture = null;
                }
                else
                {
                    m_riBg.texture = m_infoSO.whiteTex;
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

        public void OnClick()
        {
            m_bSelected = !m_bSelected;

            if (m_bSelected)
            {
                TPOne.Events.EventSystem.OnCardClicked(m_iId);
            }
            else
            {
                TPOne.Events.EventSystem.OnCardCanceled(m_iId);
            }
            MoveUpAnimation(m_bSelected);
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
