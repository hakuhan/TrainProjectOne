/* 
    create by baihan 2020.02.20 
    CardHost：管理整个卡牌系统 
        1. 相应ui点击事件
        2. 管理发牌、出牌、排序子模块以及通信
*/
using System.Collections;
using System.Collections.Generic;
using TPOne.Events;
using TPOne.Submodule;
using TPOne.Ui;
using UnityEngine;

namespace TPOne
{
    public class CardHost : MonoBehaviour
    {
        #region Properties

        public ICardCreater m_cardCreater;
        public ICardSorter m_cardSorter;
        public ICardPlayer m_cardPlayer;
        public CardUiSystem m_cardUiSystem;
        public CardRuler m_cardRuler;
        List<int> m_lsSelectedCards;

        #endregion

        #region Override

        void Awake()
        {
            m_cardCreater = GetComponent<NomalCardCreater>();
            m_cardPlayer = GetComponent<CardPlayer>();
            m_cardSorter = GetComponent<BubbleSorter>();
            m_lsSelectedCards = new List<int>();

            m_cardRuler.UpdateRule();
        }

        public void OnDisable()
        {
            EventSystem.OnCardClicked -= OnSelectedCard;
            EventSystem.OnCardCanceled -= OnCancelCard;
        }

        public void OnEnable()
        {
            EventSystem.OnCardClicked += OnSelectedCard;
            EventSystem.OnCardCanceled += OnCancelCard;
        }

        #endregion

        #region public func
        public void CreateCards()
        {
            m_cardCreater.CreateCards();
            m_cardUiSystem.RefreshViewAnimaition();
        }

        public void PlayCards()
        {
            m_cardPlayer.PlayCard(m_lsSelectedCards);
            m_cardUiSystem.RefreshCards();

            m_lsSelectedCards.Clear();
        }

        public void ReorderCards(bool reverse)
        {
            m_cardSorter.SortCards(reverse);
            m_cardUiSystem.RefreshWithOpenAnimation();
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

        #endregion
    }
}
