using System.Collections.Generic;
using TPOne.Datas;
using TPOne.Events;
using UnityEngine;

namespace TPOne.CardSelector
{
    public class BombS : MonoBehaviour, ICardSelector
    {
        public CardInfoSO m_infoSO;
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
            if(!CommonModule.UpdateOffset(m_lsBomb, ref m_iBombOffset))
                return;

            var lsIds = Utils.GetIdsByCardNumber(m_infoSO, m_lsBomb[m_iBombOffset]);
            ShowingCardEvents.PopupCard(lsIds.ToArray());
        }

        public void RefreshCard()
        {
            m_lsBomb.Clear();
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