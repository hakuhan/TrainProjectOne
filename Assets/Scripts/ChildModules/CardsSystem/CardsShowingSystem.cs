/* 
    create by baihan 2020.02.21 
    管理card的效果 
*/

using System.Collections;
using TPOne.Datas;
using TPOne.Events;
using TPOne.Ui;
using UnityEngine;

namespace TPOne.Submodule
{
    public class CardsShowingSystem : MonoBehaviour
    {
        public CardInfoSO m_cardInfo;
        public GameObject m_objCardTemp;
        public Transform m_tfContainer;
        public GameObject m_objHideMask;

        #region life circle

        private void OnEnable()
        {
            RefreshEvents.RefreshCard += RefreshCards;
            RefreshEvents.RefreshCardDelayWithOpen += RefreshWithDelayOpenAnimation;
            RefreshEvents.RefreshFoldAndOpen += RefreshFoldAndOpen;
            ShowingCardEvents.PopupCard += PopupCards;
            RefreshEvents.OnCardDataRefreshed += PopDownAll;
        }

        private void OnDisable()
        {
            RefreshEvents.RefreshCard -= RefreshCards;
            RefreshEvents.RefreshCardDelayWithOpen -= RefreshWithDelayOpenAnimation;
            RefreshEvents.RefreshFoldAndOpen -= RefreshFoldAndOpen;
            ShowingCardEvents.PopupCard -= PopupCards;
            RefreshEvents.OnCardDataRefreshed -= PopDownAll;
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

            PopDownAll();
        }

        public void RefreshWithDelayOpenAnimation()
        {
            StartCoroutine(RefreshAsync());
            PopDownAll();
        }

        IEnumerator RefreshAsync()
        {
            m_objHideMask.SetActive(true);

            var lsDatas = CardContainer.Instance.m_lsCardDatas;
            var lsCards = CardContainer.Instance.m_lsCards;
            var lsCardRunnningDatas = CardContainer.Instance.m_lsCardRunningData;

            for (int i = 0; i < lsCards.Count; ++i)
            {
                // Reset card
                lsCards[i].HideCard();
                lsCardRunnningDatas[i].m_bVisble = true;
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
            m_objHideMask.SetActive(false);
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

            PopDownAll();
        }

        public void PopupCards(int[] lsCardId)
        {
            var lsCards = CardContainer.Instance.m_lsCards;
            var lsCardRunnningDatas = CardContainer.Instance.m_lsCardRunningData;
            var lsCardData = CardContainer.Instance.m_lsCardDatas;
            for (int i = 0; i < lsCardRunnningDatas.Count; ++i)
            {
                bool bFind = false;
                for (int j = 0; j < lsCardId.Length; ++j)
                {
                    if (lsCardId[j] == lsCardData[i].m_iId)
                    {
                        bFind = true;
                        break;
                    }
                }

                if (bFind && !lsCardRunnningDatas[i].m_bSelected)
                {
                    lsCards[i].ChangeState(true);
                }
                else if (!bFind && lsCardRunnningDatas[i].m_bSelected)
                {
                    lsCards[i].ChangeState(false);
                }
            }
        }

        public void PopDownAll()
        {
            var lsCards = CardContainer.Instance.m_lsCards;
            var lsCardRunnningDatas = CardContainer.Instance.m_lsCardRunningData;
            for (int i = 0; i < lsCardRunnningDatas.Count; ++i)
            {
                if (lsCardRunnningDatas[i].m_bSelected)
                {
                    lsCards[i].ChangeState(false);
                }
                lsCardRunnningDatas[i].m_bSelected = false;
                lsCardRunnningDatas[i].m_bSelectedTemp = false;
            }
        }
    }
}