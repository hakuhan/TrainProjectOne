/* 
    create by baihan 2020.02.24 
    选择连对 
*/

using System.Collections.Generic;
using TPOne.Datas;
using TPOne.Events;
using UnityEngine;

namespace TPOne.CardSelector
{
    public class SequencePairS : MonoBehaviour, ICardSelector
    {
        List<E_CardNumber> m_lsSqPair;
        // 连子的数量
        List<int> m_lsSequenceCount;
        int m_iSqPairOffset = -1;
        // 相同类型的数量
        int m_equalCount = 2;

        private void Awake()
        {
            m_lsSequenceCount = new List<int>();
            m_lsSqPair = new List<E_CardNumber>();
        }

        private void OnEnable()
        {
            RefreshEvents.OnCardDataRefreshed += OnCardRefreshed;
        }

        private void OnDisable()
        {
            RefreshEvents.OnCardDataRefreshed -= OnCardRefreshed;
        }

        public void OnCardRefreshed()
        {
            m_lsSqPair.Clear();
            m_lsSequenceCount.Clear();

            var lsCardInfo = CardContainer.Instance.m_lsCardDatas;
            for (int i = 0; i < lsCardInfo.Count; ++i)
            {
                if (lsCardInfo[i].m_eNumber == E_CardNumber.joker)
                {
                    continue;
                }
                int iCount = 0;
                int[] lsCurrentOrder = new int[(int)E_CardNumber.joker];
                for (int j = 0; j < lsCardInfo.Count; ++j)
                {
                    if (lsCardInfo[j].m_eNumber == E_CardNumber.joker || lsCardInfo[j].m_eNumber == E_CardNumber.two)
                    {
                        continue;
                    }
                    int iLargerNumber = Utils.GetLandlordRealOrder(lsCardInfo[j].m_eNumber)
                                        - Utils.GetLandlordRealOrder(lsCardInfo[i].m_eNumber);
                    if (iLargerNumber >= 0)
                    {
                        lsCurrentOrder[iLargerNumber] += 1;
                    }
                }

                for (int k = 0; k < lsCurrentOrder.Length; ++k)
                {
                    if (lsCurrentOrder[k] >= 2)
                    {
                        ++iCount;
                    }
                    else
                    {
                        break;
                    }
                }

                var eCardNumber = lsCardInfo[i].m_eNumber;
                if (iCount >= 3 && !m_lsSqPair.Contains(eCardNumber))
                {
                    m_lsSqPair.Add(eCardNumber);
                    m_lsSequenceCount.Add(iCount);
                }
            }
            m_iSqPairOffset = -1;
        }

        public void SelectCard()
        {
            if (!CommonModule.UpdateOffset(m_lsSqPair, ref m_iSqPairOffset))
            {
                UiEvents.NoneSelectionFond();
                return;
            }

            var lsIds = new List<int>();
            var lsDatas = CardContainer.Instance.m_lsCardDatas;
            for (int i = 0; i < m_lsSequenceCount[m_iSqPairOffset]; ++i)
            {
                var lsNums = lsDatas.FindAll(d => d.m_eNumber == Utils.PlugLandlordNumber(m_lsSqPair[m_iSqPairOffset], i));
                if (lsNums.Count >= m_equalCount)
                {
                    for (int j = 0; j < m_equalCount; ++j)
                    {
                        lsIds.Add(lsNums[j].m_iId);
                    }
                }
            }

            ShowingCardEvents.PopupCard(lsIds.ToArray());
        }
    }

}