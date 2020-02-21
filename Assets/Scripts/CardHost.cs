/* 
    create by baihan 2020.02.20 
    CardHost：管理整个卡牌系统 
        1. 相应ui点击事件
        2. 管理发牌、出牌、排序子模块以及通信
*/
using System.Collections;
using System.Collections.Generic;
using TPOne.CardSelector;
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

        // Selector
        public ICardSelector m_pairSelector;
        public ICardSelector m_bombSelector;
        public ICardSelector m_sequenceSelector;
        public ICardSelector m_sHUNZASelector;
        public ICardSelector m_tierceSelector;

        #endregion

        #region Override

        void Awake()
        {
            m_cardCreater = GetComponentInChildren<NomalCardCreater>();
            m_cardPlayer = GetComponentInChildren<CardPlayer>();
            m_cardSorter = GetComponentInChildren<BubbleSorter>();

            // selector
            m_pairSelector = GetComponentInChildren<PairS>();
            m_bombSelector = GetComponentInChildren<BombS>();
            m_sequenceSelector = GetComponentInChildren<SequencePairS>();
            m_sHUNZASelector = GetComponentInChildren<SHUNZAS>();
            m_tierceSelector = GetComponentInChildren<TierceS>();

            m_cardRuler.UpdateRule();
        }

        #endregion

        #region public func
        public void CreateCards()
        {
            m_cardCreater.CreateCards();
        }

        public void PlayCards()
        {
            m_cardPlayer.PlayCard();
        }

        public void ReorderCards(bool reverse)
        {
            m_cardSorter.SortCards(reverse);
        }

        #region Selector
        public void OnSelectPair()
        {
            m_pairSelector.SelectCard();
        }

        public void OnSelectBomb()
        {
            m_bombSelector.SelectCard();
        } 

        public void OnSequencePari()
        {
            m_sequenceSelector.SelectCard();
        }

        public void OnSelectSHUNZA()
        {
            m_sHUNZASelector.SelectCard();
        }

        public void OnSelectTierce()
        {
            m_tierceSelector.SelectCard();
        }

        #endregion

        #endregion
    }
}
