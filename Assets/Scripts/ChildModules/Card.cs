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
using TPOne.Datas;

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
        public float m_fOpenDelay = 1.5f;
        public bool m_bVisble = false;

        public int m_iId;
        public InfoData m_info;
        // 最终的选择状态
        public bool m_bSelected = false;
        bool m_bSelectedTemp = false;

        public bool BSelected
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

        public void UpdateSelectedState(bool bState)
        {
            if (BSelected)
            {
                // m_bSelected = !m_bSelected;
                m_bSelected = bState;
            }
            BSelected = false;

            MoveUpAnimation(m_bSelected);
            if (m_bSelected)
            {
                TPOne.Events.TouchEvents.OnCardClicked(m_iId);
            }
            else
            {
                TPOne.Events.TouchEvents.OnCardCanceled(m_iId);
            }
        }

        public void ChangeState(bool bState)
        {
            MoveUpAnimation(bState);
            if (bState)
            {
                TPOne.Events.TouchEvents.OnCardClicked(m_iId);
            }
            else
            {
                TPOne.Events.TouchEvents.OnCardCanceled(m_iId);
            }
        }

        public void Refresh(CardData data)
        {
            m_iId = data.m_iId;

            var index = m_infoSO.m_infos.FindIndex(info => info.m_iId == m_iId);
            if (index != -1)
            {
                m_info = m_infoSO.m_infos[index];
            }
        }

        public void Show()
        {
            m_bVisble = true;
            gameObject.SetActive(true);
        }

        public void HideCard()
        {
            m_bVisble = false;
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
                if (m_infoSO.m_infos[texIndex].m_eNumber == E_CardNumber.joker)
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

        public void OpenWithAnimation()
        {
            StartCoroutine(OpenSync());
        }

        IEnumerator OpenSync()
        {
            yield return new WaitForSeconds(m_fOpenDelay);

            Open();
        }

        public void Fold()
        {
            m_riBg.texture = m_infoSO.backTex;
            m_riNum.gameObject.SetActive(false);
            m_riFlower1.gameObject.SetActive(false);
            m_riFlower2.gameObject.SetActive(false);
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
