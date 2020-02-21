using System.Collections.Generic;
using TPOne.Datas;
using TPOne.Events;
using UnityEngine;

namespace TPOne.CardSelector
{
    public class BombS : MonoBehaviour, ICardSelector
    {
        List<E_CardNumber> m_lsBomb;
        int m_iBombOffset = -1;

        #region life circle
        private void Awake()
        {
            m_lsBomb = new List<E_CardNumber>();
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
        public void SelectCard()
        {
            if (m_lsBomb == null || m_lsBomb.Count == 0)
            {
                return;
            }

            var lsCards = CardContainer.Instance.m_lsCards;

            // Check offset
            if (m_iBombOffset > m_lsBomb.Count - 1)
            {
                ++m_iBombOffset;
            }
            else
            {
                m_iBombOffset = 0;
            }

            // hide all
            foreach (var c in lsCards)
            {
                c.ChangeState(false);
            }

            // pop up
            foreach (var c1 in lsCards.FindAll(_c => _c.m_info.m_eNumber == m_lsBomb[m_iBombOffset]))
            {
                c1.ChangeState(true);
            }
        }

        public void RefreshCard()
        {
            m_lsBomb.Clear();
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
                if (iTypeCount == 4 && !m_lsBomb.Contains(eCardNumber)
                    || (iTypeCount == 2 && eCardNumber == E_CardNumber.joker))
                {
                    m_lsBomb.Add(eCardNumber);
                }

            }
            m_iBombOffset = -1;
        }
    }
}