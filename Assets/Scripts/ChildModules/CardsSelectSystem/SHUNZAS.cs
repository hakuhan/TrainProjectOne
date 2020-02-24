/* 
    create by baihan 2020.02.24 
    选择顺子 
*/

using System.Collections.Generic;
using TPOne.Datas;
using TPOne.Events;
using UnityEngine;

namespace TPOne.CardSelector
{
    public class SHUNZAS : MonoBehaviour, ICardSelector
    {
        List<E_CardNumber> m_lsSHUN;
        List<int> m_lsCount;
        int m_iSHUNOffset = -1;

        private void Awake()
        {
            m_lsSHUN = new List<E_CardNumber>();
            m_lsCount = new List<int>();
        }

        private void OnEnable()
        {
            RefreshEvents.OnCardDataRefreshed += RefreshCard;
        }

        private void OnDisable()
        {
            RefreshEvents.OnCardDataRefreshed -= RefreshCard;
        }


        public void RefreshCard()
        {
            m_lsSHUN.Clear();
            m_lsCount.Clear();
            var lsCardInfo = CardContainer.Instance.m_lsCardDatas;

            // Find pair
            for (int i = 0; i < lsCardInfo.Count; ++i)
            {
                if (lsCardInfo[i].m_eNumber == E_CardNumber.joker)
                {
                    continue;
                }
                bool bShun = false;
                int iCount = 1;
                int[] lslargerIndeses = new int[(int)E_CardNumber.joker];
                for (int j = 0; j < lsCardInfo.Count - 1; ++j)
                {
                    if (lsCardInfo[j].m_eNumber == E_CardNumber.joker)
                    {
                        continue;
                    }
                    int iLargeNum = Utils.GetLandlordRealOrder(lsCardInfo[j].m_eNumber) 
                                    - Utils.GetLandlordRealOrder(lsCardInfo[i].m_eNumber);
                    if (iLargeNum > 0)
                    {
                        lslargerIndeses[iLargeNum] += 1;
                    }
                }

                for (int k = 1; k < lslargerIndeses.Length; ++k)
                {
                    if (lslargerIndeses[k] != 0)
                    {
                        ++iCount;
                    }
                    else
                    {
                        break;
                    }
                }

                if (iCount >= 5)
                {
                    bShun = true;
                }

                var eCardNumber = lsCardInfo[i].m_eNumber;
                if (bShun && !m_lsSHUN.Contains(eCardNumber))
                {
                    m_lsSHUN.Add(eCardNumber);
                    m_lsCount.Add(iCount);
                }

            }
            m_iSHUNOffset = -1;
        }
        public void SelectCard()
        {
            if (!CommonModule.UpdateOffset(m_lsSHUN, ref m_iSHUNOffset))
                return;

            List<int> lsIds = new List<int>();

            var lsDatas = CardContainer.Instance.m_lsCardDatas;

            for (int i = 0; i < m_lsCount[m_iSHUNOffset]; ++i)
            {
                int index = lsDatas.FindIndex(_data => _data.m_eNumber == (m_lsSHUN[m_iSHUNOffset] + i));
                if (index != -1)
                {
                    lsIds.Add(lsDatas[index].m_iId);
                }
            }

            ShowingCardEvents.PopupCard(lsIds.ToArray());
        }
    }

}