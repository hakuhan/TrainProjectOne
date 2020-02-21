/* 
    create by baihan 2020.02.20 
    CardPlayer: 负责出牌操作 
*/

using System.Collections.Generic;
using TPOne.Datas;
using TPOne.Events;
using UnityEngine;

namespace TPOne.Submodule
{
    public class CardPlayer : MonoBehaviour, ICardPlayer
    {
        List<int> m_lsSelectedCards;

        #region Life circle

        private void Awake()
        {
            m_lsSelectedCards = new List<int>();
        }

        public void OnDisable()
        {
            TouchEvents.OnCardClicked -= OnSelectedCard;
            TouchEvents.OnCardCanceled -= OnCancelCard;
        }

        public void OnEnable()
        {
            TouchEvents.OnCardClicked += OnSelectedCard;
            TouchEvents.OnCardCanceled += OnCancelCard;
        }

        #endregion

        public void PlayCard()
        {
            // Remove selected card
            foreach (int id in m_lsSelectedCards)
            {
                int index = CardContainer.Instance.m_lsCardDatas.FindIndex(c => c.m_iId == id);
                if (index != -1)
                {
                    CardContainer.Instance.m_lsCardDatas.RemoveAt(index);
                }
            }

            RefreshEvents.RefreshCard();
            RefreshEvents.OnCardDataRefreshed();
            m_lsSelectedCards.Clear();
        }

        void OnSelectedCard(int id)
        {
            int index = m_lsSelectedCards.FindIndex(c => c == id);
            if (index == -1)
            {
                m_lsSelectedCards.Add(id);
            }
        }

        void OnCancelCard(int id)
        {
            int index = m_lsSelectedCards.FindIndex(c => c == id);
            if (index != -1)
            {
                m_lsSelectedCards.RemoveAt(index);
            }
        }
    }
}