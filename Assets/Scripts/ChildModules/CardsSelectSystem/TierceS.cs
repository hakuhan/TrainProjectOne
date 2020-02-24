/* 
    create by baihan 2020.02.24 
    选择三张 
*/

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
            {
                UiEvents.NoneSelectionFond();
                return;
            }

            var lsIds = Utils.GetIdsByCardNumber(m_infoSO, m_lsTierce[m_iTierceOffset]);
            ShowingCardEvents.PopupCard(lsIds.ToArray());
        }

    }
}