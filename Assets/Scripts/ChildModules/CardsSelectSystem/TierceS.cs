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
                if (iTypeCount == 3 && !m_lsTierce.Contains(eCardNumber))
                {
                    m_lsTierce.Add(eCardNumber);
                }

            }
            m_iTierceOffset = -1;
        }

        public void SelectCard()
        {
            if (!CommonModule.UpdateOffset(m_lsTierce, ref m_iTierceOffset))
                return;

            var lsIds = Utils.GetIdsByCardNumber(m_infoSO, m_lsTierce[m_iTierceOffset]);
            ShowingCardEvents.PopupCard(lsIds.ToArray());
        }

    }
}