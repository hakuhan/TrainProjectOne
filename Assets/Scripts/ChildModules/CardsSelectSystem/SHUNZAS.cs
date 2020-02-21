using System.Collections.Generic;
using TPOne.Datas;
using TPOne.Events;
using UnityEngine;

namespace TPOne.CardSelector
{
    public class SHUNZAS : MonoBehaviour, ICardSelector
    {
        List<E_CardNumber> m_lsSHUN;
        List<int> m_lsCount;
        int m_iSHUNOffset = -1;

        private void Awake()
        {
            m_lsSHUN = new List<E_CardNumber>();
            m_lsCount = new List<int>();
        }

        private void OnEnable()
        {
            RefreshEvents.OnCardDataRefreshed += RefreshCard;
        }

        private void OnDisable()
        {
            RefreshEvents.OnCardDataRefreshed -= RefreshCard;
        }


        public void RefreshCard()
        {
            m_lsSHUN.Clear();
            m_lsCount.Clear();
            var lsCards = CardContainer.Instance.m_lsCards;

            // Find pair
            for (int i = 0; i < lsCards.Count; ++i)
            {
                bool bShun = false;
                int iCount = 0;
                for (int j = 1; j < lsCards.Count - 1; ++j)
                {
                    var index = lsCards.FindIndex
                                    (_c => _c.m_bVisble
                                            && _c.m_info.m_eNumber == (lsCards[i].m_info.m_eNumber + j));
                    if (index != -1)
                    {
                        iCount++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (iCount >= 5)
                {
                    bShun = true;
                }

                var eCardNumber = lsCards[i].m_info.m_eNumber;
                if (bShun && !m_lsSHUN.Contains(eCardNumber))
                {
                    m_lsSHUN.Add(eCardNumber);
                    m_lsCount.Add(iCount);
                }

            }
            m_iSHUNOffset = -1;
        }
        public void SelectCard()
        {
            if (m_lsSHUN == null || m_lsSHUN.Count == 0)
            {
                return;
            }

            var lsCards = CardContainer.Instance.m_lsCards;

            // Check offset
            if (m_iSHUNOffset > m_lsSHUN.Count - 1)
            {
                ++m_iSHUNOffset;
            }
            else
            {
                m_iSHUNOffset = 0;
            }

            // hide all
            foreach (var c in lsCards)
            {
                c.ChangeState(false);
            }

            for (int i = 0; i < m_lsCount[m_iSHUNOffset]; ++i)
            {
                int index = lsCards.FindIndex(_c => _c.m_info.m_eNumber == (m_lsSHUN[m_iSHUNOffset] + i));
                if (index != -1)
                {
                    lsCards[index].ChangeState(true);
                }
            }
        }
    }

}