/* 
    create by baihan 2020.02.20 
    CardContainer: 单例容器类，提供数据操作接口 
*/

using System.Collections;
using System.Collections.Generic;
using TPOne.Singleton;
using TPOne.Submodule;
using UnityEngine;

namespace TPOne.Datas
{
    public class CardContainer : MonoSingleton<CardContainer>
    {
        public List<InfoData> m_lsCardDatas = new List<InfoData>();
        public List<Card> m_lsCards = new List<Card>();
        public List<CardCurrentData> m_lsCardRunningData = new List<CardCurrentData>();

        public Card GetCard(int id)
        {
            int index = m_lsCardDatas.FindIndex(data => data.m_iId == id);
            if (index != -1)
            {
                return m_lsCards[index];
            }

            Debug.LogError("Cannot find card with id: " + id);
            return null;
        }

        public InfoData GetCardInfoById(int id)
        {
            return m_lsCardDatas.Find(data => data.m_iId == id);
        }

        public CardCurrentData GetCardCurrentData(int id)
        {
            int index = m_lsCardDatas.FindIndex(data => data.m_iId == id);
            if (index != -1)
            {
                return m_lsCardRunningData[index];
            }

            Debug.LogError("Cannot find card Running with id: " + id);
            return null;
        }
    }
}
