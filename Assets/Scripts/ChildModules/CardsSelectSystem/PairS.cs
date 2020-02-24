/* 
    create by baihan 2020.02.24 
    选择对子 
*/

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
            var lsCardRunningData = CardContainer.Instance.m_lsCardRunningData;
            var lsCardInfo = CardContainer.Instance.m_lsCardDatas;

            // Find pair
            for (int i = 0; i < lsCardInfo.Count; ++i)
            {
                int iTypeCount = 0;
                for (int j = 0; j < lsCardInfo.Count; ++j)
                {
                    if (lsCardRunningData[j].m_bVisble
                        && lsCardInfo[i].m_eNumber == lsCardInfo[j].m_eNumber)
                    {
                        ++iTypeCount;
                    }
                }

                var eCardNumber = lsCardInfo[i].m_eNumber;
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