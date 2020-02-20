/* 
    create by baihan 2020.02.20 
    BubbleSorter:提供排序功能 
*/

using UnityEngine;

namespace TPOne.Submodule
{
    public class BubbleSorter : MonoBehaviour, ICardSorter
    {
        public CardInfoSO m_cardInfos;
        public void SortCards(bool bReverse)
        {
            Utils.Order(CardContainer.Instance.m_lsCardDatas, m_cardInfos, bReverse);
        }
    }
}