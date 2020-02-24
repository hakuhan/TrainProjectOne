/* 
    create by baihan 2020.02.20 
    NomalCardCreater类: 负责创建卡牌 
*/

using System.Collections;
using System.Collections.Generic;
using TPOne.Datas;
using TPOne.Events;
using UnityEngine;

namespace TPOne.Submodule
{
    public class NomalCardCreater : MonoBehaviour, ICardCreater
    {
        class CreaterData
        {
            public List<int> lsId;

            public CreaterData(int iCount)
            {
                lsId = new List<int>();
                for (int i = 0; i < iCount; ++i)
                {
                    lsId.Add(i);
                }
            }
        }

        public CardInfoSO m_cardInfoSO;
        CreaterData m_data;

        private void Awake()
        {
            m_data = new CreaterData(m_cardInfoSO.m_iCardTotalCount);

            ShuffleRandomCards(10, m_data.lsId);
        }

        public void CreateCards()
        {
            // Refresh card data

            // Update card data
            var m_lsCardData = CardContainer.Instance.m_lsCardDatas;
            var m_lsRunningData = CardContainer.Instance.m_lsCardRunningData;
            m_lsCardData.Clear();

            ShuffleCards(Random.Range(0, 20), m_data.lsId);
            for (int i = 0; i < m_cardInfoSO.m_iCardInHandCount; ++i)
            {
                int iId = m_data.lsId[i];
                m_lsCardData.Add(m_cardInfoSO.m_infos[iId]);
                m_lsRunningData.Add(new CardCurrentData());
            }

            RefreshEvents.RefreshCardDelayWithOpen();
            RefreshEvents.OnCardDataRefreshed();
        }

        /// <summary>
        /// 采取对半洗牌
        /// </summary>
        /// <param name="iTimes"></param>
        void ShuffleCards(int iTimes, List<int> lsIds)
        {
            var lsTemp = new List<int>();
            for (int i = 0; i < iTimes; ++i)
            {
                lsTemp.Clear();
                int iIndex = 0;
                int iRightIndex = Random.Range(0, lsIds.Count);

                while (true)
                {
                    bool bLeftOver = iIndex >= iRightIndex;
                    bool bRightOver = iRightIndex + iIndex >= lsIds.Count;
                    if (bLeftOver)
                    {
                        // Add rest of right 
                        lsTemp.AddRange(lsIds.GetRange(iRightIndex + iIndex, lsIds.Count - iIndex * 2));
                        break;
                    }
                    else if (bRightOver)
                    {
                        // Add rest of left
                        lsTemp.AddRange(lsIds.GetRange(iIndex, iRightIndex - iIndex));
                        break;
                    }
                    else
                    {
                        lsTemp.Add(lsIds[iIndex]);
                        lsTemp.Add(lsIds[iRightIndex + iIndex]);
                    }
                    ++iIndex;
                }

                lsIds.Clear();
                lsIds.AddRange(lsTemp);
            }
        }

        /// <summary>
        /// 随机洗牌
        /// </summary>
        /// <param name="iTimes"></param>
        /// <param name="lsIds"></param>
        void ShuffleRandomCards(int iTimes, List<int> lsIds)
        {
            for (int i = 0; i < iTimes; ++i)
            {
                int id1 = Random.Range(0, lsIds.Count);
                int id2 = Random.Range(0, lsIds.Count);

                if (id1 != id2)
                {
                    int iTemp = lsIds[id1];
                    lsIds[id1] = lsIds[id2];
                    lsIds[id2] = iTemp;
                }
            }
        }
    }
}
