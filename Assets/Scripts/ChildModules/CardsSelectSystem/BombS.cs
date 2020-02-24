/* 
    create by baihan 2020.02.24 
    选择炸弹
*/

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