/* 
    create by baihan 2020.02.21 
    管理card的效果 
*/

using System.Collections;
using TPOne.Datas;
using TPOne.Events;
using UnityEngine;

namespace TPOne.Submodule
{
    public class CardsShowingSystem : MonoBehaviour
    {
        public CardInfoSO m_cardInfo;
        public GameObject m_objCardTemp;
        public Transform m_tfContainer;

        #region life circle

        private void OnEnable()
        {
            RefreshEvents.RefreshCard += RefreshCards;
            RefreshEvents.RefreshCardDelayWithOpen += RefreshWithDelayOpenAnimation;
            RefreshEvents.RefreshFoldAndOpen += RefreshFoldAndOpen;
        }

        private void OnDisable()
        {
            RefreshEvents.RefreshCard -= RefreshCards;
            RefreshEvents.RefreshCardDelayWithOpen -= RefreshWithDelayOpenAnimation;
            RefreshEvents.RefreshFoldAndOpen -= RefreshFoldAndOpen;
        }

        #endregion

        public void InitCard()
        {
            var lsCards = CardContainer.Instance.m_lsCards;
            if (CardContainer.Instance.m_lsCards.Count > 0)
            {
                foreach (var c in lsCards)
                {
                    Destroy(c.gameObject);
                }
            }

            lsCards.Clear();

            for (int i = 0; i < m_cardInfo.m_iCardInHandCount; ++i)
            {
                var cObj = Instantiate(m_objCardTemp, m_tfContainer);
                var card = cObj.GetComponent<Card>();
                lsCards.Add(card);
                card.gameObject.SetActive(false);
            }
        }

        public void RefreshCards()
        {
            var lsDatas = CardContainer.Instance.m_lsCardDatas;
            var lsCards = CardContainer.Instance.m_lsCards;

            for (int i = 0; i < lsCards.Count; ++i)
            {
                // Reset card
                lsCards[i].HideCard();
            }

            for (int j = 0; j < lsDatas.Count; ++j)
            {
                // Update card
                lsCards[j].Refresh(lsDatas[j]);
                lsCards[j].Show();
                lsCards[j].Open();
            }
        }

        public void RefreshWithDelayOpenAnimation()
        {
            StartCoroutine(RefreshAsync());
        }

        IEnumerator RefreshAsync()
        {
            var lsDatas = CardContainer.Instance.m_lsCardDatas;
            var lsCards = CardContainer.Instance.m_lsCards;

            for (int i = 0; i < lsCards.Count; ++i)
            {
                // Reset card
                lsCards[i].HideCard();
            }

            for (int j = 0; j < lsDatas.Count; ++j)
            {
                // Update card
                lsCards[j].Refresh(lsDatas[j]);
                lsCards[j].Fold();
            }

            foreach (var c in lsCards)
            {
                c.Show();
                yield return new WaitForSeconds(0.1f);
            }

            foreach (var c in lsCards)
            {
                c.Open();
            }
        }

        public void RefreshFoldAndOpen()
        {
            var lsDatas = CardContainer.Instance.m_lsCardDatas;
            var lsCards = CardContainer.Instance.m_lsCards;

            for (int j = 0; j < lsDatas.Count; ++j)
            {
                // Update card
                lsCards[j].Refresh(lsDatas[j]);
            }

            for (int i = 0; i < lsCards.Count; ++i)
            {
                lsCards[i].Fold();
                lsCards[i].OpenWithAnimation();
            }
        }
    }
}