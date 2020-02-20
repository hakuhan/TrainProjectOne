/* 
    create by baihan 2020.02.20 
    NomalCardCreater类: 负责创建卡牌 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPOne.Submodule
{
    public class NomalCardCreater : MonoBehaviour, ICardCreater
    {
        public CardInfoSO m_cardInfoSO;
        public int m_iCardCount = 13;
        List<CardData> m_lsCreateList = new List<CardData>();
        public void CreateCards()
        {
            // Get random number
            m_lsCreateList.Clear();
            while (m_lsCreateList.Count < m_iCardCount)
            {
                int id = Random.Range(0, m_cardInfoSO.m_infos.Count);
                if (m_lsCreateList.FindIndex(c => c.m_iId == id) == -1)
                {
                    m_lsCreateList.Add(new CardData() { m_iId = id });
                }
            }

            // Insert to card datas
            CardContainer.Instance.m_lsCardDatas = new List<CardData>(m_lsCreateList);
        }
    }
}
