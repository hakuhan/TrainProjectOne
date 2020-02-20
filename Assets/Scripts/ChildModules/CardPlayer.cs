/* 
    create by baihan 2020.02.20 
    CardPlayer: 负责出牌操作 
*/

using System.Collections.Generic;
using UnityEngine;

namespace TPOne.Submodule
{
    public class CardPlayer : MonoBehaviour, ICardPlayer
    {
        public void PlayCard(List<int> lsCards)
        {
            // Remove selected card
            foreach (int id in lsCards)
            {
                int index = CardContainer.Instance.m_lsCardDatas.FindIndex(c => c.m_iId == id);
                if (index != -1)
                {
                    CardContainer.Instance.m_lsCardDatas.RemoveAt(index);
                }
            }

        }
    }
}