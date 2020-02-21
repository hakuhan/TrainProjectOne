using System.Collections.Generic;
using TPOne.Datas;
using TPOne.Events;
using UnityEngine;

namespace TPOne.CardSelector
{
    public class PairS : MonoBehaviour, ICardSelector
    {
        public CardInfoSO m_infoSO;
        List<E_CardNumber> m_lsPair;
        int m_iPairOffset = -1;

        #region life circle
        private void Awake()
        {
            m_lsPair = new List<E_CardNumber>();
        }

        private void OnEnable()
        {
            RefreshEvents.OnCardDataRefreshed += RefreshCard;
        }

        private void OnDisable()
        {
            RefreshEvents.OnCardDataRefreshed -= RefreshCard;
        }

        #endregion

        public void RefreshCard()
        {
            m_lsPair.Clear();
            var lsCards = CardContainer.Instance.m_lsCards;

            // Find pair
            for (int i = 0; i < lsCards.Count; ++i)
            {
                int iTypeCount = 0;
                for (int j = 0; j < lsCards.Count; ++j)
                {
                    if (lsCards[i].m_info.m_eNumber == lsCards[j].m_info.m_eNumber)
                    {
                        ++iTypeCount;
                    }
                }

                var eCardNumber = lsCards[i].m_info.m_eNumber;
                if (iTypeCount == 2 && !m_lsPair.Contains(eCardNumber))
                {
                    m_lsPair.Add(eCardNumber);
                }

            }
            m_iPairOffset = -1;
        }

        public void SelectCard()
        {
            if (m_lsPair == null || m_lsPair.Count == 0)
            {
                return;
            }

            var lsCards = CardContainer.Instance.m_lsCards;

            // Check offset
            if (m_iPairOffset > m_lsPair.Count - 1)
            {
                ++m_iPairOffset;
            }
            else
            {
                m_iPairOffset = 0;
            }

            // hide all
            foreach (var c in lsCards)
            {
                c.ChangeState(false);
            }

            // pop up
            foreach(var c1 in lsCards.FindAll(_c => _c.m_info.m_eNumber == m_lsPair[m_iPairOffset]))
            {
                c1.ChangeState(true);
            }
        }


    }
}