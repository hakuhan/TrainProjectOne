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
        public CardInfoSO m_cardInfoSO;
        
        public void CreateCards()
        {
            // Get random number
            var m_lsCardData = CardContainer.Instance.m_lsCardDatas;
            var m_lsRunningData = CardContainer.Instance.m_lsCardRunningData;
            m_lsCardData.Clear();

            while (m_lsCardData.Count < m_cardInfoSO.m_iCardInHandCount)
            {
                int id = Random.Range(0, m_cardInfoSO.m_iCardTotalCount);
                if (m_lsCardData.FindIndex(c => c.m_iId == id) == -1)
                {
                    m_lsCardData.Add(m_cardInfoSO.m_infos[id]);
                    m_lsRunningData.Add(new CardCurrentData());
                }
            }

            RefreshEvents.RefreshCardDelayWithOpen();
            RefreshEvents.OnCardDataRefreshed();
        }
    }
}
