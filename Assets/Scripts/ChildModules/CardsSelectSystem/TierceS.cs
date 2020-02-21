using System.Collections.Generic;
using TPOne.Datas;
using TPOne.Events;
using UnityEngine;

namespace TPOne.CardSelector
{
    public class TierceS : MonoBehaviour, ICardSelector
    {
        public CardInfoSO m_infoSO;
        List<E_CardNumber> m_lsTierce;
        int m_iTierceOffset = -1;

        #region life circle
        private void Awake()
        {
            m_lsTierce = new List<E_CardNumber>();
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
            m_lsTierce.Clear();
            var lsCards = CardContainer.Instance.m_lsCards;

            // Find pair
            for (int i = 0; i < lsCards.Count; ++i)
            {
                int iTypeCount = 0;
                for (int j = 0; j < lsCards.Count; ++j)
                {
                    if (lsCards[j].m_bVisble
                        && lsCards[i].m_info.m_eNumber == lsCards[j].m_info.m_eNumber)
                    {
                        ++iTypeCount;
                    }
                }

                var eCardNumber = lsCards[i].m_info.m_eNumber;
                if (iTypeCount == 3 && !m_lsTierce.Contains(eCardNumber))
                {
                    m_lsTierce.Add(eCardNumber);
                }

            }
            m_iTierceOffset = -1;
        }

        public void SelectCard()
        {
            if (m_lsTierce == null || m_lsTierce.Count == 0)
            {
                return;
            }

            var lsCards = CardContainer.Instance.m_lsCards;

            // Check offset
            if (m_iTierceOffset < m_lsTierce.Count - 1)
            {
                ++m_iTierceOffset;
            }
            else
            {
                m_iTierceOffset = 0;
            }

            // hide all
            foreach (var c in lsCards)
            {
                c.ChangeState(false);
            }

            // pop up
            foreach (var c1 in lsCards.FindAll(_c => _c.m_info.m_eNumber == m_lsTierce[m_iTierceOffset]))
            {
                c1.ChangeState(true);
            }
        }

    }
}