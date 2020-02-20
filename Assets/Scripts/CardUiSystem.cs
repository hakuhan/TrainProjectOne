using System.Collections;
using System.Collections.Generic;
using TPOne.Submodule;
using UnityEngine;

namespace TPOne.Ui
{
    public class CardUiSystem : MonoBehaviour
    {
        public GameObject m_objCardTemp;
        public Transform m_tfCardContainer;
        public int m_iCardCount = 13;
        public List<Card> m_lsCards;
        void Awake()
        {
            m_lsCards = new List<Card>();

            for (int i = 0; i < m_iCardCount; ++i)
            {
                var cObj = Instantiate(m_objCardTemp, m_tfCardContainer);
                var card = cObj.GetComponent<Card>();
                m_lsCards.Add(card);
                card.gameObject.SetActive(false);
            }
        }

        public void RefreshCards()
        {
            var lsDatas = CardContainer.Instance.m_lsCardDatas;

            for (int i = 0; i < m_lsCards.Count; ++i)
            {
                // Reset card
                m_lsCards[i].HideCard();
            }

            for (int j = 0; j < lsDatas.Count; ++j)
            {
                // Update card
                m_lsCards[j].Refresh(lsDatas[j]);
            }
        }
    }
}
