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
            var lsCardRunningData = CardContainer.Instance.m_lsCardRunningData;

            // Find pair
            for (int i = 0; i < lsCards.Count; ++i)
            {
                int iTypeCount = 0;
                for (int j = 0; j < lsCards.Count; ++j)
                {
                    if (lsCardRunningData[j].m_bVisble
                        && lsCards[i].m_info.m_eNumber == lsCards[j].m_info.m_eNumber)
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
            if (!CommonModule.UpdateOffset(m_lsPair, ref m_iPairOffset))
                return;

            var lsCards = CardContainer.Instance.m_lsCards;
            
            var lsIds = Utils.GetIdsByCardNumber(m_infoSO, m_lsPair[m_iPairOffset]);
            ShowingCardEvents.PopupCard(lsIds.ToArray());
        }


    }
}